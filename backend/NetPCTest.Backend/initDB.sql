drop table if exists 'Categories';
drop table if exists 'SubCategories';
drop table if exists 'Contacts';
drop table if exists 'LocaleKeyStrings';
drop table if exists 'Locales';

BEGIN TRANSACTION;
CREATE TABLE "Categories" (
                              "Id" INTEGER NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY AUTOINCREMENT,
                              "Name" TEXT NOT NULL,
                              "CustomSubcategoryRequired" INTEGER NOT NULL
);

CREATE TABLE "Locales" (
                           "Id" INTEGER NOT NULL CONSTRAINT "PK_Locales" PRIMARY KEY AUTOINCREMENT,
                           "Name" TEXT NOT NULL
);

CREATE TABLE "SubCategories" (
                                 "Id" INTEGER NOT NULL CONSTRAINT "PK_SubCategories" PRIMARY KEY AUTOINCREMENT,
                                 "Name" TEXT NOT NULL,
                                 "CategoryId" INTEGER NOT NULL,
                                 CONSTRAINT "FK_SubCategories_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE
);

CREATE TABLE "LocaleKeyStrings" (
                                    "Key" TEXT NOT NULL,
                                    "LocaleId" INTEGER NOT NULL,
                                    "Value" TEXT NOT NULL,
                                    CONSTRAINT "PK_LocaleKeyStrings" PRIMARY KEY ("Key", "LocaleId"),
                                    CONSTRAINT "FK_LocaleKeyStrings_Locales_LocaleId" FOREIGN KEY ("LocaleId") REFERENCES "Locales" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Contacts" (
                            "Id" INTEGER NOT NULL CONSTRAINT "PK_Contacts" PRIMARY KEY AUTOINCREMENT,
                            "Name" TEXT NOT NULL,
                            "Surname" TEXT NOT NULL,
                            "Email" TEXT NOT NULL,
                            "PasswordHash" TEXT NOT NULL,
                            "Phone" TEXT NOT NULL,
                            "BirthDate" TEXT NOT NULL,
                            "CategoryId" INTEGER NOT NULL,
                            "SubCategoryId" INTEGER NULL,
                            "CustomSubCategory" TEXT NULL,
                            CONSTRAINT "FK_Contacts_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
                            CONSTRAINT "FK_Contacts_SubCategories_SubCategoryId" FOREIGN KEY ("SubCategoryId") REFERENCES "SubCategories" ("Id")
);

CREATE INDEX "IX_Contacts_CategoryId" ON "Contacts" ("CategoryId");

CREATE UNIQUE INDEX "IX_Contacts_Email" ON "Contacts" ("Email");

CREATE INDEX "IX_Contacts_SubCategoryId" ON "Contacts" ("SubCategoryId");

CREATE INDEX "IX_LocaleKeyStrings_LocaleId" ON "LocaleKeyStrings" ("LocaleId");

CREATE UNIQUE INDEX "IX_Locales_Name" ON "Locales" ("Name");

CREATE INDEX "IX_SubCategories_CategoryId" ON "SubCategories" ("CategoryId");

COMMIT;

-- Base categories
insert into 'Categories' (Name, CustomSubcategoryRequired) 
values ('category.other', true),
       ('category.work', false),
       ('category.personal', false);

-- Base subcategories
insert into 'SubCategories' (Name, CategoryId)
values ('subcategory.manager', 2),
       ('subcategory.employee', 2),
       ('subcategory.consultant', 2),
       ('subcategory.client', 2);

-- Base user to get our foot in when testing
insert into Contacts (Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory) 
    values (
            'Giga', 
            'Tester', 
            'tester@test.edu',
            'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
            112,
            '2001-07-13 00:00:00',
            1,
            'Fajny tester.'        
        );

-- Localisation
insert into Locales (Name) values ('pl');
insert into Locales (Name) values ('en');

insert into LocaleKeyStrings (LocaleId, Key, Value) 
values (1, 'title', 'Frontend Aplikacji Rekrutacyjnej NetPC'),
       (2, 'title', 'NetPC Recruitment Application Frontend');
insert into LocaleKeyStrings (LocaleId, Key, Value)
values (1, 'category.other', 'Inny'),
       (2, 'category.other', 'Other'),
       (1, 'category.work', 'Służbowy'),
       (2, 'category.work', 'Work'),
       (1, 'category.personal', 'Prywatny'),
       (2, 'category.personal', 'Personal');
insert into LocaleKeyStrings (LocaleId, Key, Value)
values (1, 'subcategory.manager', 'Menedżer'),
       (2, 'subcategory.manager', 'Manager'),
       (1, 'subcategory.employee', 'Pracownik'),
       (2, 'subcategory.employee', 'Employee'),
       (1, 'subcategory.consultant', 'Konsultant'),
       (2, 'subcategory.consultant', 'Consultant'),
       (1, 'subcategory.client', 'Klient'),
       (2, 'subcategory.client', 'Client');
insert into LocaleKeyStrings (LocaleId, Key, Value)
values (1, 'ui.login', 'Logowanie'),
       (2, 'ui.login', 'Login'),
       (1, 'ui.login.email', 'Email'),
       (2, 'ui.login.email', 'Email'),
       (1, 'ui.login.password', 'Hasło'),
       (2, 'ui.login.password', 'Password'),
       (1, 'ui.login.submit', 'Zaloguj się'),
       (2, 'ui.login.submit', 'Login'),
       (1, 'ui.contact.edit', 'Edycja kontaktu'),
       (2, 'ui.contact.edit', 'Contact edit');
