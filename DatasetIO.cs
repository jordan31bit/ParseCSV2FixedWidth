using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ParseCSV2FixedWidth
{
    internal class DatasetIO
    {
        private string inputFile;
        private string outputFile;
        private string delimiter;

        public DatasetIO(string inputFile, string outputFile, string delimiter)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            this.delimiter = delimiter;
        }

        // Create and setup parser.
        public TextFieldParser InitParser()
        {
            try
            {
                TextFieldParser parser;
                parser = new TextFieldParser(inputFile);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(delimiter);
                parser.HasFieldsEnclosedInQuotes = true;

                return parser;
            }
            catch (Exception error)
            {
                throw new IOException($"File IO Error: {error.Message}");
            }
        }

        // Create and setup writer for file IO.
        private StreamWriter InitWriter()
        {
            try
            {
                StreamWriter writer = new StreamWriter(outputFile);
                return writer;

            }
            catch (Exception error)
            {

                throw new IOException($"File IO error: {error.Message}");
            }
        }

        /**************************************************************************
        * 
        * READING IO SECTION
        * 
        **************************************************************************/

        public string[] ReadFields()
        {
            using TextFieldParser parser = InitParser();
            return parser.ReadFields();
        }

        // Read only FIRST RECORD.
        public string ReadRecord()
        {
            // Will return null on empty record or end of file.
            using TextFieldParser parser = InitParser();
            return parser.ReadLine();
        }

        // Read a certain record/line only.
        public string ReadRecord(int lineNum)
        {
            // Will return null on empty record or end of file.
            using TextFieldParser parser = InitParser();
            int x = 0;
            string line = null;
            while (!parser.EndOfData)
            {   // Search through the dataset and increment x for each line read
                line = parser.ReadLine();
                if (x == lineNum)
                {
                    break; 
                }
               
                x++;
            }
            return line;
        }

        // Not performat and could RUN OUT OF MEMORY on very large datasets.
        public List<string> ReadAllRecords()
        {
            List<string> allRecords = new List<string>();
            StringBuilder sb = new StringBuilder();
            using TextFieldParser parser = InitParser();
  
            while (!parser.EndOfData)
            {
                foreach (string item in parser.ReadFields())
                {
                    if (item == null)
                    {
                        throw new InvalidDataException("Error: null gotten in foreach loop with in function READALLRECORDS()");
                        
                    }
                    sb.Append(item);
                }
                allRecords.Add(sb.ToString());
                sb.Clear();
            }
            return allRecords;
        }


        /**************************************************************************
         * 
         * WRITING IO SECTION
         * 
         **************************************************************************/

        public void WriteRecord(string record)
        {
            using StreamWriter writer = InitWriter();   
            writer.WriteLine(record);
        }
    }
}
