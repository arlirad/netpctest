#!/usr/bin/python

names = [
    "Otepi", "Lav", "Sobinta", "Irlet", "Invera", "Got", "Sarved", "Avribul",
    "Oli", "Aget", "Gelit", "Avdi", "Lesiha", "Ilet", "Ivdia", "Din", "Ava",
    "Itra", "Eki", "Nel", "Hunvediva", "Sugor", "Lovdet", "Gol", "Vur", "Okut",
    "Mavoli", "Par", "Nit", "Gul", "Verlet", "Tehuti", "Isod", "Hupit", "Otra",
    "Arlin", "Ken", "Vutil", "Nud", "Gir", "Hiv", "Gumavdi", "Kira", "Pohepirvia",
    "Evdobi", "Kud", "Hutri", "Ivdul", "Pev", "Istupa", "Puntad", "Kenuhad",
    "Ervil", "Adel", "Irla", "Irvesavi", "Ivdol", "Pin", "Simurlad", "Rut",
    "Inurvia", "Valuv", "Nol", "Dina", "Arli", "Robivda", "Vit", "Vosiv", "Ogi",
    "Atri", "Lasti", "Envituv", "Ori", "Epi", "Puked", "Noki", "Anti", "Mevren",
    "Adal", "Enva", "Ohev", "Pul", "Torvuni", "Urlunadi", "Pud", "Lat", "Ivduhogi",
    "Onvihar", "Sivda", "Ubat", "Ogistia", "Ronvat", "Oseha", "Notrepud", "Gumot",
    "Tovrikia", "Amonvi", "Menta", "Gan", "Ontula", "Avi", "Udenva", "Umunta",
    "Uvra", "Harvi", "Unta", "Uhar", "Hiv", "Erlav", "Egul", "Kava", "Etri", "Nar",
    "Ehir", "Avil", "Hadi", "Gon", "Pistal", "Kul", "Oria", "Let", "Sit", "Hentur",
    "Supata", "Osti", "Vil", "Otrehi", "Ostal", "Anta", "Hutegir", "Vutul", "Gir",
    "Utorasa", "Got", "Rel", "Arven", "Aka", "Ika", "Nostala", "Ohi", "Atol", "Mut",
    "Val", "Apitrev", "Kodan", "Vugat", "Tavepi", "Verla", "Odav", "Dur", "Utrod",
    "Ahit", "Inia", "Untupa", "Emia", "Dar", "Sutri", "Hepovra", "Murli", "Opud",
    "Ostuni", "Donvi", "Rat", "Envet", "Onvia", "Purahia", "Osa", "Oti", "Tiv",
    "Netred", "Hot", "Onvi", "Eba", "Akepuv", "Ala", "Enurvan", "Kontehi", "Nukor",
    "Tovra", "Kol", "Hurvun", "Par", "Hatel", "Enti", "Ugi", "Ohir", "Urved",
    "Unvi", "Ihur", "Evdugovri", "Mod", "Hev", "Mad", "Sav", "Orla", "Hav", "Olera",
    "Uvdal", "Mel", "Ihunved", "Pevri", "Ibia", "Dosa", "Heputi", "Sol", "Uluv",
    "Itia", "Ked", "Venori", "Uvepa", "Uvdagi", "Upenta", "Irvun", "Minta", "Dan",
    "Henvora", "Netria", "Egi", "Gori", "Vori", "Ihason", "Lir", "Hir", "Onti",
    "Ister", "Goba", "Lontumi", "Pat", "Losirlia", "Kuhia", "Tuvdut", "Vud", "Luni",
    "Lut", "Avdot", "Udi", "Tanvi", "Dev", "Aka", "Urihav", "Elun", "Okitrad",
    "Donvara", "Ita", "Ogalir", "Gapastun", "Denvesupia", "Geli", "Ler", "Orvi",
    "Vit", "Gervad", "Girlat", "Tenorlupi", "Onvi", "Larvaki", "Ragervoba", "Iha",
    "Muvit", "Lurihia", "Govdit", "Opa", "Har", "Ipan", "Ostel", "Usa", "Osinal",
    "Losupul", "Ovra", "Urvon", "Istobut", "Rukina", "Apa", "Ivral", "Evdasa",
    "Demil", "Divia", "Asta", "Suvrotia", "Ovrud", "Pestil", "Rat", "Api", "Man",
    "Esi", "Ponti", "Vustimel", "Divroka", "Movda", "Govra", "Uba", "Tanilur",
    "Sahuntodi", "Erlun", "Inter", "Ral", "Vol", "Itren", "Davi", "Irva", "Tukia",
    "Huv", "Postia", "Gat", "Antubi", "Itrev", "Erul", "Ovdi", "Lar", "Sovri",
    "San", "Mur", "Rin", "Orvapi", "Rel", "Tarot", "Edin", "Ripun", "Govdula",
    "Elan", "Gobesil", "Avratil", "Sapiv", "Iset", "Per", "Entebonva", "Sinvia",
    "Avribul", "Eha", "Suti", "Sahev", "Dir", "Var", "Patrad", "Inta", "Ega",
    "Ruv", "Ilugosi", "Ven", "Abav", "Sumi", "Igor", "Hesi", "Hipun", "Upivria",
    "Gentosia", "Dopi", "Vur", "Mentusa", "Emotral", "Lon", "Dev", "Arlat", "Pat",
    "Estud", "Upia", "Vasa", "Apa", "Ihuv", "Logia", "Gintor", "Lamut", "Gusuvdet",
    "Votreduli", "Onipa", "Larvia", "Onvi", "Guteravi", "Muda", "Hegobat", "Pamul",
    "Mobi", "Pebul", "Geni", "Kerania", "Sivrut", "Vin", "Intul", "Ponekeba",
    "Adirlev", "Nuvdi", "Etri", "Rol", "Motrud", "Dakumuli", "Uvri", "Egenti",
    "Sogi", "Rivda", "Ama", "Sehi", "Tukev", "Guvden", "Peki", "Ginvat", "Natri",
    "Umukori", "Vul", "Dorla", "Tima", "Mun", "Vantuv", "Nat", "Toginia", "Urvel",
    "Imav", "Avrud", "Estuv", "Antod", "Ota", "Pasi", "Duvena", "Anvia", "Vehodul",
    "Nabi", "Avreni", "Usi", "Hovri", "Sitren", "Gav", "Otrin", "Vipor", "Rat",
    "Ivdet", "Unveti", "Irvan", "Tirvia", "Abiv", "Uda", "Hitrul", "Lemivdat",
    "Sukadi", "Pon", "Hivoluvi", "Van", "Ipat", "Eli", "Inveba", "Nar", "Atrav",
    "Git", "Horli", "Omatel", "Runvi", "Dor", "Uvri", "Min", "Mevdetonva", "Dan",
    "Urit", "Mav", "Iga", "Vet", "Odev", "Soda", "Avdoli", "Estekur", "Onvuki",
    "Gir", "Ped", "Uguntil", "Tuv", "Sapesi", "Esel", "Onupa", "Ukihia", "Apa",
    "Ahi", "Pil", "Parvin", "Imed", "Tori", "Uli", "Anirlen", "Dorvasi", "Pin",
    "Ima", "Gur", "Lustin", "Una", "Rumobit", "Linvobia", "Dur", "Sinva", "Revdia",
    "Ovri", "Gilentubi", "Uvril", "Vusen", "Nel", "Tesi", "Ivda", "Vastia",
    "Enviba", "Nopi", "Poneteri", "Viluna", "Lonvet", "Radi", "Evri", "Div",
    "Tun", "Tul", "Dari", "Eropa", "Hivdon", "Neven", "Kal", "Emosa", "Vuma",
    "Parut", "Oma", "Ogi", "Lemia", "Uvromi", "Vodi", "Mevrodi", "Muvria", "Sel",
    "Vehi", "Vut", "Pet", "Sol", "Lomi", "Vel", "Moda", "Utruv", "Lir", "Dorvina",
    "Varul", "Ontarin", "Ubustia", "Git", "Istun", "Kan", "Istara", "Samed",
    "Estapi", "Vetrahur", "Desenavi", "Lan", "Rel", "Ugit", "Uket", "Dil", "Nutri",
    "Ometra", "Tavrepi", "Rakin", "Istan", "Huv", "Kurukavria", "Serlut", "Durveda",
    "Ula", "Riget", "Ruga", "Ovdul", "Ekiv", "Emun", "Ivrun", "Ontared", "Riv",
    "Giv", "Gonavdia", "Nil", "Eki", "Garlad", "Tev", "Uvrel", "Iston", "Edustuli",
    "Losa", "Hor", "Kev", "Kotrumivda", "Dav", "Kad", "Ogisia", "Pusta", "Por",
    "Samud", "Sonvehed", "Onvumet", "Avrol", "Sut", "Invar", "Ubi", "Ison", "Orvut",
    "Lut", "Garvud", "Nir", "Norat", "Ikod", "Ukon", "Tugabal", "Usti", "Vir",
    "Uned", "Gurli", "Regit", "Dotren", "Mir", "Itia", "Kipia", "Pihur", "Avri",
    "Isia", "Osel", "Dan", "Lud", "Nolesi", "Hir", "Oki", "Enia", "Konti", "Osav",
    "Etia", "Teli", "Pat", "Kentehia", "Kala", "Upin", "Utragi", "Parli", "Repa",
    "Tav", "Unta", "Gonta", "Luv", "Ogenva", "Usted", "Gotan", "Ata", "Menva",
    "Lekenat", "Ovden", "Min", "Umitra", "Nitra", "Vod", "Ati", "Murvi", "Ovrena",
    "Mar", "Ner", "Lev", "Enul", "Adun", "Norvesul", "Tar", "Okad", "Pukat", "Het",
    "Uteli", "Man", "Hor", "Ora", "Muvdi", "Sena", "Upev", "Runvupeni", "Mur",
    "Emerisa", "Riv", "Peba", "Gav", "Loganal", "Ara", "Avruniga", "Vebeta",
    "Emenvor", "Natovdir", "Nali", "Tel", "Migut", "Kotuvdia", "Iva", "Tonvia",
    "Vetruda", "Erali", "Amuton", "Gati", "Ogavdi", "Kupagi", "Vurlan", "Lur",
    "Minvut", "Ovrusi", "Tuha", "Apukot", "Erli", "Givumora", "Orvi", "Dipal",
    "Abol", "Uda", "Der", "Turvod", "Iped", "Onuv", "Okasi", "Havdi", "Ubanta",
    "Pevri", "Robia", "Ekia", "Hut", "Gil", "Arvi", "Unveki", "Dikia", "Paster",
    "Vit", "Ana", "Hud", "Sivonta", "Toled", "Gituv", "Harliguntia", "Ima", "Romi",
    "Rokia", "Disotia", "Hopi", "Kurveka", "Tav", "Orla", "Uhia", "Horvul", "Pohi",
    "Uta", "Gilut", "Onvar", "Rokupiv", "Rima", "Ula", "Avrit", "Onti", "Gival",
    "Varev", "Usan", "Oli", "Ama", "Astel", "Avrin", "Ked", "Arlod", "Vin",
    "Luvohia", "Tal", "Ostir", "Obuntia", "Tor", "Dekirva", "Osten", "Dosi",
    "Konti", "Tev", "Intev", "Utun", "Ver", "Sulemia", "Muntir", "Uvred", "Norva",
    "Kav", "Antur", "Lad", "Itegabi", "Let", "Ostibun", "Noger", "Tunvari",
    "Korvekia", "Orvi", "Rad", "Vasagev", "Gemurvit", "Ter", "Oni", "Anvoti",
    "Nal", "Erovdi", "Mivdekia", "Odasad", "Vot", "Ega", "Gar", "Tel", "Ani",
    "Pima", "Epi", "Ilopa", "Ika", "Doma", "Dur", "Ekosia", "Vad", "Erit", "Pitria",
    "Lav", "Nod", "Okal", "Hoti", "Tevriga", "Utrima", "Atrived", "Unvagir", "Ipia",
    "Otri", "Paha", "Let", "Udi", "Lesta", "Uti", "Punavan", "Napanvi", "Golupi",
    "Ogi", "Nirva", "Genia", "Eria", "Kavel", "Astami", "Abia", "Huri", "Mokunvat",
    "Ubut", "Mir", "Negostia", "Gotia", "Nuderli", "Utra", "Antur", "Olervi", "Kara",
    "Iruv", "Torvi", "Evrudi", "Nadan", "Avar", "Avria", "Ketri", "Taharliv", "Pan",
    "Vokun", "Kisev", "Akonvul", "Kod", "Nuvdepa", "Iha", "Evdur", "Rev", "Envi",
    "Gimia", "Urluhia", "Guv", "Gat", "Pastan", "Itenval", "Envota", "Evri",
    "Itreda", "Vera", "Vur", "Sat", "Delir", "Eremi", "Kunva", "Uvromel", "Ehi",
    "Mud", "Odi", "Hatrel", "Asti", "Opi", "Urigia", "Had", "Omipav", "Umi", "Iga",
    "Mastil", "Hiv", "Tavi", "Dasin", "Odetri", "Olia", "Tol", "Urlika", "Orvad",
    "Nigen", "Potri", "Pahod", "Arvon", "Noli", "Dunvepia", "Galorvi", "Urvaberi",
]

