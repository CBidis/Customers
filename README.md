# Customers

The customers solution consists of the following projects,

<details>

### Customers.DAL

The data access layer is a .NET Standard 2.0 library that makes use of EF Core 2.1 using a code first approach in order to create the database through model - classes.

### Customers.DTOs

A .NET Standard 2.0 library that contains Data Transfer Objects that will be used for moving data between layers (DB to API).

### Customers.Services

A .NET Standard 2.0 library that encapsulates the DBContext class of the DAL in order to provide Database Operations upon the Customers table.
Also contains a custom defined exception and Profiles used by AutoMapper. (we could create a Customers.Common Library actually!)

### Customers.Api

A .NET Core 2.1 Web Api with a Controller(CustomersController) that servers the requests of GET, PUT, POST, DELETE for the basic operations upon the Customers Entity.

### Customers.Api.Tests

An xUNIT .NET Core 2.1 Test project with Test methods upon CustomersController that uses the In Memory Database of Entity Framework in order to provide an instance of the Database.

## NUGET Packages used:

* Entity Framework Core v2.1.4 
* Pomelo.EntityFrameworkCore.MySql v2.1.2 (MySQL provider for Entity Framework Core)
* AutoMapper v7.0.1 (A convention-based object-object mapper.)
* Swashbuckle.AspNetCore v3.0.0 (Swagger tools for documenting APIs built on ASP.NET Core)
* NLog.Web.AspNetCore v4.7.0 (Logging Support)

##

</details>

## How To Use/Debug/Test

For starters clone or download the source through https://github.com/CBidis/Customers.git

***Before opening the solution through VS 2017 you need to validate that you have at least version 15.7.5 and .NET Core 2.1 SDK!***

Upon completion of the prerequisites and opening the solution though visual studio before running the application you have to setup the connection string of MySQL Database, the connection string resides in appSettings.json file.

```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "MySQLConn": "server=localhost;database=CustomersDB;uid=root"
  },
  "AllowedHosts": "*"
}

```

After succesfully setting the connection string you have to apply the latest migration through the Package Manager console by running the command ***update-database*** with default project set to ***Customers.Api***.


In case you have problems by running the migrations or you have issues with MySQL Database you can always use the InMemory Database by changing the following line of the ***Startup.cs*** 

From:

```csharp
            services.AddDbContext<CustomerDBContext>(opt =>
                            opt.UseMySql(Configuration.GetConnectionString("MySQLConn")));
```

To:

```csharp
            services.AddDbContext<CustomerDBContext>(opt =>
                            opt.UseInMemoryDatabase("InMemoryCustomers"));
```
