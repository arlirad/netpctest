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
       (2, 'ui.contact.edit', 'Contact edit'),
       (1, 'ui.list.contact.id', 'ID'),
       (2, 'ui.list.contact.id', 'ID'),
       (1, 'ui.list.contact.name', 'Imię'),
       (2, 'ui.list.contact.name', 'Firstname'),
       (1, 'ui.list.contact.surname', 'Nazwisko'),
       (2, 'ui.list.contact.surname', 'Surname');

-- Sample contact data
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ubi',
           'Kiv',
           'ubi.kiv@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           548174073,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Onva',
           'Irod',
           'onva.irod@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           990920573,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Torli',
           'Narvia',
           'torli.narvia@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           544160417,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ostar',
           'Apan',
           'ostar.apan@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           448166582,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Uhia',
           'Emupit',
           'uhia.emupit@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           366585133,
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
           500887762,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Itul',
           'Eter',
           'itul.eter@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           730654643,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Eketri',
           'Tivrel',
           'eketri.tivrel@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           365603511,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Nuvdi',
           'Montal',
           'nuvdi.montal@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           730210460,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Sev',
           'Med',
           'sev.med@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           659402245,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nusi',
           'Gin',
           'nusi.gin@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           802103915,
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
           818668767,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ati',
           'Ater',
           'ati.ater@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           970897760,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ola',
           'Udur',
           'ola.udur@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           127854084,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Lesapa',
           'Tar',
           'lesapa.tar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           208694741,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Otenvi',
           'Ret',
           'otenvi.ret@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           360835977,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Savra',
           'Nustun',
           'savra.nustun@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           922237473,
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
           232058044,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Dav',
           'Uvrupa',
           'dav.uvrupa@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           146332560,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Etrud',
           'Goba',
           'etrud.goba@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           634808869,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Onvet',
           'Pestud',
           'onvet.pestud@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           841438326,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Kopa',
           'Ental',
           'kopa.ental@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           596094862,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Niva',
           'Arvi',
           'niva.arvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           621436422,
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
           224743424,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Agevrel',
           'Oki',
           'agevrel.oki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           581287410,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ega',
           'Hev',
           'ega.hev@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           307888216,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Mugia',
           'Usoki',
           'mugia.usoki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           699712354,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Oriba',
           'Lutri',
           'oriba.lutri@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           817938026,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Uki',
           'Lenved',
           'uki.lenved@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           733290221,
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
           310146523,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Povdir',
           'Luti',
           'povdir.luti@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           397768890,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Vun',
           'Onvi',
           'vun.onvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           438534371,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Nit',
           'Pet',
           'nit.pet@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           272285724,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Rugi',
           'Mastekonvi',
           'rugi.mastekonvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           936269670,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Usutra',
           'Asti',
           'usutra.asti@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           388530551,
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
           117322238,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Etrapar',
           'Nirul',
           'etrapar.nirul@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           914391391,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Pil',
           'Ruhovdi',
           'pil.ruhovdi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           189782867,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Ilet',
           'Evrad',
           'ilet.evrad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           188554296,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Hubi',
           'Mabar',
           'hubi.mabar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           965776942,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Girla',
           'Ril',
           'girla.ril@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           623758158,
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
           813660925,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Lugia',
           'Anvet',
           'lugia.anvet@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           649484564,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ovra',
           'Hestun',
           'ovra.hestun@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           469307570,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Gasiv',
           'Ivrut',
           'gasiv.ivrut@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           544341340,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Uber',
           'Usad',
           'uber.usad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           558237437,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Utal',
           'Hir',
           'utal.hir@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           882467931,
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
           651021031,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Loda',
           'Mor',
           'loda.mor@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           117111069,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Nerlod',
           'Anekir',
           'nerlod.anekir@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           782291861,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Usa',
           'Savdi',
           'usa.savdi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           628980734,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Dakia',
           'Uketreka',
           'dakia.uketreka@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           632813292,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Upol',
           'Orvi',
           'upol.orvi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           558250752,
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
           894653437,
           '2001-07-13 00:00:00',
           2,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Unti',
           'Giv',
           'unti.giv@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           987135142,
           '2001-07-13 00:00:00',
           0,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Rul',
           'Moki',
           'rul.moki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           955153508,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Silenut',
           'Uhi',
           'silenut.uhi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           368271375,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Orvabar',
           'Elada',
           'orvabar.elada@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           901711482,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ohit',
           'Unil',
           'ohit.unil@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           641514671,
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
           532821652,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Erviha',
           'Okin',
           'erviha.okin@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           870850309,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Ivren',
           'Hovrubil',
           'ivren.hovrubil@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           245122772,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Envapa',
           'Gur',
           'envapa.gur@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           506775313,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Tovdin',
           'Supiha',
           'tovdin.supiha@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           574581965,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Epi',
           'Onar',
           'epi.onar@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           852372289,
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
           438445198,
           '2001-07-13 00:00:00',
           2,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Nuvi',
           'Verlagot',
           'nuvi.verlagot@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           127256823,
           '2001-07-13 00:00:00',
           0,
           4
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Maved',
           'Tasia',
           'maved.tasia@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           914904663,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Hegola',
           'Nuhi',
           'hegola.nuhi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           634169303,
           '2001-07-13 00:00:00',
           2,
           3
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Guna',
           'Intad',
           'guna.intad@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           211832474,
           '2001-07-13 00:00:00',
           0,
           1
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Gev',
           'Eki',
           'gev.eki@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           903712967,
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
           945730404,
           '2001-07-13 00:00:00',
           2,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Uvduri',
           'Dan',
           'uvduri.dan@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           249326076,
           '2001-07-13 00:00:00',
           0,
           2
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, CustomSubCategory)
values (
           'Muginver',
           'Otrehi',
           'muginver.otrehi@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           392012765,
           '2001-07-13 00:00:00',
           1,
           'Lorem ipsum dolor sit amet.'
       );
insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, SubCategoryId)
values (
           'Revdesi',
           'Urveha',
           'revdesi.urveha@test.edu',
           'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
           364372021,
           '2001-07-13 00:00:00',
           2,
           4
       );