name_index = 0
person_index = 0


def insert_person(family, isFemale, father, mother):
    global name_index
    global person_index

    firstname = names[name_index + 0]
    surname = names[name_index + 1]

    name_index = name_index + 2

    columns = ["Firstname", "Surname", "BirthDate", "IsFemale"]
    values = [f"'{firstname}'", f"'{surname} {family}'",
              "'someday'", str(1 if isFemale else 0)]

    if father != None:
        columns.append("FatherId")
        values.append(str(father))

    if mother != None:
        columns.append("MotherId")
        values.append(str(mother))

    print(
        f"insert into Person ({", ".join(columns)}) values ({", ".join(values)});")

    person_index = person_index + 1
    return person_index


def insert_marital_union(husband, wife):
    columns = ["HusbandId", "WifeId"]
    values = [str(husband), str(wife)]

    print(
        f"insert into MaritalUnion ({", ".join(columns)}) values ({", ".join(values)});")


def insert_company(president, name):
    columns = ["PresidentId", "Name"]
    values = [str(president), f"'{name}'"]

    print(
        f"insert into Company ({", ".join(columns)}) values ({", ".join(values)});")


def insert_employment(person, company, isEmploymentContract, salary):
    columns = ["PersonId", "CompanyId", "IsEmploymentContract", "Salary"]
    values = [str(person),  str(company), str(
        1 if isEmploymentContract else 0), str(salary)]

    print(
        f"insert into Employment ({", ".join(columns)}) values ({", ".join(values)});")


