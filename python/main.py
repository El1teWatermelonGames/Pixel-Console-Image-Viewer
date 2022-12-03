from os import name, system

# Uses hexadecimal color system (16 colors to pick from)

# 0 | BLACK
# 1 | RED
# 2 | GREEN
# 3 | YELLOW
# 4 | BLUE
# 5 | MAGENTA
# 6 | CYAN
# 7 | WHITE
# 8 | BRIGHT BLACK
# 9 | BRIGHT RED
# A | BRIGHT GREEN
# B | BRIGHT YELLOW
# C | BRIGHT BLUE
# D | BRIGHT MAGENTA
# E | BRIGHT CYAN
# F | BRIGHT WHITE

# ----- Data -----
CEND = "\u001b[0m"

CBLACK = "\u001b[40m"
CRED = "\u001b[41m"
CGREEN = "\u001b[42m" 
CYELLOW = "\u001b[43m"
CBLUE = "\u001b[44m"
CMAGENTA = "\u001b[45m"
CCYAN = "\u001b[46m"
CWHITE = "\u001b[47m"
CBBLACK = "\u001b[40;1m"
CBRED = "\u001b[41;1m"
CBGREEN = "\u001b[42;1m"
CBYELLOW = "\u001b[43;1m"
CBBLUE = "\u001b[44;1m"
CBMAGENTA = "\u001b[45;1m"
CBCYAN = "\u001b[46;1m"
CBWHITE = "\u001b[47;1m"

# ----- Process -----

def clearConsole():
    if name == "nt":
        system("cls")
    else:
        system("clear")

def processLines(image):
    image = open(image, 'r') # Open the image file
    data = []
    for line in image:
        data.append(line.replace("\n", "")) # Remove \n character from start of line and add the line to the list
    image.close()
    return data

def processPixel(data, patfile):
    out = []
    for line in data: # Cycle through each line of data
        newLine = ""
        for char in line: # Cycle through character
            if patfile == False: char = char.replace(char, appendPixel(char, "  ")) # Normal image processing
            if patfile == True: char = char.replace(char, appendPixel(char, char+" ")) # Pat file processing
            newLine += char
        out.append(newLine)
    return out

def appendPixel(char, outChar):
    out = ""
    if char == "0": out = CBLACK
    elif char == "1": out = CRED
    elif char == "2": out = CGREEN
    elif char == "3": out = CYELLOW
    elif char == "4": out = CBLUE
    elif char == "5": out = CMAGENTA
    elif char == "6": out = CCYAN
    elif char == "7": out = CWHITE
    elif char == "8": out = CBBLACK
    elif char == "9": out = CBRED
    elif char == "A": out = CBGREEN
    elif char == "B": out = CBYELLOW
    elif char == "C": out = CBBLUE
    elif char == "D": out = CBMAGENTA
    elif char == "E": out = CBCYAN
    elif char == "F": out = CBWHITE
    else:
        print(f"Invalid char in image file: {char}")
        exit(1)
    return str(out + outChar + CEND)


# ----- Front-end -----

def main():
    # Get the file name/path and ensure it is valid
    fp = str(input("Enter the name of the image: "))
    patfile = False

    # Ensure it is a valid file format and account if the image is patted
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