# Customers

The customers solution consists of the following projects,

### Customers.DAL

The data access layer is a .NET Standard 2.0 library that makes use of EF Core 2.1 using a code first approach in order to create the database through model - classes.

### Customers.DTOs

A .NET Standard 2.0 library that contains Data Transfer Objects that will be used for moving data between layers (DB to API).

### Customers.Services

A .NET Standard 2.0 library that encapsulates the DBContext class of the DAL in order to provide Database Operations upon the Customers table.
Also contains a custom defined exception and Profiles used by AutoMapper (Hint we could them to Customers.Common Library actually!)

### Customers.Api

A .NET Core 2.1 Web Api with a Controller(CustomersController) that servers the requests of GET, PUT, POST, DELETE for the basic operations upon the Customers Entity.

The start up page of the API is a Swagger API Page.

### Customers.Api.Tests

An xUNIT .NET Core 2.1 Test project with Test methods upon CustomersController that uses the In Memory Database of Entity Framework in order to provide an instance of the Database.

## NUGET Packages used:

* Entity Framework Core v2.1.4 
* Pomelo.EntityFrameworkCore.MySql v2.1.2 (MySQL provider for Entity Framework Core)
* AutoMapper v7.0.1 (A convention-based object-object mapper.)
* Swashbuckle.AspNetCore v3.0.0 (Swagger tools for documenting APIs built on ASP.NET Core)
* NLog.Web.AspNetCore v4.7.0 (Logging Support)

##
