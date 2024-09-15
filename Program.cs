using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ParseCSV2FixedWidth {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Creating new dataset with fixed width");
            String tmpFilePath = @"C:\Users\Jordan\Projects\Datasets\tmp.txt";
            StreamWriter writer = new StreamWriter(tmpFilePath, false);
            String dataSet = "";
            String delimiter = "";
            int fixedWidth = 30; // change per dataset
            
            if(args != null) {
                dataSet = args[0];
                delimiter = args[1];
            }
            else {
                Console.WriteLine("ERROR: please give full file path.");
            }
            
            StreamReader readStream = new StreamReader(dataSet);
            String record = "";
            while((record = readStream.ReadLine()) != null) {
                String[] subStrings = null;
                subStrings = record.Split(delimiter);
                foreach (String newRecord in subStrings) {
                    writer.Write(newRecord.PadRight(fixedWidth));
                    writer.Flush();
                }
                writer.Write('\n');
                writer.Flush();
            }
            writer.Close();
            readStream.Close();
            Console.WriteLine("Done creating fixed width dataset...");
            Console.Read();
        }
    }
}
