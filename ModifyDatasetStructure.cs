using System.Text;

namespace ParseCSV2FixedWidth
{
    internal class ModifyDatasetStructure
    {
        private string inFile;
        private string outFile;
        private string record;
        private StreamWriter writer;
        private StreamReader reader;
        private CalcFields calcFields;
        private string delimiter;
        private string newRecord;
        private int[] foo;

        public ModifyDatasetStructure(string _inFile, string _outFile, string _delimiter, int[] _foo)
        {
            this.inFile = _inFile;
            this.outFile = _outFile;
            this.record = string.Empty;
            this.reader = new StreamReader(this.inFile);
            this.writer = new StreamWriter(this.outFile);
            this.calcFields = new CalcFields(_inFile, _delimiter);
            this.delimiter = _delimiter;
            this.foo = _foo;
        }

        public bool ModifyRecords()
        {
            string[] tmpStr = new string[this.calcFields.CountColumns()];
            this.reader.BaseStream.Position = 0;
            while (this.reader.ReadToEnd() != null)
            {
                this.record = this.calcFields.ReadRecord();
                if (this.record == null)
                {
                    return false;
                }
                tmpStr = this.calcFields.SplitRecord(this.record, this.delimiter);
                for (int i = 0; i < tmpStr.Length; i++)
                {
                    tmpStr[i] = tmpStr[i].PadRight(foo[i] + 1);

                }
                StringBuilder sb = new StringBuilder();
                foreach (string items in tmpStr)
                {
                    sb.Append(items);
                }
                this.newRecord = sb.ToString();

                WriteOutRecord(newRecord);
            }
            return true;
        }
    }
}
