# ApiCatalog

A **RESTful API** for managing a product and category catalog. This project allows clients to perform CRUD operations on products based on their category organizations, and includes support for authentication, pagination, and clean architecture.

## Features

- Full CRUD for **Products** and **Categories**
  
- Result pagination
  
- Authentication using **JWT**
  
- **CORS** configuration
  
- Structure based on **Repository Pattern** and **Unit of Work**, using **DTOs**
  
- Two implementations: one using **MVC** and another following **Clean Architecture**
  

## Technologies Used

- **ASP.NET Core 8 and 9**
  
- **Entity Framework Core**
  
- **MySQL** (compatible with SQL Server)
  
- **AutoMapper**
  
- **Swagger** for API documentation
  
- **Asynchronous programming** (async/await)
  

## Project Structure

The project includes two main approaches:

1. **Traditional MVC model**
  
2. **Clean Architecture model**, separating domain, application, infrastructure, and API layers
  

## Requirements

- [.NET 8 or 9 SDK](https://dotnet.microsoft.com/en-us/download)
  
- MySQL or SQL Server
  
- Visual Studio / VS Code / JetBrains Rider
  

## How to Run

1. Clone the repository:

```bash
git clone https://github.com/seu-usuario/apicatalog.git
cd apicatalog
```

2. Configure the **connection string** in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=apicatalog;User=root;Password=senha"
}
```

3. Update the database:

```bash
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

5. Access the documentation via **Swagger**:
