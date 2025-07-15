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

The backend expects a SQLite database at `./backend/NetPCTest.Backend/SQLLiteDatabase.db`. There is an SQL script to create tables and insert sample data under `./backend/NetPCTest.Backend/initDB.sql`. 

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

## API Rundown
Authorization is done using Bearer tokens.

### GET /api/auth

Returns the ID and email of the currently logged in user. Requires authorization.

#### Returns
```
{
  "id": ID,
  "email": Email
}
```


### POST /api/auth

Attempts to retrieve a Bearer token using the specified credentials.

#### Parameters
 - Body
    ```
    {
      "email": Contact email,
      "password": Contact password
    }
    ```

#### Returns
```
{
  "token": Bearer token
}
```


### GET /api/categories/count

Retrieves the count of categories.

#### Returns
```
{
  "count": Number of categories
}
```


### GET /api/categories

Retrieves all categories.

#### Returns
```
{
  {
    "id": Category ID,
    "name": Category Name,
    "customSubcategoryRequired": Whether the category requires a custom subcategory,
    "subCategories": [
      {
        "id": Subcategory ID,
        "name": Subcategory Name,
        "categoryId": Parent category ID
      },
      [...]
    ]
  },
  [...]
}
```


### GET /api/contacts/count

Retrieves the count of contacts.

#### Returns
```
{
  "count": Count of contacts
}
```


### GET /api/contacts?startIndex=*number*&count=*number*

Retrieves the specified range of contacts.

#### Parameters
 - startIndex
   - The zero-based index of the first contact to retrieve.
 - count
   - The number of contacts to retrieve.

#### Returns
```
[
  {
    "id": Contact ID,
    "name": Contact Name,
    "surname": Contact Surname
  },
  [...]
]
```

### POST /api/contacts

Creates a new contact. Requires authorization.

#### Parameters
```
{
  "name": Name,
  "surname": Surname,
  "email": Email,
  "password": Password,
  "confirmPassword": Confirmed password,
  "phone": Phone number,
  "birthDate": Birth date,
  "categoryId": ID of the category the new contact belongs to,
  "subCategoryId": Nullable, ID of the subcategory the new contact belongs to,
  "customSubCategory": Nullable, the custom subcategory the new contact belongs to
}
```
#### Returns
Found, with the target URL being the newly created contact.


### GET /api/contacts/*id*

Retrieves the details of a contact.

#### Parameters
 - id
   - ID of the contact to retrieve the details of.
#### Returns
```
{
  "id": Contact ID,
  "name": Contact name,
  "surname": Contact surname,
  "email": Contact email,
  "phone": Contact phone number,
  "birthDate": Contact birth date,
  "categoryId": Contact category ID,
  "subCategoryId": Nullable, contact subcategory ID,
  "customSubCategory": Nullable, custom subcategory name
}
```


### PUT /api/contacts/*id*

Modifies the details of a contact. This endpoint does not change the password of a contact. Requires authorization.

#### Parameters
 - id
   - ID of the contact to modify.
 - body
   ```
   {
     "name": New contact name,
     "surname": New contact surname,
     "email": New contact email,
     "phone": New contact phone,
     "birthDate": New contact birth date,
     "categoryId": New contact category ID,
     "subCategoryId": Nullable, new contact subcategory ID,
     "customSubCategory": Nullable, new custom subcategory name
   }
   ```


### DELETE /api/contacts/*id*

Deletes a contact. Requires authorization.

#### Parameters
 - id
   - ID of the contact to delete.


### PUT /api/contacts/*id*/password

Changes the password of a contact. Requires authorization.

#### Parameters
 - id
   - ID of the contact change the password of.
 - body
   ```
   {
     "password": New password,
     "confirmPassword": Confirmed password
   }
   ```


### GET /api/localisation

Retrieves all available locales.

#### Returns
```
[
  Locale string,
  [...]
]
```


### GET /api/localisation/*locale*

Retrieves the dictionary of translations belonging the specified locale.

#### Parameters
 - locale
   - Name of the locale to retrieve the translations of.

#### Returns
```
{
  key: Value,
  [...]
}
```

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

## Important components

### NetPCTest.Frontend.Components.ContactList
 - Displays the list of contacts and allows for viewing of their details. Allows for modification of data when the user is logged in.

### NetPCTest.Frontend.Components.L
 - Provides a way of dynamically translating strings using ILocalisationService.

### NetPCTest.Frontend.Components.Modal
 - Provides a basic extendable modal.

