#!/bin/bash

# SQL Server Docker Setup Script for MS3-Back-End

echo "Setting up SQL Server Docker container for development..."

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "ERROR: Docker is not installed. Please install Docker first."
    echo "Visit: https://docs.docker.com/get-docker/"
    exit 1
fi

# Check if Docker is running
if ! docker info &> /dev/null; then
    echo "ERROR: Docker is not running. Please start Docker service."
    exit 1
fi

# Stop and remove existing container if it exists
echo "Stopping existing SQL Server container (if any)..."
docker stop sqlserver-dev 2>/dev/null || true
docker rm sqlserver-dev 2>/dev/null || true

# Run SQL Server container
echo "Starting SQL Server 2022 container..."
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Passw0rd123" \
    -p 1433:1433 --name sqlserver-dev \
    -d mcr.microsoft.com/mssql/server:2022-latest

# Wait for container to start
echo "Waiting for SQL Server to start..."
sleep 10

# Check if container is running
if docker ps | grep -q sqlserver-dev; then
    echo "✅ SQL Server container is running successfully!"
    echo ""
    echo "Connection details:"
    echo "  Server: localhost,1433"
    echo "  Database: (will be created by EF migrations)"
    echo "  Username: sa"
    echo "  Password: YourStrong@Passw0rd123"
    echo ""
    echo "Update your appsettings.json with:"
    echo '  "ConnectionStrings": {'
    echo '    "DBConnection": "Server=localhost,1433;Database=itInstituteDB;User Id=sa;Password=YourStrong@Passw0rd123;TrustServerCertificate=true;MultipleActiveResultSets=true"'
    echo '  }'
    echo ""
    echo "Then run: dotnet ef database update --project MS3-Back-End"
else
    echo "❌ Failed to start SQL Server container"
    echo "Check Docker logs: docker logs sqlserver-dev"
    exit 1
fi