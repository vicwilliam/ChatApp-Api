# ChatApp Api
Instructions to run the api

## 0 - Requirements
These are the necessary software to run the application, you can install them directly on your machine or you can use a container.
```
RabbitMQ 
PostgreSQL 
.NET 6 SDK 
```
## 1 - Configure credentials
First, setup the credentials for Postgres and RabbitMQ and also the token parameters on ChatApp.Api/appSettings.json and appsettings.Development.json

## 2 - Build
Restore the nuget packages and build the solution by opening it through an IDE or using CLI.
The startup project is **ChatApp.Api**.

## 3 - Run migrations
You have to run the migrations before starting the project for the first time. Go to the root directory of the project and run the command.
```
dotnet ef --startup-project ChatApp.Api/ChatApp.Api.csproj --project ./ChatApp.Infrastructure/ChatApp.Infrastructure.csproj database update
```

## 4  - Run the application
You can now run the application. Use a CLI or an IDE.
