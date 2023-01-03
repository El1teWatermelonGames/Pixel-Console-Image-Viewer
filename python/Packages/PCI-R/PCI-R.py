from os import name, system

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

def processLines(image):
    image = open(image, "r")
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
    elif char == " ": out = CEND
    else:
        print(f"Invalid char in image file: {char}")
        exit(1)
    return str(out + outChar + CEND)

# ----- Front-end -----

def clearConsole():
    if name == "nt":
        system("cls")
    else: # Posix
        system("clear")

def printImage(imageData):
    for line in imageData:
        print(line)

def main():
    while(True):
        # Get the file name/path and ensure it is valid
        fp = str(input("Enter the name of the image: "))
        patfile = False
        size = len(fp)

        if fp.endswith(" -pat"):
            patfile = True
            fp = fp[:size - 5]

        # Ensure it is a valid file format and account if the image is patted
        if fp.endswith(".pci"):
            # Process the data to an image
            out = processLines(fp) # Fetch the data and format into a list
            out = processPixel(out, patfile) # Convert each value into the color needed

            # Output each section of the array on individual lines
            clearConsole()
            printImage(out)
            input("\nPress enter to close window...")
            break
        else:
            clearConsole()
            print("Not a valid file format! Please use a Pixel Console Image file (.pci)")

if __name__ == "__main__":
    main()