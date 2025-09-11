using Microsoft.VisualBasic.FileIO;

namespace ParseCSV2FixedWidth
{
    internal class CalcFields
    {
        private string dataset;
        private string delimiter;

        // Constructor
        public CalcFields(string _dataset, string _delimiter)
        {
            dataset = _dataset;
            delimiter = _delimiter;
        }

        // Setup parser
        private TextFieldParser initParser()
        {
            try
            {
                TextFieldParser parser;
                parser = new TextFieldParser(dataset);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);
                parser.HasFieldsEnclosedInQuotes = true;

                return parser;
            }
            catch (Exception error)
            {

                Console.WriteLine($"There was an error opening the file.\nError: {error}", error);
                throw new FileNotFoundException($"File not found or access is denied: {error}", error);
            }
        }

        // Count how many records are in the dataset and set the recCount value.
        public int CountRecords()
        {
            int recCount = 0;
            using TextFieldParser parser = initParser();
            while(!parser.EndOfData) {
                parser.ReadLine();
                recCount++;
            }

            return recCount;
        }

        // Read the first record and count the columns and set the colCount value.
        public int CountColumns()
        {
            int colCount = 0;
            using TextFieldParser parser = initParser();
            string[] subString = parser.ReadFields();
            colCount = subString.Length;
            
            return colCount;
        }        

        /* 
         * Find the longest field/column in any record in the entire dataset and that will be our
         * new length for those specific fields.
        */
        public int[] findLongestFields()
        {        
            int[] longestFieldLength = new int[CountColumns()];
            using TextFieldParser parser = initParser();
            string[] fieldsArray = new string[CountColumns()];
            // Search through entire dataset.
            while (!parser.EndOfData)
            {
                // Check for null/end of line
                if ((fieldsArray = parser.ReadFields()) != null)
                {
                    for (int i = 0; i < fieldsArray.Length; i++)
                    {
                        if (fieldsArray[i].Length > longestFieldLength[i])
                        {
                            longestFieldLength[i] = fieldsArray[i].Length;
                        }
                    }
                }
            }
       
            return longestFieldLength;
        }
    }
}
