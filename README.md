# Task Management System - Backend

Backend of the Task Management System built with ASP.NET Core Web API.

## Features

* User Registration and Login
* JWT Authentication
* User and Admin Roles
* Task CRUD Operations
* User Task Authorization
* Admin User Management
* Search and Filtering
* Global Exception Handling

## Technologies

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt

## Project Structure

```text
Controllers
Services
Models
DTOs
Data
Middleware
```

## Database

The application uses **SQL Server** with **Entity Framework Core**.

## Run the Project

Open the project in Visual Studio and run the application.

The API will start using the configured HTTPS URL.

## Authentication

The API uses JWT Bearer Authentication.

* Users receive a JWT token after login.
* Protected endpoints require authentication.
* Admin-only endpoints require the Admin role.
* Users can manage only their own tasks.

## Status

Completed and tested.
