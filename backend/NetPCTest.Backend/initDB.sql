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
       (1, 'ui.logout', 'Wyloguj się'),
       (2, 'ui.logout', 'Logout'),
       (1, 'ui.login.email', 'Email'),
       (2, 'ui.login.email', 'Email'),
       (1, 'ui.login.password', 'Hasło'),
       (2, 'ui.login.password', 'Password'),
       (1, 'ui.login.submit', 'Zaloguj się'),
       (2, 'ui.login.submit', 'Login'),
       (1, 'ui.contact.edit', 'Edycja kontaktu'),
       (2, 'ui.contact.edit', 'Contact edit'),
       (1, 'ui.contact.details', 'Szczegóły kontaktu'),
       (2, 'ui.contact.details', 'Contact details'),
       (1, 'ui.contact.details.name', 'Imię'),
       (2, 'ui.contact.details.name', 'Name'),
       (1, 'ui.contact.details.surname', 'Nazwisko'),
       (2, 'ui.contact.details.surname', 'Surname'),
       (1, 'ui.contact.details.email', 'Email'),
       (2, 'ui.contact.details.email', 'Email'),
       (1, 'ui.contact.details.phone', 'Numer telefonu'),
       (2, 'ui.contact.details.phone', 'Phone number'),
       (1, 'ui.contact.details.birthdate', 'Data urodzenia'),
       (2, 'ui.contact.details.birthdate', 'Birth date'),
       (1, 'ui.contact.details.category', 'Kategoria'),
       (2, 'ui.contact.details.category', 'Category'),
       (1, 'ui.contact.details.subcategory', 'Podkategoria'),
       (2, 'ui.contact.details.subcategory', 'Subcategory'),
       (1, 'ui.contact.details.custom_subcategory', 'Własna podkategoria'),
       (2, 'ui.contact.details.custom_subcategory', 'Custom subcategory'),
       (1, 'ui.contact.details.edit_login_required', 'Do edycji wymagane jest zalogowanie się.'),
       (2, 'ui.contact.details.edit_login_required', 'You need to be logged in in order to edit.'),
       (1, 'ui.contact.details.delete', 'Usuń'),
       (2, 'ui.contact.details.delete', 'Delete'),
       (1, 'ui.contact.details.edit', 'Edytuj'),
       (2, 'ui.contact.details.edit', 'Edit'),
       (1, 'ui.contact.edit.password.change', 'Zmień hasło'),
       (2, 'ui.contact.edit.password.change', 'Change password'),
       (1, 'ui.contact.edit.password', 'Hasło'),
       (2, 'ui.contact.edit.password', 'Password'),
       (1, 'ui.contact.edit.password.confirm', 'Potwierdź hasło'),
       (2, 'ui.contact.edit.password.confirm', 'Confirm password'),
       (1, 'ui.contact.edit.submit', 'Potwierdź'),
       (2, 'ui.contact.edit.submit', 'Confirm'),
       (1, 'ui.contact.delete', 'Usuń kontakt'),
       (2, 'ui.contact.delete', 'Delete contact'),
       (1, 'ui.contact.delete.yes', 'Potwierdź'),
       (2, 'ui.contact.delete.yes', 'Confirm'),
       (1, 'ui.contact.delete.no', 'Anuluj'),
       (2, 'ui.contact.delete.no', 'Cancel'),
       (1, 'ui.list.contact.id', 'ID'),
       (2, 'ui.list.contact.id', 'ID'),
       (1, 'ui.list.contact.name', 'Imię'),
       (2, 'ui.list.contact.name', 'Firstname'),
       (1, 'ui.list.contact.surname', 'Nazwisko'),
       (2, 'ui.list.contact.surname', 'Surname');