### NetPCTest.Frontend.Components.NavBar
 - Provides a navbar with login/logout functionality.

## Important classes

### NetPCTest.Frontend.Services.IAuthService
Defines an abstraction of authentication.

 - #### AuthStateChangedAsync
   - An event raised on every authentication state change.

 - #### Login(**string** *email*, **string** *password*)
   - Retrieves a Bearer token using the specified contact credentials asynchronously.
   - ##### Parameters
     - *email* - Contact email.
     - *password* - Contact password.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.

 - #### Logout
   - Discards the current Bearer token asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation.

 - #### GetBearer
   - Retrieves the current Bearer token asynchronously.
   - ##### Returns
     - The Bearer token, or null if none is present.

 - #### IsLoggedIn
   - Checks whether a Bearer token is stored asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.

 - #### GetEmail
   - Retrieves the email stored in the currently held Bearer token asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.String**.

 - #### GetUser
   - Retrieves the email stored in the currently held Bearer token asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Security.Claims.ClaimsPrincipal**.


### NetPCTest.Frontend.Services.ICategoriesService
Defines an abstraction for accessing category data.

 - #### RefreshCategoriesAsync
   - Refreshes the list of categories from the data source.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation.

 - #### GetCategory(**int** *id*)
   - Retrieves a category with the specified ID from the local in-memory cache.
   - ##### Parameters
     - *id* - The ID of the category to retrieve.
   - ##### Returns
     - The matching **NetPCTest.Frontend.Dtos.CategoryDto**, or null if not found.

 - #### GetSubCategory(**int** *id*)
   - Retrieves a subcategory with the specified ID from the local in-memory cache.
   - ##### Parameters
     - *id* - The ID of the subcategory to retrieve.
   - ##### Returns
     - The matching **NetPCTest.Frontend.Dtos.SubCategoryDto**, or null if not found.

 - #### GetCategories
   - Retrieves a list of categories from the local in-memory cache.
   - ##### Returns
     - A read only list containing **NetPCTest.Frontend.Dtos.CategoryDto**.


### NetPCTest.Frontend.Services.IContactsService
Defines an abstraction of accessing contact data.

 - #### GetContactCountAsync
   - Retrieves the count of contacts asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation. The result of the task is the count of contacts.

 - #### GetContactAsync(**int** *id*)
   - Retrieves contact details asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to retrieve the data of.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation. The result of the task is the count of contacts.

 - #### GetContactsAsync(**int** *start*, **int** *count*)
   - Retrieves a range of contacts asynchronously.
   - ##### Parameters
     - *start* - The zero-based index of the first contact to retrieve.
     - *count* - The number of contacts to retrieve.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a list of
            **NetPCTest.Frontend.Dtos.ContactBriefDto**

 - #### CreateContactAsync(**NetPCTest.Frontend.Dtos.ContactCreationDto** *newData*)
   - Creates a contact asynchronously.
   - ##### Parameters
     - *newData* - A **NetPCTest.Frontend.Dtos.ContactCreationDto** representing new contact data.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.

 - #### UpdateContactAsync(**int** *id*, **NetPCTest.Frontend.Dtos.ContactUpdateDto** *newData*)
   - Updates a contact asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to modify.
     - *newData* - **NetPCTest.Frontend.Dtos.ContactUpdateDto** containing new contact data.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation, with a result containing a
            **System.Boolean**.

 - #### UpdateContactPasswordAsync(**int** *id*, **NetPCTest.Frontend.Dtos.ContactPasswordChangeDto** *newPassword*)
   - Sets the password of a contact asynchronously.
   - ##### Parameters
     - *id* - The ID of the contact to modify.
     - *newPassword* - **NetPCTest.Frontend.Dtos.ContactPasswordChangeDto** containing the new password.
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


### NetPCTest.Frontend.Services.ILocalisationService
Provides a way to retrieve localised strings.

 - #### RefreshAvailableLocalesAsync
   - Refreshes available locales asynchronously.
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation.

 - #### SetLocaleAsync(**string** *localeName*)
   - Changes the current locale and requests it's localisation dictionary.
   - ##### Parameters
     - *localeName* - The name of the locale to change to
   - ##### Returns
     - **System.Threading.Tasks.Task** representing the asynchronous operation.

 - #### Translate(**string** *key*)
   - Localises a string.
   - ##### Parameters
     - *key* - Key name to retrieve the localisation of.
   - ##### Returns
     - Localised string, or key on failure.
