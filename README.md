# Books Archiving System

An ASP.NET Core MVC application for managing a book archive with role-based authentication and authorization.

## Features

- **User Authentication & Authorization**: Role-based access control with Admin, Librarian, and User roles
- **Book Management**: Full CRUD operations for books, authors, and categories
- **Responsive Design**: Bootstrap 5 for modern, mobile-friendly UI
- **Mock Data**: Pre-seeded test data for immediate testing
- **SQLite Database**: Lightweight, file-based database for easy deployment

## Architecture

The application follows n-tier architecture:

- **Data Layer**: Entity Framework Core models, DbContext, and data seeding
- **Business Layer**: Services, DTOs, and business logic
- **Presentation Layer**: Controllers, Views, and UI components

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the following commands:

```powershell
dotnet restore
dotnet build
dotnet run
```

The application will start at `https://localhost:5001` or `http://localhost:5000`.

### Default Users

The application seeds the following test users:

| Email                     | Password      | Role      |
| ------------------------- | ------------- | --------- |
| admin@bookarchive.com     | Admin123!     | Admin     |
| librarian@bookarchive.com | Librarian123! | Librarian |
| user@bookarchive.com      | User123!      | User      |

## Project Structure

```
BooksArchivingSystem/
├── Data/
│   ├── Models/          # Entity models
│   ├── Seed/            # Data seeding
│   └── ApplicationDbContext.cs
├── Business/
│   ├── DTOs/            # Data Transfer Objects
│   └── Services/        # Business logic services
├── Controllers/         # MVC Controllers
├── Views/              # Razor Views
└── wwwroot/            # Static files
```

## Key Technologies

- ASP.NET Core 8.0 MVC
- Entity Framework Core
- ASP.NET Core Identity
- SQLite Database
- Bootstrap 5
- Razor Pages

## Database Schema

The application uses the following main entities:

- **Books**: Core book information with ISBN, title, description, etc.
- **Authors**: Author information with biography and dates
- **Categories**: Book categorization
- **BookAuthors**: Many-to-many relationship between books and authors
- **ApplicationUser**: Extended Identity user with first/last name

## Authorization

The application implements role-based authorization:

- **Admin**: Full access to all features
- **Librarian**: Can manage books, authors, and categories
- **User**: Read-only access to book information

## Development

### Adding New Features

1. Add models to the `Data/Models` folder
2. Update `ApplicationDbContext` to include new DbSets
3. Create corresponding DTOs in `Business/DTOs`
4. Implement services in `Business/Services`
5. Create controllers and views in `Controllers` and `Views`

### Database Migrations

To create and apply migrations:

```powershell
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.