-- Sample contact data
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ubi',
           'Kiv',
           'ubi.kiv@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           379294294,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Onva',
           'Irod',
           'onva.irod@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           808506671,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Torli',
           'Narvia',
           'torli.narvia@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           518369885,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ostar',
           'Apan',
           'ostar.apan@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           439845936,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Uhia',
           'Emupit',
           'uhia.emupit@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           579800890,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Harleter',
           'Kalia',
           'harleter.kalia@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           303158828,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Itul',
           'Eter',
           'itul.eter@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           567264691,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Eketri',
           'Tivrel',
           'eketri.tivrel@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           569613673,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nuvdi',
           'Montal',
           'nuvdi.montal@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           160543382,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Sev',
           'Med',
           'sev.med@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           980686425,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nusi',
           'Gin',
           'nusi.gin@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           778643908,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Distun',
           'Orvi',
           'distun.orvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           938592570,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ati',
           'Ater',
           'ati.ater@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           351029598,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ola',
           'Udur',
           'ola.udur@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           843379211,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Lesapa',
           'Tar',
           'lesapa.tar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           918333184,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Otenvi',
           'Ret',
           'otenvi.ret@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           587988467,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Savra',
           'Nustun',
           'savra.nustun@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           385004690,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ivra',
           'Ovdi',
           'ivra.ovdi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           619171601,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Dav',
           'Uvrupa',
           'dav.uvrupa@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           293923524,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Etrud',
           'Goba',
           'etrud.goba@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           802817090,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Onvet',
           'Pestud',
           'onvet.pestud@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           705789552,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Kopa',
           'Ental',
           'kopa.ental@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           513983346,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Niva',
           'Arvi',
           'niva.arvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           298379167,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Verla',
           'Pihorud',
           'verla.pihorud@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           803957899,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Agevrel',
           'Oki',
           'agevrel.oki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           245211636,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ega',
           'Hev',
           'ega.hev@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           771069188,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Mugia',
           'Usoki',
           'mugia.usoki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           825055327,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Oriba',
           'Lutri',
           'oriba.lutri@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           508082409,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Uki',
           'Lenved',
           'uki.lenved@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           625002961,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Led',
           'Dir',
           'led.dir@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           132614882,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Povdir',
           'Luti',
           'povdir.luti@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           806928058,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Vun',
           'Onvi',
           'vun.onvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           542407000,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nit',
           'Pet',
           'nit.pet@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           258075356,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Rugi',
           'Mastekonvi',
           'rugi.mastekonvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           242671967,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Usutra',
           'Asti',
           'usutra.asti@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           947845766,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Imarvud',
           'Lovden',
           'imarvud.lovden@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           314634254,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Etrapar',
           'Nirul',
           'etrapar.nirul@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           247127532,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Pil',
           'Ruhovdi',
           'pil.ruhovdi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           959797101,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ilet',
           'Evrad',
           'ilet.evrad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           727089682,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Hubi',
           'Mabar',
           'hubi.mabar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           985544025,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Girla',
           'Ril',
           'girla.ril@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           425465148,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ner',
           'Atapi',
           'ner.atapi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           591705974,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Lugia',
           'Anvet',
           'lugia.anvet@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           759114371,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ovra',
           'Hestun',
           'ovra.hestun@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           491853793,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Gasiv',
           'Ivrut',
           'gasiv.ivrut@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           578190874,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Uber',
           'Usad',
           'uber.usad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           445195954,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Utal',
           'Hir',
           'utal.hir@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           332369066,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ika',
           'Anter',
           'ika.anter@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           765422850,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Loda',
           'Mor',
           'loda.mor@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           548682908,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Nerlod',
           'Anekir',
           'nerlod.anekir@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           993571873,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Usa',
           'Savdi',
           'usa.savdi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           788638519,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Dakia',
           'Uketreka',
           'dakia.uketreka@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           908961530,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Upol',
           'Orvi',
           'upol.orvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           419603287,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Put',
           'Nel',
           'put.nel@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           445734664,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Unti',
           'Giv',
           'unti.giv@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           482425758,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Rul',
           'Moki',
           'rul.moki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           217164454,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Silenut',
           'Uhi',
           'silenut.uhi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           420648176,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Orvabar',
           'Elada',
           'orvabar.elada@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           200184881,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ohit',
           'Unil',
           'ohit.unil@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           736051299,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Segia',
           'Ihar',
           'segia.ihar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           534436984,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Erviha',
           'Okin',
           'erviha.okin@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           853281028,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ivren',
           'Hovrubil',
           'ivren.hovrubil@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           360075312,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Envapa',
           'Gur',
           'envapa.gur@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           870026850,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Tovdin',
           'Supiha',
           'tovdin.supiha@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           462831823,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Epi',
           'Onar',
           'epi.onar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           966147174,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ohia',
           'Gapa',
           'ohia.gapa@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           904031723,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nuvi',
           'Verlagot',
           'nuvi.verlagot@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           916452590,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Maved',
           'Tasia',
           'maved.tasia@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           599361934,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Hegola',
           'Nuhi',
           'hegola.nuhi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           453472746,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Guna',
           'Intad',
           'guna.intad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           135188885,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Gev',
           'Eki',
           'gev.eki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           336713734,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Tukirvia',
           'Ken',
           'tukirvia.ken@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           609381943,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Uvduri',
           'Dan',
           'uvduri.dan@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           856419352,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Muginver',
           'Otrehi',
           'muginver.otrehi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           719204616,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Revdesi',
           'Urveha',
           'revdesi.urveha@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           898316659,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
