namespace cs
{
    internal class Program
    {
        public static List<string> splashscreen = new List<string> {
            "0066611111133300",
            "0666611111133330",
            "6666611111133333",
            "6666611111133333",
            "4444455555522222",
            "4000455005520002",
            "4040450550522022",
            "4000450555522022",
            "4044450555522022",
            "4044450550522022",
            "4044455005520002",
            "4444455555522222",
            "6666611111133333",
            "6666611111133333",
            "0666611111133330",
            "0066611111133300",
        };

        public static bool debugMode { get; set; } = false;

        static void debugOut(string message) { if (debugMode) Console.WriteLine(message); }

        static void printImage(List<string> Image) { foreach (string line in Image) { Console.WriteLine(line); } }

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

            if (inChar == "0") output = "\u001b[40m";
            else if (inChar == "1") output = "\u001b[41m";
            else if (inChar == "2") output = "\u001b[42m";
            else if (inChar == "3") output = "\u001b[43m";
            else if (inChar == "4") output = "\u001b[44m";
            else if (inChar == "5") output = "\u001b[45m";
            else if (inChar == "6") output = "\u001b[46m";
            else if (inChar == "7") output = "\u001b[47m";
            else if (inChar == "8") output = "\u001b[40;1m";
            else if (inChar == "9") output = "\u001b[41;1m";
            else if (inChar == "A") output = "\u001b[42;1m";
            else if (inChar == "B") output = "\u001b[43;1m";
            else if (inChar == "C") output = "\u001b[44;1m";
            else if (inChar == "D") output = "\u001b[45;1m";
            else if (inChar == "E") output = "\u001b[46;1m";
            else if (inChar == "F") output = "\u001b[47;1m";
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

            else if (args.Length == 1 && args[0].EndsWith(".pci"))
            { // Print the image in args 0
                debugOut("args length 1 & ends with .pci");

                output = processLines(args[0]);
                output = processPixel(output, pat);

                printImage(output);
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