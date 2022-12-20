namespace cs
{
    class Program
    {
        public static List<string> splashscreen = new List<string> {
            "FFF000FFF00FFF",
            "F0F00F000000F0",
            "FFF00F000000F0",
            "F0000F000000F0",
            "F00000FFF00FFF"
        };

        public static List<string> editorHeaderColors = new List<string> {
            "0123456789ABCDEF"
        };

        public static List<string> editorHeaderSpacing = new List<string> {
            "FFFFFFFFFFFFFFFFFFFFFF"
        };
        
        public static string editorHeaderControls = "q = Quit  |  s = Save  |  n = New Line  |  k = Backspace  |  m = Revert Save";

        public static string CEND = "\u001b[0m"; //TODO move from ANSI codes to Console.BackgroundColor | https://stackoverflow.com/questions/7524057/how-do-i-change-the-full-background-color-of-the-console-window-in-c

        public static string CBLACK = "\u001b[40m";
        public static string CRED = "\u001b[41m";
        public static string CGREEN = "\u001b[42m";
        public static string CYELLOW = "\u001b[43m";
        public static string CBLUE = "\u001b[44m";
        public static string CMAGENTA = "\u001b[45m";
        public static string CCYAN = "\u001b[46m";
        public static string CWHITE = "\u001b[47m";
        public static string CBBLACK = "\u001b[40;1m";
        public static string CBRED = "\u001b[41;1m";
        public static string CBGREEN = "\u001b[42;1m";
        public static string CBYELLOW = "\u001b[43;1m";
        public static string CBBLUE = "\u001b[44;1m";
        public static string CBMAGENTA = "\u001b[45;1m";
        public static string CBCYAN = "\u001b[46;1m";
        public static string CBWHITE = "\u001b[47;1m";

        public static List<char> allowedKeysChar = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };

        public static bool debugMode { get; set; } = false;

        static void debugOut(string message)
        {
            if (debugMode) Console.WriteLine(message);
        }

        static void printImage(List<string> Image)
        {
            foreach (string line in Image)
            {
                Console.WriteLine(line);
            }
        }

        static void printToolbar(List<string> Toolbar) {
            foreach (string line in Toolbar)
            {
                Console.Write(line);
            }
        }

        static List<string> processLines(string filepath)
        {
            List<string> data = new List<string>();
            debugOut($"filepath: {filepath}");

            string rawData = File.ReadAllText(filepath);
            foreach (string line in rawData.Split(new[] { '\n' }))
            {
                data.Add(line.Trim());
            }

            return data;
        }

        static string appendPixel(string inChar, string outChar)
        {
            string output = "";
            debugOut($"In Char: {inChar}");

            if (inChar == "0") output = CBLACK;
            else if (inChar == "1") output = CRED;
            else if (inChar == "2") output = CGREEN;
            else if (inChar == "3") output = CYELLOW;
            else if (inChar == "4") output = CBLUE;
            else if (inChar == "5") output = CMAGENTA;
            else if (inChar == "6") output = CCYAN;
            else if (inChar == "7") output = CWHITE;
            else if (inChar == "8") output = CBBLACK;
            else if (inChar == "9") output = CBRED;
            else if (inChar == "A") output = CBGREEN;
            else if (inChar == "B") output = CBYELLOW;
            else if (inChar == "C") output = CBBLUE;
            else if (inChar == "D") output = CBMAGENTA;
            else if (inChar == "E") output = CBCYAN;
            else if (inChar == "F") output = CBWHITE;
            else
            {
                Console.WriteLine($"Invalid char in image file: {inChar}");
                Environment.Exit(1);
            }

            output = output + outChar + "\u001b[0m";

            debugOut($"Out Char: {output}");
            return output;
        }

        static List<string> processPixel(List<string> data, bool pat)
        {
            List<string> output = new List<string>();

            for (int i=0; i < data.Count; i++)
            {
                string line = data[i];
                string newLine = "";
                for (int j=0; j < line.Length; j++)
                {
                    string character = line[j].ToString();

                    if (pat)
                    {
                        character = appendPixel(character, character + " ");
                    }
                    else
                    {
                        character = appendPixel(character, "  ");
                    }

                    newLine += character;
                }
                debugOut($"Line {i}: {newLine}");
                output.Add(newLine);
                debugOut($"data[{i}]: {data[i]}");
                debugOut($"output[{i}]: {output[i]}");
            }
            return output;
        }

        static void renderer()
        {
            debugOut("Opened renderer with menu");

            bool pat = false;
            List<string> output = new List<string>();

            string? filepath;
            debugOut("args length 0");
            while (true)
            {
                Console.Write("Pat the file? (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out pat)) break;
            }
            while (true)
            {
                Console.Write("Filepath: ");
                filepath = Console.ReadLine();
                if (filepath == null) Console.WriteLine("Please input an answer!");
                else if (filepath.EndsWith(".pci") && File.Exists(filepath)) break;
                else Console.WriteLine("That file does not exist!");
            }
            output = processLines(filepath);
            output = processPixel(output, pat);

            printImage(output);
        }

        static List<string> LiveEditor(string file){
            debugOut("Live Editor Opened");
            List<string> output = new List<string>();
            int selectedRow = 0;
            string baseHeader = $"Pixel Console Editor | Editing: {file}";
            string headerColor = CBWHITE;
            string extraHeader = $"                           ";
            while(true)
            {
                Console.Clear();

                Console.WriteLine($"{headerColor}{baseHeader}{extraHeader}{CEND}");
                headerColor = CBWHITE;
                extraHeader = $"                           ";

                debugOut("Live editor loop iterated");
                Console.WriteLine(CBWHITE + editorHeaderControls + CEND);
                printToolbar(processPixel(editorHeaderColors, true));
                printToolbar(processPixel(editorHeaderSpacing, false));
                Console.WriteLine("\n");
                // Print current state of the image
                printImage(processPixel(output, true));

                // Allow input of next character & clear console
                ConsoleKeyInfo input = Console.ReadKey();
                Console.WriteLine();
                debugOut($"Input: {input}");

                // Process input
                if(input.KeyChar == 'q') {
                    debugOut("Break Live Editor");
                    Console.Clear();
                    Environment.Exit(0);
                }
                else if(input.KeyChar == 's') {
                    debugOut("Save to file");
                    File.Delete(file);
                    File.AppendAllText(file, string.Join("\n", output));

                    headerColor = CBGREEN;
                    extraHeader = " | Saved                   ";
                }
                else if(input.KeyChar == 'n') {
                    debugOut("New line");
                    selectedRow++;
                }
                else if(input.KeyChar == 'm') {
                    output = processLines(file);

                    headerColor = CBRED;
                    extraHeader = " | Reverted to last save   ";
                }
                else if(input.KeyChar == 'k') {
                    debugOut("Backspace");
                    if(output[selectedRow].Length > 0)
                        output[selectedRow] = output[selectedRow].Remove(output[selectedRow].Length - 1, 1);
                }
                else if(Char.IsNumber(input.KeyChar) || (Char.IsAscii(input.KeyChar) && allowedKeysChar.Contains(input.KeyChar))) {
                    debugOut("Valid key used");
                    if(output.Count-1 == selectedRow){
                        output[selectedRow] += input.KeyChar.ToString().ToUpper();
                    } else{
                        output.Add(input.KeyChar.ToString().ToUpper());
                    }
                }
            }
        }

        static void editor()
        {
            debugOut("Opened editor");

            Console.Write("Name the new image (do not include a file extension): ");
            string? filename = Console.ReadLine() + ".pci";
            if (!File.Exists(filename)) {
                var newFile = File.Create(filename);
                newFile.Close();
            } 
            else
            {
                Console.Write("A file with this name already exists, are you sure you want to continue (y/n): ");
                string? ans = Console.ReadLine();
                if (ans == "y") {
                    var newFile = File.Create(filename);
                    newFile.Close();
                }
                else
                    Environment.Exit(0);
            }
            List<string> output = LiveEditor(filename);
        }

        static void Main(string[] args)
        {
            bool pat = false;
            List<string> output = new List<string>();
            bool splash = true;

            if (args.Length > 0)
            {
                if (args[args.Length - 1] == "-no_splash")
                {
                    splash = false;
                    args = args.Except(new string[] { "-no_splash" }).ToArray();
                    debugOut("No splash image");
                }
            }

            if (args.Length > 0) {
                if (args[args.Length - 1] == "-debug")
                {
                    debugMode = true;
                    args = args.Except(new string[] { "-debug" }).ToArray();
                    debugOut("Debug Started");
                }
            }

            debugOut("Program Started");

            if (args.Length== 0)
            { // Basic run, ask for file path and -pat | Works same as Python
                if (splash)
                {
                    printImage(processPixel(splashscreen, false));
                }

                while (true)
                {
                    Console.Write("Renderer or Editor: ");
#pragma warning disable CS8602
                    string? ans = Console.ReadLine().ToLower();
#pragma warning restore CS8602
                    if (ans == null)
                        Console.WriteLine("Not a valid input!");
                    else if (ans == "renderer")
                    {
                        renderer();
                        break;
                    }
                    else if (ans == "editor")
                    {
                        editor();
                        break;
                    }
                    else
                        Console.WriteLine("Not a valid input!");
                }
            }

            else if (args.Length == 1 && args[0].EndsWith(".pci"))
            { // Print the image in args 0
                debugOut("args length 1 & ends with .pci");

                output = processLines(args[0]);
                output = processPixel(output, pat);

                printImage(output);
            }

            else if (args.Length == 1 && args[0] == "editor")
            {
                editor();
            }

            else if (args.Length == 2 && args[0] == "-pat" && args[1].EndsWith(".pci"))
            { // Pat the image in args 1
                debugOut("args length 2 & -pat called & ends with .pci");

                pat = true;
                output = processLines(args[1]);
                output = processPixel(output, pat);

                printImage(output);
            }

            else
            { // Exception
                Console.WriteLine("Args not valid!");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}