# Patika - Survivor API
Survivor API is a backend system built with ASP.NET Core and Entity Framework Core (Code-First approach). It allows users to manage competitors and categories through a RESTful API, providing essential CRUD operations and soft delete functionality.

## 🚀 Project Purpose
Survivor API is designed to manage a competitive event system where categories and competitors are stored and maintained. This API allows users to create, retrieve, update, and delete both categories and competitors, ensuring smooth data management for competition-based applications.

## 🎯 Features
- RESTful API architecture
- Entity Framework Core with Code-First Approach
- SQL Server integration with migration support
- CRUD operations for Categories and Competitors
- Soft delete mechanism to maintain data integrity
- Swagger documentation for easy API testing
- Dependency Injection for service management
- Organized request and response DTOs for better API structure

## 🔗 API Endpoints
### Categories
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/categories` | Create a new category |
| **GET** | `/api/categories` | Get all categories |
| **GET** | `/api/categories/{id}` | Get category by ID |
| **PUT** | `/api/categories/{id}` | Update category |
| **DELETE** | `/api/categories/{id}` | Soft delete category |

### Competitors
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/competitors` | Create a new competitor |
| **GET** | `/api/competitors` | Get all competitors |
| **GET** | `/api/competitors/{id}` | Get competitor by ID |
| **GET** | `/api/competitors/categories/{categoryId}` | Get competitors by category |
| **PUT** | `/api/competitors/{id}` | Update competitor |
| **DELETE** | `/api/competitors/{id}` | Soft delete competitor |

## 🗄️ Entities
### BaseEntity
```bash
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}
```
### CategoryEntity
```bash
public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }

    public List<CompetitorEntity> Competitors { get; set; }
}
```
### CompetitorEntity
```bash
public class CompetitorEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public CategoryEntity Category { get; set; }
}
```

## 📊 DbContext Configuration
```bash
public class SurvivorDbContext : DbContext
{
    public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options)
    {
        
    }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CompetitorEntity> Competitors { get; set; }
}
```

## 🛠️ Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Windows Authentication)
- Swagger for API Documentation
- Dependency Injection

## 🏗️ Installation and Execution

Follow these steps to run the application:

1. **Clone the project:**
   ```bash
   git clone https://github.com/ulaskarakas/PatikaSurvivorAPI.git
   ```
2. **Install .NET SDK:**
   - If not already installed, download the appropriate version from the [.NET SDK](https://dotnet.microsoft.com/download) page.

3. **Navigate to the project directory and run the application:**
   ```bash
   cd PatikaSurvivorAPI
   ```
4. **Install Dependencies:**
   ```bash
   dotnet restore
   ```
5. **Update appsettings.json with your database connection string**
   ```bash
     {
      "ConnectionStrings": {
        "Default": "Server=.\\SQLEXPRESS; Database=SurvivorDb; Trusted_Connection=true; TrustServerCertificate=true;"
      }
    }
   ```
5. **Update the Database:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. **Run the Application:**
   ```bash
   dotnet run
   ```

## 🤝 Contributing
To contribute to this project, please submit a **Pull Request** or create an **Issue**.

## ⚖️ License
This project is licensed under the MIT License.

## 📬 Contact
For any questions or suggestions, feel free to contact me:

[![Gmail](https://ziadoua.github.io/m3-Markdown-Badges/badges/Gmail/gmail1.svg)](mailto:ulaskarakas95@gmail.com)
[![LinkedIn](https://ziadoua.github.io/m3-Markdown-Badges/badges/LinkedIn/linkedin1.svg)](https://www.linkedin.com/in/ulas-karakas/)
