#!/usr/bin/python

import xml.etree.ElementTree as ET


def get_full_text(elem):
    parts = []
    if elem.text:
        parts.append(elem.text)

    for child in elem:
        if child.tag == "see":
            parts.append(f"**{child.attrib["cref"].strip("T:")}**")

        if child.tail:
            parts.append(child.tail)

    return ''.join(parts)


def convert(path, important):
    tree = ET.parse(path)
    root = tree.getroot()

    prev_class = None

    for member in root[1]:
        name = member.attrib["name"].removeprefix("T:") \
            .removeprefix("M:").removeprefix("E:")
        parent_name = None
        member_of_important = False
        parameters_printed = False

        if member.attrib["name"].startswith("M:") or member.attrib["name"].startswith("E:"):
            for important_class_name in important:
                if name.startswith(important_class_name):
                    parent_name = important_class_name
                    member_of_important = True
                    break

        if not member_of_important and not name in important:
            continue

        if not member_of_important and name != prev_class:
            prev_class = name
            print()

        name = name.replace("System.Int32", "int")
        name = name.replace("System.String", "string")

        if not member_of_important:
            print(f"### {name}")
            print(f"{get_full_text(member[0]).strip()}")
            print()
        else:
            name = name.replace(parent_name, "").lstrip(".")
            name_split = name.split("(")
            name_parameters = name_split[1].rstrip(
                ")") if len(name_split) > 1 else None
            name_clean = name_split[0]

            if name_parameters != None:
                params = []
                params_index = 0

                for param_type in name_parameters.split(","):
                    params.append([param_type, None])

                for child in member:
                    if child.tag == "param":
                        params[params_index][1] = child.attrib["name"]
                        params_index = params_index + 1

                params_flattened = []

                for param in params:
                    params_flattened.append(f"**{param[0]}** *{param[1]}*")

                name_clean = name_clean + f"({", ".join(params_flattened)})"

            print(f" - #### {name_clean}")
            print(f"   - {get_full_text(member[0]).strip()}")

            for child in member:
                if child.tag == "param":
                    if not parameters_printed:
                        print("   - ##### Parameters")
                        parameters_printed = True

                    print(
                        f"     - *{child.attrib["name"]}* - {get_full_text(child)}")
                if child.tag == "returns":
                    print("   - ##### Returns")
                    print(f"     - {get_full_text(child)}")

            print()


backend_important = [
    "NetPCTest.Backend.Services.ICategoriesService",
    "NetPCTest.Backend.Services.IContactsService",
    "NetPCTest.Backend.Services.ILocalisationService",
    "NetPCTest.Backend.Services.IPasswordService",
]

frontend_important = [
    "NetPCTest.Frontend.Services.IAuthService",
    "NetPCTest.Frontend.Services.ICategoriesService",
    "NetPCTest.Frontend.Services.IContactsService",
    "NetPCTest.Frontend.Services.ILocalisationService",
]

convert("./backend/NetPCTest.Backend/bin/Debug/net9.0/NetPCTest.Backend.xml",
        backend_important)
convert("./frontend/NetPCTest.Frontend/bin/Debug/net9.0/NetPCTest.Frontend.xml",
        frontend_important)
