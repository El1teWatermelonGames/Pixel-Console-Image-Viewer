from colorama import init, Back
init()

# Uses hexadecimal color system (16 colors to pick from)

# 0 | BLACK
# 1 | BLUE
# 2 | CYAN
# 3 | GREEN
# 4 | MAGENTA
# 5 | RED
# 6 | WHITE
# 7 | YELLOW
# 8 | LIGHT BLACK
# 9 | LIGHT BLUE
# A | LIGHT CYAN
# B | LIGHT GREEN
# C | LIGHT YELLOW
# D | LIGHT RED
# E | LIGHT MAGENTA
# F | LIGHT WHITE

# ----- Data -----
CEND = Back.RESET

CBLACK = Back.BLACK
CBLUE = Back.BLUE
CCYAN = Back.CYAN
CGREEN = Back.GREEN
CMAGENTA = Back.MAGENTA
CRED = Back.RED
CWHITE = Back.WHITE
CYELLOW = Back.YELLOW
CLBLACK = Back.LIGHTBLACK_EX
CLBLUE = Back.LIGHTBLUE_EX
CLCYAN = Back.LIGHTCYAN_EX
CLGREEN = Back.LIGHTGREEN_EX
CLYELLOW = Back.LIGHTYELLOW_EX
CLRED = Back.LIGHTRED_EX
CLMAGENTA = Back.LIGHTMAGENTA_EX
CLWHITE = Back.LIGHTWHITE_EX

# ----- Process -----

def processLines(image):
    image = open(image, 'r')
    data = []
    for line in image:
        data.append(line.replace("\n", ""))
    image.close()
    return data

def processPixel(data, patfile):
    out = []
    for line in data:
        newLine = ""
        for char in line:
            if patfile == False: char = char.replace(char, appendPixel(char, "  "))
            if patfile == True: char = char.replace(char, appendPixel(char, char+" "))
            newLine += char
        out.append(newLine)
    return out

def appendPixel(char, outChar):
    out = ""
    if char == "0": out = CBLACK
    elif char == "1": out = CBLUE
    elif char == "2": out = CCYAN
    elif char == "3": out = CGREEN
    elif char == "4": out = CMAGENTA
    elif char == "5": out = CRED
    elif char == "6": out = CWHITE
    elif char == "7": out = CYELLOW
    elif char == "8": out = CLBLACK
    elif char == "9": out = CLBLUE
    elif char == "A": out = CLCYAN
    elif char == "B": out = CLGREEN
    elif char == "C": out = CLYELLOW
    elif char == "D": out = CLRED
    elif char == "E": out = CLMAGENTA
    elif char == "F": out = CLWHITE
    elif char == " ": return "   "
    else:
        print(f"Invalid char in image file: {char}")
        exit(1)
    return str(out + outChar + CEND)


# ----- Front-end -----

def main():
    # Get the file name/path and ensure it is valid
    fp = str(input("Enter the name of the image: "))
    patfile = False

    # Ensure it is a valid file format and if it is a Print As Text file (pat.)
    if fp.endswith(".pci"):
        if fp.startswith("-pat "): 
            patfile = True
            for i in range(5): fp = fp.lstrip(fp[0])
        # Process the data to an image
        out = processLines(fp) # Fetch the data and format into a list
        out = processPixel(out, patfile) # Convert each value into the color needed

        # Output each section of the array on individual lines
        for x in out:
            print(x)
    else:
        print("Not a valid file format! Please use a Pixel Console Image file (.pci)")

if __name__ == "__main__":
    main()