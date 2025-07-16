begin transaction;
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Otepi', 'Lav A0', 'someday', 0);
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Sobinta', 'Irlet A0', 'someday', 1);
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Invera', 'Got B0', 'someday', 0);
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Sarved', 'Avribul C0', 'someday', 0);
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Oli', 'Aget C0', 'someday', 1);
insert into Person (Firstname, Surname, BirthDate, IsFemale) values ('Gelit', 'Avdi D0', 'someday', 1);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Lesiha', 'Ilet A1', 'someday', 0, 1, 2);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Ivdia', 'Din A1', 'someday', 1, 1, 2);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Ava', 'Itra A1', 'someday', 0, 1, 2);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Eki', 'Nel A1', 'someday', 1, 1, 2);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId) values ('Hunvediva', 'Sugor B1', 'someday', 0, 3);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId) values ('Lovdet', 'Gol B1', 'someday', 0, 3);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId) values ('Vur', 'Okut B1', 'someday', 1, 3);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Mavoli', 'Par C1', 'someday', 1, 4, 5);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Nit', 'Gul C1', 'someday', 1, 4, 5);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Verlet', 'Tehuti C1', 'someday', 0, 4, 5);
insert into Person (Firstname, Surname, BirthDate, IsFemale, MotherId) values ('Isod', 'Hupit D1', 'someday', 0, 6);
insert into Person (Firstname, Surname, BirthDate, IsFemale, MotherId) values ('Otra', 'Arlin D1', 'someday', 0, 6);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Ken', 'Vutil AD2', 'someday', 0, 17, 8);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Nud', 'Gir AD2', 'someday', 1, 18, 10);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Hiv', 'Gumavdi AD2', 'someday', 0, 17, 8);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Kira', 'Pohepirvia AD2', 'someday', 1, 18, 10);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Evdobi', 'Kud AD2', 'someday', 0, 17, 8);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Hutri', 'Ivdul AD2', 'someday', 0, 18, 10);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Pev', 'Istupa AD2', 'someday', 0, 17, 8);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Puntad', 'Kenuhad AD2', 'someday', 0, 18, 10);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Ervil', 'Adel BC2', 'someday', 1, 11, 14);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Irla', 'Irvesavi BC2', 'someday', 1, 11, 14);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Ivdol', 'Pin BC2', 'someday', 1, 12, 15);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Simurlad', 'Rut BC2', 'someday', 1, 16, 13);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Inurvia', 'Valuv BC2', 'someday', 0, 12, 15);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Nol', 'Dina BC2', 'someday', 0, 16, 13);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Arli', 'Robivda BC2', 'someday', 0, 11, 14);
insert into Person (Firstname, Surname, BirthDate, IsFemale, FatherId, MotherId) values ('Vit', 'Vosiv BC2', 'someday', 1, 11, 14);
insert into MaritalUnion (HusbandId, WifeId) values (17, 8);
insert into MaritalUnion (HusbandId, WifeId) values (18, 10);
insert into MaritalUnion (HusbandId, WifeId) values (11, 14);
insert into MaritalUnion (HusbandId, WifeId) values (12, 15);
insert into MaritalUnion (HusbandId, WifeId) values (16, 13);
insert into Company (PresidentId, Name) values (2, 'PermaCura');
insert into Company (PresidentId, Name) values (3, 'Strohmann Media');
insert into Company (PresidentId, Name) values (5, 'COB Genetics');
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (19, 1, 1, 3000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (20, 1, 0, 1000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (21, 1, 1, 1500);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (22, 2, 1, 2000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (23, 2, 1, 1000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (24, 3, 0, 2000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (25, 1, 1, 3000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (26, 1, 0, 1000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (27, 1, 1, 1500);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (28, 2, 1, 2000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (29, 2, 1, 1000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (30, 3, 0, 2000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (31, 1, 1, 3000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (32, 1, 0, 1000);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (33, 1, 1, 1500);
insert into Employment (PersonId, CompanyId, IsEmploymentContract, Salary) values (34, 2, 1, 2000);
commit;
-- A.
select
    G1.Firstname, G1.Surname,
    count(*) as GrandDaughters
from Person G1
join Person G2 on G2.FatherId = G1.Id or G2.Motherid = G1.Id
join Person G3 on G3.FatherId = G2.Id or G3.Motherid = G2.Id
where G3.IsFemale = true
group by G1.Id, G1.Firstname, G1.Surname
order by GrandDaughters desc
limit 1;

-- B.
select
    CompanyId,
    IsEmploymentContract,
    avg(Workers) as AverageWorkersPerCompany,
    avg(AverageSalary) as AverageSalaryPerCompany
from (
    select CompanyId,
             IsEmploymentContract,
             count(distinct PersonId) as Workers,
             avg(Salary)              as AverageSalary
    from Employment
    group by CompanyId, IsEmploymentContract
)
as CompanyStats
GROUP BY CompanyId, IsEmploymentContract;

-- C.
with FamilyMembers as (
    select
        MU.HusbandId as FamilyId,
        P.Id as PersonId
    from MaritalUnion MU
    join Person P on P.Id == MU.HusbandId or P.Id == MU.WifeId
    union
    select
        MU.HusbandId as FamilyId,
        P.Id as PersonId
    from MaritalUnion MU
    join Person C on C.FatherId = MU.HusbandId or C.MotherId = MU.WifeId
    join person P on P.Id = C.Id
),
Salaries as (
    select
        PersonId,
        sum(Salary) as TotalSalary
    from Employment
    group by PersonId
)
select
    FamilyMembers.FamilyId,
    coalesce(sum(S.TotalSalary), 0) as FamilyIncome,
    min(P2.Firstname) as FamilyFirstname,
    min(P2.Surname) as FamilyLastname
from FamilyMembers
left join Salaries S on S.PersonId = FamilyMembers.PersonId
join Person P2 on P2.Id = FamilyMembers.PersonId
group by FamilyMembers.FamilyId
order by FamilyIncome asc
limit 1;

