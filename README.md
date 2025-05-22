## ⚠️ NOTICE  
**Ongoing updates and refactoring to resolve initial issues with the project.**  
_Last updated: 2025-05-22_


## 🔄 Backend Updates
- Migrated database and storage from Supabase to Azure.
- Introduced Infrastructure as Code (IaC) using Terraform to configure Azure resources.
- Resolved deployment issues on Azure by updating Docker setup and web app configuration.
- Transitioned CI/CD pipeline from Render to Azure using secure credentials.



# Thesis
This project is a comprehensive analysis of server-based applications and serverless functions, implemented with a variety of technologies.

## Getting Started
These instructions will get the user a copy of the project up and running on the local machine for development and testing purposes.

### Prerequisites
- PostgreSQL
- .NET 8.0
- Microsoft.AspNet.WebApi
- AspNetCore.Identity
- RLS (Row Level Security)


### Installation

1. Clone the repository
```bash
   git clone https://github.com/yourusername/the-repo-name.git
```
2. Navigate to the project directory
```bash
    cd the-repo-name
```
3. Install the necessary dependencies. For a .NET project, the user would use the `dotnet restore` command.
```bash
    dotnet restore
```
4. Set up the database. This step will vary depending on the database setup. the user might need to run a script to create and populate the database or the user might need to set up a connection to a remote database.

5. Start the development server. For a .NET project, the user would use the `dotnet run` command.
```bash
    dotnet run
```

Please replace the placeholders (`yourusername`, `the-repo-name`, etc.) with the actual values for the project. Also, adjust the database setup step based on the project's specific requirements.

## Usage
To use this project, follow these steps:

Once the user have the project running on the local machine (see the Installation section), navigate to `http://localhost:3000` (or whatever port the project is running on) in the web browser.

- **Navigate through the application**: 
Use the navigation bar/menu to move between different sections of the application. 

- Each section of Home provides different topics of discussion related to server analysis.
- Each Section of About discusses the technology used for this project.

## Built With
This project uses a variety of technologies:

### Back-End
- C#: The back-end logic is written in C#.
- .NET 8.0: The project uses the .NET 8.0 framework.
- Microsoft.AspNet.WebApi: This package is used to build the API.
- AspNetCore.Identity: This package is used for user authentication.
- RLS (Row Level Security): This is used for database security.

### Front-End
- React.js: The front-end interface is built with React.js.
- JavaScript: Additional front-end functionality is implemented with JavaScript.
- HTML/CSS: The layout and styling of the web interface are done with HTML and CSS.

### Unit Tests
- MSTest: Unit tests are written using the MSTest framework.

### Database
- PostgreSQL: The project uses a PostgreSQL database.

### Deployment
- Docker Image: The project is containerized using Docker.
- GitHub Packages: The Docker image is stored in GitHub Packages.
- Render/Microsoft Azure/Supabase/Netlify: The project is deployed on these platforms.

### CI/CD
- GitHub Actions: This is used for continuous integration and continuous deployment.

### Back-End
- C#

### Front-End
- React.js
- JavaScript
- HTML
- CSS

### Unit Tests
- MSTest

## Deployment
- Docker Image
- GitHub Packages
- Render
- Microsoft Azure
- Supabase
- Netlify

### Continuous Integration/Continuous Deployment (CI/CD)
- GitHub Actions is used for CI/CD.

- The CI/CD workflow works for deploying to Microsoft Azure, but there is an issue with the Azure portal not recognizing the port even when it is set in the workflow.

- The CI part of the workflow works fine for Render, but more research is needed to resolve issues with its' CD part of the workflow.

## Discussion
If the user have any questions or would like to discuss this project further, feel free to contact me on: 
[LinkedIn](https://www.linkedin.com/in/yakhoub-soumare-2019/).

## Website
- [Server Analysis](https://server-analysis.netlify.app/) (Cold Start due to Free Tier hosting of API)

## Future Improvements
- Integration Tests
- Control of CORS Policies