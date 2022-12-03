namespace ConsolePixelApplication
{
    class Program
    {
        static void processLines(string filepath)
        {
            string rawData = File.ReadAllText(filepath);
            List<string> data = new List<string>();
        }

        static void Main(string[] args)
        {
            // Get the image file and ensure it is valid
            Console.WriteLine("Enter the filepath to the image:");
            string filepath = Console.ReadLine();
            bool patfile = false;
            List<string> output = new List<string>();

            // Ensure it is a valid file format and if it is a Print As Text file (-pat ) take that into account
            if (filepath.EndsWith(".pci")){
                if (filepath.StartsWith("-pat ")){
                    patfile = true;
                    filepath = filepath.Remove(0, 5);
                }

                // Process the data functions


                // Output each section of the array on an individual line
                for (int i=0; i < output.Count; i++)
                {
                    Console.WriteLine(output[i]);
                }
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not a valid file format! Please use a Pixel Console Image file (.pci)\nPress enter to exit...");
                Console.ReadLine();
            }
        }
    }
}