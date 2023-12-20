# Coffee Tracker API

The Coffee Tracker API is a backend service for tracking and managing coffee consumption records. It provides endpoints for creating, retrieving, updating, and deleting coffee consumption records.

## Features

- RESTful API for coffee consumption records.
- CRUD operations for managing records.
- Built with ASP.NET Core and Entity Framework.

## Getting Started

To get started with the Coffee Tracker API, follow these steps:

1. Clone the repository:

    ```bash
    git clone https://github.com/1heykal/coffee-tracker-api.git
    ```

2. Navigate to the project directory:

    ```bash
    cd coffee-tracker-api
    ```

3. Configure the API by updating the `appsettings.json` file with your database connection string.

4. Build and run the API:

    ```bash
    dotnet run
    ```

## API Endpoints

- **GET /records**: Get all coffee consumption records.
- **GET /records/{id}**: Get a specific coffee consumption record by ID.
- **POST /records**: Create a new coffee consumption record.
- **PUT /records/{id}**: Update a coffee consumption record.
- **DELETE /records/{id}**: Delete a coffee consumption record.

## Configuration

Update the `appsettings.json` file with your database connection string:

```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=your-server;Database=YourCoffeeTrackerDB;Trusted_Connection=True;"
  }
}