workers = []
company_selector = [1, 1, 1, 2, 2, 3]
employment_type_selector = [True, False, True, True, True, False]
salary_selector = [3000, 1000, 1500, 2000, 1000, 2000]

print("begin transaction;")

# Grandparents first, Gen 0
grandpa_a0 = insert_person("A0", False, None, None)
grandma_a0 = insert_person("A0", True, None, None)

grandpa_b0 = insert_person("B0", False, None, None)

grandpa_c0 = insert_person("C0", False, None, None)
grandma_c0 = insert_person("C0", True, None, None)

grandma_d0 = insert_person("D0", True, None, None)

# Parents now, Gen 1
first_worker = dad0_a1 = insert_person("A1", False, grandpa_a0, grandma_a0)
mom1_a1 = insert_person("A1", True, grandpa_a0, grandma_a0)
dad2_a1 = insert_person("A1", False, grandpa_a0, grandma_a0)
mom3_a1 = insert_person("A1", True, grandpa_a0, grandma_a0)

dad0_b1 = insert_person("B1", False, grandpa_b0, None)
dad1_b1 = insert_person("B1", False, grandpa_b0, None)
mom2_b1 = insert_person("B1", True, grandpa_b0, None)

mom0_c1 = insert_person("C1", True, grandpa_c0, grandma_c0)
mom1_c1 = insert_person("C1", True, grandpa_c0, grandma_c0)
dad2_c1 = insert_person("C1", False, grandpa_c0, grandma_c0)

