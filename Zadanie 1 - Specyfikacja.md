# netpctest

This app is split across two solutions, one for the backend and one for the frontend. The code is commented with `/// <summary>` pretty much everywhere, so this specification will only go the most important classes and methods in order to keep this short.

The app was developed with:
 - Arch Linux
 - Rider 2025.1.3
 - .NET SDK 9.0.302


# Backend

The backend is in the `./backend/` directory.

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

### NetPCTest.Backend.Services.ICategoriesService
Provides an abstraction for accessing **NetPCTest.Backend.Models.Category** entities.

 - #### GetCategoryCountAsync(**System.Threading.CancellationToken** *cancellationToken*)
   - Returns the count of categories asynchronously.
   - ##### Parameters
     - *cancellationToken* - A **System.Threading.CancellationToken** that can be used to cancel the operation.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation. The result of the task is the count of categories.

 - #### GetCategoriesAsync(**System.Threading.CancellationToken** *cancellationToken*)
   - Returns all categories asynchronously.
   - ##### Parameters
     - *cancellationToken* - A **System.Threading.CancellationToken** that can be used to cancel the operation.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation. The result of the task is a list of categories.


### NetPCTest.Backend.Services.IContactsService
Provides an abstraction for creating, reading, updating and deleting contacts.

 - #### GetContactCountAsync(**System.Threading.CancellationToken** *cancellationToken*)
   - Retrieves the count of contacts asynchronously.
   - ##### Parameters
     - *cancellationToken* - A **System.Threading.CancellationToken** that can be used to cancel the operation.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing the count of
            contacts.

 - #### GetContactsAsync(**int** *startIndex*, **int** *count*, **System.Threading.CancellationToken** *cancellationToken*)
   - Retrieves a range of contacts asynchronously.
   - ##### Parameters
     - *startIndex* - The zero-based index of the first contact to retrieve.
     - *count* - The number of contacts to retrieve.
     - *cancellationToken* - A **System.Threading.CancellationToken** that can be used to cancel the operation.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a list of
            **NetPCTest.Backend.Dtos.ContactBriefDto**.

 - #### CreateContactAsync(**NetPCTest.Backend.Dtos.ContactCreationDto** *contactCreationDto*)
   - Creates a contact asynchronously.
   - ##### Parameters
     - *contactCreationDto* - A **NetPCTest.Backend.Dtos.ContactCreationDto** representing new contact data.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **NetPCTest.Backend.Results.CreateContactResult**.

 - #### GetContactAsync(**int** *id*, **System.Threading.CancellationToken** *cancellationToken*)
   - Retrieves contact details asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to retrieve the data of.
     - *cancellationToken* - A **System.Threading.CancellationToken** that can be used to cancel the operation.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **NetPCTest.Backend.Results.CreateContactResult**.

 - #### UpdateContactAsync(**int** *id*, **NetPCTest.Backend.Dtos.ContactUpdateDto** *newData*)
   - Updates a contact asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to modify.
     - *newData* - **NetPCTest.Backend.Dtos.ContactUpdateDto** containing new contact data.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **NetPCTest.Backend.Results.UpdateContactResult**.

 - #### SetContactPasswordAsync(**int** *id*, **NetPCTest.Backend.Dtos.ContactPasswordChangeDto** *newPassword*)
   - Sets the password of a contact asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to modify.
     - *newPassword* - **NetPCTest.Backend.Dtos.ContactPasswordChangeDto** containing the new password.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.

 - #### DeleteContactAsync(**int** *id*)
   - Deletes a contact asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to delete.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.


### NetPCTest.Backend.Services.ILocalisationService
Provides an abstraction of locale-related methods.

 - #### GetAllLocales
   - Returns all available locales.
   - ##### Returns
     - A list of available locales.

 - #### GetLocaleKeyStrings(**string** *locale*)
   - Returns all KeyStrings present in a locale.
   - ##### Parameters
     - *locale* - Locale name.
   - ##### Returns
     - Dictionary of translations specific to the specified locale.


### NetPCTest.Backend.Services.IPasswordService
Defines methods for securely hashing and verifying passwords.

 - #### HashPassword(**NetPCTest.Backend.Models.Contact** *contact*, **string** *password*)
   - Hashes a password.
   - ##### Parameters
     - *contact* - **NetPCTest.Backend.Models.Contact** for which the password is intended.
     - *password* - Plaintext password
   - ##### Returns
     - A **System.String** containing the hashed password.

 - #### ComparePassword(**NetPCTest.Backend.Models.Contact** *contact*, **string** *hashedPassword*, **string** *providedPlainPassword*)
   - Verifies a password.
   - ##### Parameters
     - *contact* - **NetPCTest.Backend.Models.Contact** for which the comparison is being done.
     - *hashedPassword* - Hashed password.
     - *providedPlainPassword* - Plaintext password.
   - ##### Returns
     - A **Microsoft.AspNetCore.Identity.PasswordVerificationResult** of the comparison.



# Frontend

The frontend is in the `./frontend/` directory.

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
