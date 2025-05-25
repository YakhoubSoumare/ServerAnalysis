# Generates a unique resource group name using a random pet name
resource "random_pet" "rg_name" {
  prefix      = var.resource_group_name_prefix
}

# Creates an Azure resource group with a unique name and specified location
resource "azurerm_resource_group" "rg" {
  name        = random_pet.rg_name.id
  location    = var.resource_group_location
}

# Generates a unique SQL Server name using a random pet name with "sql" prefix
resource "random_pet" "azurerm_mssql_server_name" {
  prefix      = "sql"
}

# Generates a random password with specific criteria (length, special characters, and complexity).
resource "random_password" "admin_password" {
  count         = var.admin_password == null ? 1 : 0
  length        = var.password_length >= 10 ? var.password_length : 10     # Make sure it's at least 10
  special       = true
  min_numeric   = 1
  min_upper     = 1
  min_lower     = 1
  min_special   = 1
}

# Attempts to use the randomly generated admin password; falls back to a manually provided password if generation fails.
locals {
  admin_password    = try(random_password.admin_password[0].result, var.admin_password)
}

# SQL Server creation in the new resource group
resource "azurerm_mssql_server" "server" {
  name                            = random_pet.azurerm_mssql_server_name.id
  resource_group_name             = azurerm_resource_group.rg.name         # Reference the new resource group
  location                        = azurerm_resource_group.rg.location     # Same location as before
  administrator_login             = var.admin_username
  administrator_login_password    = local.admin_password
  version                         = "12.0"
  minimum_tls_version             = "1.2"                                      # Enforcing TLS 1.2 for secure connections

  # Sets connectivity method to Public Endpoint
  public_network_access_enabled   = true                                       # Enabling public endpoint

  # Sets Connection policy to Default
  connection_policy               = "Default"
}

# Allowing list of IPs from tfvars
resource "azurerm_mssql_firewall_rule" "allow_appservice_outbound" {
  for_each = toset(var.appservice_outbound_ips)

  name              = "AllowAppIP-${replace(each.value, ".", "-")}"  # Name needs safe chars
  server_id         = azurerm_mssql_server.server.id
  start_ip_address  = each.value                                     # IP stays unchanged
  end_ip_address    = each.value
}

resource "azurerm_mssql_database" "db"{
  name                             = var.sql_db_name
  server_id                        = azurerm_mssql_server.server.id
  collation                        = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb                      = 32
  sku_name                         = "GP_S_Gen5_1"
  auto_pause_delay_in_minutes      = 60
  min_capacity                     = 0.5
  zone_redundant                   = false
  read_scale                       = false
  # license_type                     = "LicenseIncluded"                 # Not valid here (serverless). Only valid for provisioned vCore. 
  elastic_pool_id                  = null                                # Ensures it's not part of an elastic pool
  storage_account_type             = "Local"                             # Ensures that the storage_account_type is not geo (cost reasons)

  tags = {}
}

resource "azurerm_service_plan" "asp" {
  name                = "asp-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku_name = var.app_service_plan_sku_size

  os_type             = "Linux"  # must be Linux for Docker
}

resource "azurerm_linux_web_app" "web_app" {
  name                = "server-analysis-api-${var.environment}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id = azurerm_service_plan.asp.id

  site_config {
    # configure docker container image
    application_stack {
      docker_image = "ghcr.io/yakhoubsoumare/serveranalysisapi"
      docker_image_tag  = "latest"
    }
    
    always_on = false  # Explicitly disable "Always On"
  }

  app_settings = {
    "WEBSITES_PORT"                  = "8080"
    "SEEDED_ADMIN_EMAIL"            = var.seeded_admin_email
    "SEEDED_ADMIN_PASSWORD"         = var.seeded_admin_password
    "DOCKER_REGISTRY_SERVER_URL"    = "https://ghcr.io"
    "DOCKER_REGISTRY_SERVER_USERNAME" = var.docker_registry_username
    "DOCKER_REGISTRY_SERVER_PASSWORD" = var.docker_registry_password
  }

  connection_string {
    name  = "DATABASE_CONNECTION_STRING"
    type  = "SQLAzure"
    value = var.connection_string_value
  }

  https_only = true
}