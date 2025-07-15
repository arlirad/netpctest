# netpctest

This app is split across two solutions, one for the backend and one for the frontend. The code is commented with `/// <summary>` pretty much everywhere, so this specification will only go the most important classes and methods.

The app was developed with:
 - Arch Linux
 - Rider 2025.1.3
 - .NET SDK 9.0.302


# Backend

The backend is in the `backend/` directory.

 - Build: `dotnet build`
 - Run: `dotnet run --project NetPCTest.Backend`

NUnit tests are under the *NetPCTest.Backend.Tests* project.

Used NuGet packages:
 - AutoMapper*
 - coverlet.collector
 - Microsoft.AspNetCore.Authentication.JwtBearer
 - Microsoft.AspNetCore.OpenApi
 - Microsoft.EntityFrameworkCore
 - Microsoft.EntityFrameworkCore.Design
 - Microsoft.EntityFrameworkCore.Sqlite
 - Microsoft.NET.Test.Sdk
 - NUnit
 - NUnit.Analyzers
 - NUnit3TestAdapter
 - Microsoft.AspNetCore.Identity
 - Microsoft.AspNetCore.RateLimiting
 - Moq
 - Swashbuckle.AspNetCore
 - Swashbuckle.AspNetCore.Swagger
 - Swashbuckle.AspNetCore.SwaggerUI
 - System.IdentityModel.TokensJwt
 
*\*AutoMapper is explicitly downgraded to 12.0.1 in order to avoid licensing quirkyness.*

## Important classes


# Frontend

The frontend is in the `frontend/` directory.

 - Build: `dotnet build`
 - Run: `dotnet run --project NetPCTest.Frontend`
 
Used NuGet packages:
 - AutoMapper*
 - Blazored.LocalStorage
 - Microsoft.AspNetCore.Components.Authorization
 - Microsoft.AspNetCore.Components.WebAssembly
 - Microsoft.AspNetCore.Components.WebAssembly.DevServer
 - Microsoft.Extensions.Http
 - Microsoft.Extensions.Options.ConfigurationExtensions
 - Microsoft.NET.ILLink.Tasks
 - Microsoft.NET.Sdk.WebAssembly.Pack
 - System.IdentityModel.TokensJwt
 
*\*AutoMapper is explicitly downgraded to 12.0.1 in order to avoid licensing quirkyness.*

## Important classes
