using Microsoft.VisualBasic.FileIO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ParseCSV2FixedWidth
{
    internal class DatasetIO
    {
        private string inputFile;
        private string outputFile;
        private string delimiter;
        private long lineNumber;

        public DatasetIO(string inputFile, string outputFile, string delimiter)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
            this.delimiter = delimiter;
            lineNumber = 1;
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
                StreamWriter writer = new StreamWriter(outputFile, append: true);
                writer.AutoFlush = true;
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
            try
            {
                string[]? foo;
                FileStream readStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
                TextFieldParser parser = new TextFieldParser(readStream);
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(delimiter);
                    parser.HasFieldsEnclosedInQuotes = true;

                // Skip lines we have already processed.
                while(parser.LineNumber < lineNumber && !parser.EndOfData && parser.LineNumber > 0)
                {
                    parser.ReadLine();
                }
                
                while(!parser.EndOfData && lineNumber > 0)
                {
                    foo = parser.ReadFields();
                    lineNumber = parser.LineNumber;
                    return foo;
                }
                
                foo = null;
                return foo;
            }
            catch (FileNotFoundException error)
            {
                throw new FileNotFoundException($"ERROR: FILE DOES NOT EXIST.\n{error.Message}");
            }
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

        // Not performant and could RUN OUT OF MEMORY on very large datasets.
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
            Console.WriteLine(record);
            writer.WriteLine(record);
        }
    }
}
