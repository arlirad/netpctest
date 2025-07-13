#!/usr/bin/python

import random

names = ["ubi", "kiv", "onva", "irod", "torli", "narvia", "ostar", "apan", "uhia", "emupit", "harleter", "kalia", "itul", "eter", "eketri", "tivrel", "nuvdi", "montal", "sev", "med", "nusi", "gin", "distun", "orvi", "ati", "ater", "ola", "udur", "lesapa", "tar", "otenvi", "ret", "savra", "nustun", "ivra", "ovdi", "dav", "uvrupa", "etrud", "goba", "onvet", "pestud", "kopa", "ental", "niva", "arvi", "verla", "pihorud", "agevrel", "oki", "ega", "hev", "mugia", "usoki", "oriba", "lutri", "uki", "lenved", "led", "dir", "povdir", "luti", "vun", "onvi", "nit", "pet", "rugi", "mastekonvi", "usutra", "asti", "imarvud", "lovden", "etrapar", "nirul", "pil", "ruhovdi", "ilet", "evrad", "hubi", "mabar", "girla", "ril", "ner", "atapi", "lugia", "anvet", "ovra", "hestun", "gasiv", "ivrut", "uber", "usad", "utal", "hir", "ika", "anter", "loda", "mor", "nerlod", "anekir", "usa", "savdi", "dakia", "uketreka", "upol", "orvi", "put", "nel", "unti", "giv", "rul", "moki", "silenut", "uhi", "orvabar", "elada", "ohit", "unil", "segia", "ihar", "erviha", "okin", "ivren", "hovrubil", "envapa", "gur", "tovdin", "supiha", "epi", "onar", "ohia", "gapa", "nuvi", "verlagot", "maved", "tasia", "hegola", "nuhi", "guna", "intad", "gev", "eki", "tukirvia", "ken", "uvduri", "dan", "muginver", "otrehi", "revdesi",
         "urveha", "karla", "morenta", "pet", "oli", "liv", "givat", "mud", "edev", "hiv", "gotia", "imia", "nusil", "ustir", "lin", "sal", "uba", "isa", "anvoki", "ker", "val", "imul", "vud", "pomada", "ril", "lav", "tunvotia", "etuv", "vobirvel", "ruvdi", "arliv", "envia", "rimed", "ariv", "oni", "imanta", "orin", "rokuv", "istad", "gon", "epirlav", "darla", "apia", "pelir", "vasi", "atrokiv", "depehad", "ama", "arigut", "tinver", "ovret", "atri", "orin", "ribia", "urliv", "gir", "asol", "oraha", "apa", "oki", "kumi", "kuta", "mestada", "nut", "nev", "ibugovdi", "avdar", "opi", "hestur", "lar", "regantar", "oresat", "sonta", "rikud", "anva", "oni", "tevdobi", "sen", "tuhi", "rasa", "vol", "eda", "ohi", "urveti", "uvi", "mul", "asa", "nuvanot", "ena", "eti", "iron", "ipa", "vibar", "makevul", "omintul", "hor", "nibeva", "ikia", "tuli", "vir", "tinva", "ahi", "orli", "inta", "tavdia", "herepi", "ler", "rot", "marli", "gonti", "inuha", "herit", "vil", "ukin", "nimuntoda", "vori", "gol", "avri", "lonvasat", "mesa", "nevusul", "ani", "nistal", "uberva", "tal", "obinura", "doni", "arli", "tukavra", "pan", "ontia", "novra", "tev", "senul", "deluri", "vur", "ostia", "lon", "ira", "tav", "subostebi", "hestiv", "urvi", "ovrudur", "hit", "dovrela", "usutra", "getehi", "rani", "disten", "mogurvi", ]


def printStatement(name, surname, category):
    print(f"""insert into Contacts(Name, Surname, Email, PasswordHash, Phone, BirthDate, CategoryId, {"CustomSubCategory" if category == 1 else "SubCategoryId"})
        values (
            '{name.capitalize()}',
            '{surname.capitalize()}',
            '{name}.{surname}@test.edu',
            'AQAAAAIAAYagAAAAEPtC1GERtNvFfYrf//meeDL0O1UNzqU/rWQKLlFDaLs64N9AK00eO7wMvFagimxk+w==',
            {random.randrange(111111111, 999999999)},
            '2001-07-13 00:00:00',
            {category},
            {"'Lorem ipsum dolor sit amet.'" if category == 1 else random.randrange(1, 5)}
        );""")


for i in range(0, int(len(names) / 2), 2):
    printStatement(names[i + 0], names[i + 1], int((i / 2) % 3))
