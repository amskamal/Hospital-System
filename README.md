# 🏥 Hospital System

A web-based Hospital Management System built with **ASP.NET Core MVC**, **C#**, and **SQL Server (SSMS)**. This application provides a structured platform to manage hospital operations including patients, doctors, and appointments through a clean web interface.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core MVC (.NET 5) |
| Language | C# |
| Database | SQL Server (SSMS) + Entity Framework Core |
| Frontend | HTML, CSS, JavaScript |
| ORM | Entity Framework Core (Code-First with Migrations) |

---

## 📁 Project Structure

```
Hospital-System/
├── Controllers/        # MVC Controllers (request handling & routing)
├── Data/               # DbContext and database configuration
├── Migrations/         # Entity Framework migration files
├── Models/             # Data models / entities
├── Views/              # Razor views (UI templates)
├── wwwroot/            # Static files (CSS, JS, images)
├── Program.cs          # Application entry point
├── Startup.cs          # Service configuration and middleware pipeline
├── appsettings.json    # App configuration (connection strings, etc.)
└── Hospital.csproj     # Project file
```

---

## ⚙️ Prerequisites

Before running this project, make sure you have the following installed:

- [.NET 5 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Visual Studio 2019/2022](https://visualstudio.microsoft.com/) (recommended) or VS Code

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/amskamal/Hospital-System.git
cd Hospital-System
```

### 2. Configure the Database Connection

Open `appsettings.json` and update the connection string to match your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=HospitalDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 3. Apply Database Migrations

Run the following command to create and seed the database:

```bash
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

Or open `Hospital.sln` in Visual Studio and press **F5** to run.

The app will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## 🌐 Features

- Manage hospital records through a web interface
- MVC architecture with clean separation of concerns
- Entity Framework Code-First database migrations
- Responsive frontend with static assets (CSS/JS)
- Routing via ASP.NET Core endpoint configuration

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add your feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 👤 Author

**amskamal** — [GitHub Profile](https://github.com/amskamal)
