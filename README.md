# Server Analysis

Full-stack project built with .NET 8 and React.

The project was created to compare server-based applications and serverless functions. It includes a backend API, frontend, database integration, authentication, infrastructure code, deployment setup, and tests.

## Tech stack

### Backend
- .NET 8
- ASP.NET Core Web API
- ASP.NET Core Identity
- Azure SQL

### Frontend
- React
- JavaScript
- HTML
- CSS

### Infrastructure
- Terraform
- Docker
- GitHub Actions
- Azure

### Testing
- MSTest

## Architecture

```text
React frontend
      ↓
.NET API
      ↓
Azure SQL
```

Terraform is used for infrastructure.

GitHub Actions is used for build and deployment workflows.

## Run locally

### Backend

```bash
dotnet restore
dotnet run
```

### Frontend

```bash
npm install
npm run dev
```

## Tests

Run the backend tests with:

```bash
dotnet test
```

## Deployment

The project was previously deployed using Azure App Service, Azure SQL, and Netlify.

The hosted services are no longer active.

The project can still be run locally.

## Project status

The project is complete and kept as a portfolio project.

## History

During development, the project used different hosting and database services before moving to Azure.

Older deployment services are no longer used.
