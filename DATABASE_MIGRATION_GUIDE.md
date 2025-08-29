# Database Migration and Update Solution

## Problem
You're encountering the error: `LocalDB is not supported on this platform` when trying to add migrations and update the database. This happens because LocalDB is a Windows-only feature and doesn't work on Linux/Mac environments.

## Solutions

### Option 1: Use SQL Server Docker Container (Recommended)

1. **Install Docker** on your system if not already installed.

2. **Run SQL Server in Docker:**
   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd" \
   -p 1433:1433 --name sqlserver-dev \
   -d mcr.microsoft.com/mssql/server:2022-latest
   ```

3. **Update connection string** in `appsettings.json` or `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DBConnection": "Server=localhost,1433;Database=itInstituteDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Run migrations:**
   ```bash
   dotnet ef database update --project MS3-Back-End
   ```

### Option 2: Use SQLite for Development (Alternative)

The project has been configured to automatically use SQLite when LocalDB is detected on non-Windows platforms.

1. **Set environment to Development:**
   ```bash
   export ASPNETCORE_ENVIRONMENT=Development
   ```

2. **The application will automatically use SQLite** with the connection string:
   `Data Source=itInstituteDB.db`

3. **Note:** Existing migrations are SQL Server-specific and won't work with SQLite. You would need to:
   - Delete existing migration files (backup first!)
   - Create new SQLite-compatible migrations
   - Handle data type differences between SQL Server and SQLite

### Option 3: Use Azure SQL Database (Production)

For production environments, consider using Azure SQL Database or any cloud SQL Server instance.

Update the connection string to point to your cloud database:
```json
{
  "ConnectionStrings": {
    "DBConnection": "Server=your-server.database.windows.net;Database=itInstituteDB;User Id=your-username;Password=your-password;Encrypt=true;TrustServerCertificate=false;MultipleActiveResultSets=true"
  }
}
```

## Quick Commands

### After setting up SQL Server (Option 1):
```bash
# List migrations
dotnet ef migrations list --project MS3-Back-End

# Add new migration
dotnet ef migrations add MigrationName --project MS3-Back-End

# Update database
dotnet ef database update --project MS3-Back-End

# Remove last migration (if needed)
dotnet ef migrations remove --project MS3-Back-End
```

### For SQLite Development (Option 2):
```bash
# Set environment
export ASPNETCORE_ENVIRONMENT=Development

# List migrations (will show pending for SQLite)
dotnet ef migrations list --project MS3-Back-End

# Note: You'll need to create SQLite-compatible migrations
```

## Additional Notes

- The project now includes SQLite support as a fallback for development
- The Program.cs automatically detects the platform and connection string type
- For production, always use a proper SQL Server instance
- Make sure to backup your database before running migrations in production