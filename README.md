# Thesis
This project is a comprehensive analysis of server-based applications and serverless functions, implemented with a variety of technologies.

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
- PostgreSQL
- .NET 8.0
- Microsoft.AspNet.WebApi
- AspNetCore.Identity
- RLS (Row Level Security)


### Installation

1. Clone the repository
```bash
   git clone https://github.com/yourusername/your-repo-name.git
```
2. Navigate to the project directory
```bash
    cd your-repo-name
```
3. Install the necessary dependencies. For a .NET project, you would use the `dotnet restore` command.
```bash
    dotnet restore
```
4. Set up your database. This step will vary depending on your database setup. You might need to run a script to create and populate the database or you might need to set up a connection to a remote database.

5. Start the development server. For a .NET project, you would use the `dotnet run` command.
```bash
    dotnet run
```

Please replace the placeholders (`yourusername`, `your-repo-name`, etc.) with the actual values for your project. Also, adjust the database setup step based on your project's specific requirements.

## Usage
To use this project, follow these steps:

Once you have the project running on your local machine (see the Installation section), navigate to `http://localhost:3000` (or whatever port your project is running on) in your web browser.

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
If you have any questions or would like to discuss this project further, feel free to contact me on: 
[LinkedIn](https://www.linkedin.com/in/yakhoub-soumare-2019/).

## Website
- [Server Analysis](https://server-analysis.netlify.app/) (Cold Start due to Free Tier hosting of API)

## Future Improvements
- Integration Tests
- Control of CORS Policies