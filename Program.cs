namespace ParseCSV2FixedWidth {
    internal class Program {
        static void Main(string[] args) {
            /// The output path for the modified dataset.THIS IS FOR TESTING ONLY
            String inputFile = "";
            String outputFile = @"C:\Users\deleteme\output.txt";
            String delimiter = "";
            const int minimumArgs = 3;
            int numOfFields, numOfRecords;

            // Get user's inputFile (path), outputFile (path) and the defined delimter.
            if(args.Length <= minimumArgs) 
            {
                inputFile = args[0];
                delimiter = args[1];
                outputFile = args[2];
            }
            else 
            {
                Console.WriteLine("ERROR: please give full file path and delimiter...");
            }

            // Init calc class and get dataset info.
            CalcFields calcFields = new CalcFields(inputFile, delimiter);

            numOfFields = calcFields.CountColumns();
            numOfRecords = calcFields.CountRecords();
            int[] longestFields = calcFields.findLongestFields();
            Console.WriteLine("-- DATASET INFO --");
            Console.WriteLine($"Dataset INPUT: {inputFile}\nDataset OUTPUT: {outputFile}");
            Console.WriteLine($"Records: {numOfRecords}\nNumber of fields/record: {numOfFields}");
            Console.Write("Field widths will be adjusted to the following: ");
            foreach (int fieldSize in longestFields)
            {
                Console.Write($"{fieldSize.ToString()}, ");
            }
            Console.WriteLine("\n-- END OF INFO --\n");
            Console.WriteLine("Will now commence in converting the dataset to flat file format.");

            // Get and check user input for decision.
            ConsoleKeyInfo userInput;
            Console.WriteLine("Press SPACE to continue or Q to abort and quit program.");
            do
            {
                userInput = Console.ReadKey();
                if (userInput.KeyChar == 'q')
                {
                    Environment.Exit(0);
                }
                if (userInput.Key != ConsoleKey.Spacebar)
                {
                    Console.WriteLine("Ok, starting process...");
                }
            } while (userInput.KeyChar != 'q' && userInput.Key != ConsoleKey.Spacebar);

            DatasetIO datasetIO = new DatasetIO(inputFile, outputFile, delimiter);
            ModifyDatasetStructure mds;
            mds = new ModifyDatasetStructure(inputFile, outputFile, delimiter, longestFields);
            
            // Modify the record then write it to file. Do this for entire dataset.
            string modifiedRecord = string.Empty;

            int counter = 0;
            // Handle null and use it to break loop because null record means end of dataset.
            while ((modifiedRecord = mds.ModifyRecords(datasetIO.ReadFields())) != null)
            {
                
                // we can print results of a progress-bar here later
                datasetIO.WriteRecord(modifiedRecord);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write(counter);
                counter++;
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("FINISHED DOING WORK");
        }
    }
}
