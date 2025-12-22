# JWT Authentication Sample with Clean Architecture

A sample .NET 10 Web API project demonstrating JWT-based authentication and authorization using Clean Architecture principles. This project showcases role-based and scope-based access control with a PostgreSQL database backend.

## Architecture

This solution follows Clean Architecture principles with clear separation of concerns across three projects:

### Projects Structure

- **AuthChallenge.Api**: Presentation layer containing API endpoints and middleware configuration
- **AuthChallenge.Application**: Application layer containing business logic, interfaces, and authorization policies
- **AuthChallenge.Infrastructure**: Infrastructure layer containing data access, repositories, and external dependencies

### Key Architectural Decisions

- **Core Business Logic**: Authentication policies (roles and scopes) are treated as core business concerns and are defined in the Application layer
- **External Dependencies**: Database and infrastructure concerns are isolated in the Infrastructure project
- **Repository Pattern**: Data access is abstracted through the Repository pattern
- **Unit of Work**: Transaction management is handled through the Unit of Work pattern

## Technology Stack

- **.NET 10**: Latest version of the .NET platform
- **PostgreSQL**: Relational database
- **Entity Framework Core**: Latest version for ORM and data access
- **Microsoft.IdentityModel.JsonWebTokens**: Modern, high-performance JWT implementation
- **BCrypt.Net-Next**: Secure password hashing
- **Swagger/OpenAPI**: API documentation

## Authentication & Authorization

### JWT Implementation

The project uses `Microsoft.IdentityModel.JsonWebTokens` instead of the older `System.IdentityModel.Jwt` for better performance and modern API design.

JWT tokens include the following claims:
- **Issuer**: Token issuer identifier
- **Audience**: Intended token audience
- **Scope**: Custom claim for scope-based authorization

### Authorization Policies

The following authorization policies are implemented:

1. **ScreenWriterAdmin**
   - Requires: `Admin` role AND `screenings:write` scope
   - Use case: Administrative operations on screenings

2. **Admin**
   - Requires: `Admin` role
   - Use case: General administrative operations

3. **Authenticated**
   - Requires: Valid JWT token (enforced via `[Authorize]` attribute)
   - Use case: Protected resources requiring authentication

### Password Security

Passwords are hashed using BCrypt.Net-Next, providing strong cryptographic protection with built-in salt generation.

## API Endpoints

### Authentication

- **POST** `/api/auth/login`
  - Authenticates user with username and password
  - Returns JWT access token
  - No authentication required

### Tickets

- **POST** `/api/tickets/reserve`
  - Reserves a ticket
  - Requires: Authenticated user

### Screenings

- **GET** `/api/screenings/getall`
  - Retrieves all screenings
  - No authentication required

- **POST** `/api/screenings/add`
  - Adds a new screening
  - Requires: `Admin` role AND `screenings:write` scope (ScreenWriterAdmin policy)

## Configuration

### JWT Settings

JWT configuration is managed through `IConfiguration` abstraction, supporting multiple configuration providers:

- **Development**: `appsettings.json`
- **Production**: Environment variables, Azure Key Vault, or other secure providers

Example `appsettings.json` structure:

```json
{
  "Jwt": {
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "SecretKey": "your-secret-key-min-32-characters",
    "ExpirationMinutes": 60
  }
}
```

### Database Connection

Configure your PostgreSQL connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=authchallenge;Username=postgres;Password=yourpassword"
  }
}
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL database server

### Setup

1. **Clone the repository**

```bash
git clone <repository-url>
cd AuthChallenge
```

2. **Configure the database connection**

Update the connection string in `appsettings.json` with your PostgreSQL credentials.

3. **Configure JWT settings**

Update the JWT settings in `appsettings.json` with your desired configuration. Ensure the `SecretKey` is at least 32 characters long.

4. **Apply database migrations**

```bash
dotnet ef database update --project AuthChallenge.Infrastructure --startup-project AuthChallenge.Api
```

5. **Run the application**

```bash
dotnet run --project AuthChallenge.Api
```

The API will be available at `https://localhost:5001` (or the configured port).

6. **Access API documentation**

Navigate to `https://localhost:5001/swagger` to view the interactive API documentation.

## User Model

The User entity includes the following properties:

- **Id**: Unique identifier
- **Username**: Login username
- **Name**: User's display name
- **PasswordHash**: BCrypt-hashed password
- **Role**: User's role for role-based authorization
- **Scopes**: List of scopes for fine-grained access control

## Project Highlights

- ✅ Clean Architecture with proper separation of concerns
- ✅ Modern JWT implementation with scope-based authorization
- ✅ Secure password hashing with BCrypt
- ✅ Repository and Unit of Work patterns
- ✅ Entity Framework Core with PostgreSQL
- ✅ Built-in dependency injection
- ✅ Comprehensive authorization policies
- ✅ Swagger/OpenAPI documentation

## License

This project is a sample implementation for educational purposes.