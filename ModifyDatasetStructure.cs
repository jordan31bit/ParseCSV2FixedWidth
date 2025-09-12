using System.Text;

namespace ParseCSV2FixedWidth
{
    internal class ModifyDatasetStructure
    {
        private string inFile;
        private string outFile;
        private string record;
        private string delimiter;
        private string? newRecord;
        private int[] newFieldWidths;
        private DatasetIO datasetIO;

        public ModifyDatasetStructure(string _inFile, string _outFile, string _delimiter, int[] newFieldWidths)
        {
            this.inFile = _inFile;
            this.outFile = _outFile;
            this.delimiter = _delimiter;
            this.record = string.Empty;
            this.datasetIO = new DatasetIO(inFile, outFile, delimiter);
            this.newFieldWidths = newFieldWidths;
        }

        public string ModifyRecords(string[] currentRecord)
        {
            if (currentRecord == null || currentRecord.Length < 1)
            {
                newRecord = null;
                return newRecord;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                int iterator = 0;
                foreach (string item in currentRecord)
                {
                    // Add padding to right and +1 to give 1 char space to align things pretty
                    sb.Append(item.PadRight(newFieldWidths[iterator] + 1));
                    iterator++;
                }
                iterator = 0;
                newRecord = sb.ToString();
                sb.Clear();
                return newRecord;
            }
        }
    }
}