dad0_d1 = insert_person("D1", False, None, grandma_d0)
dad1_d1 = insert_person("D1", False, None, grandma_d0)

# Children, Gen 2
workers.append(insert_person("AD2", False, dad0_d1, mom1_a1))
workers.append(insert_person("AD2", True, dad1_d1, mom3_a1))
workers.append(insert_person("AD2", False, dad0_d1, mom1_a1))
workers.append(insert_person("AD2", True, dad1_d1, mom3_a1))
workers.append(insert_person("AD2", False, dad0_d1, mom1_a1))
workers.append(insert_person("AD2", False, dad1_d1, mom3_a1))
workers.append(insert_person("AD2", False, dad0_d1, mom1_a1))
workers.append(insert_person("AD2", False, dad1_d1, mom3_a1))

workers.append(insert_person("BC2", True, dad0_b1, mom0_c1))
workers.append(insert_person("BC2", True, dad0_b1, mom0_c1))
workers.append(insert_person("BC2", True, dad1_b1, mom1_c1))
workers.append(insert_person("BC2", True, dad2_c1, mom2_b1))
workers.append(insert_person("BC2", False, dad1_b1, mom1_c1))
workers.append(insert_person("BC2", False, dad2_c1, mom2_b1))
workers.append(insert_person("BC2", False, dad0_b1, mom0_c1))
workers.append(insert_person("BC2", True, dad0_b1, mom0_c1))

# Marital Unions
insert_marital_union(dad0_d1, mom1_a1)
insert_marital_union(dad1_d1, mom3_a1)

insert_marital_union(dad0_b1, mom0_c1)
insert_marital_union(dad1_b1, mom1_c1)
insert_marital_union(dad2_c1, mom2_b1)

# Companies
insert_company(grandma_a0, "PermaCura")
insert_company(grandpa_b0, "Strohmann Media")
insert_company(grandma_c0, "COB Genetics")

worker_index = 0

for worker in workers:
    local_index = worker_index % len(company_selector)
    worker_index = worker_index + 1

    company = company_selector[local_index]
    employment_type = employment_type_selector[local_index]
    salary = salary_selector[local_index]

    insert_employment(worker, company, employment_type, salary)

print("commit;")

with open("./Zadanie 2 - SQL.txt") as f:
    print(f.read())
