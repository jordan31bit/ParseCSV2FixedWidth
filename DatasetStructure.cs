using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ParseCSV2FixedWidth
{
    internal class DatasetStructure
    {
        private String outFile;
        private String inFile;
        private StreamWriter writer;
        private StreamReader reader;

        public DatasetStructure(String _outFile, String _inFile)
        {
            this.outFile = _outFile;
            this.inFile = _inFile;
            reader = new StreamReader(this.inFile);
            writer = new StreamWriter(this.outFile);
        }

        public String readRecord(int lineNumber)
        {
            for (int i = 0; i <= lineNumber; i++)
            {
                reader.ReadLine();

                if (i == lineNumber)
                {
                    return reader.ReadLine();
                }
            }
            return "I broke error message.......";
        }

        public String readRecord()
        {
            String currentRecord;
            currentRecord = reader.ReadLine();
            return currentRecord;
        }

        public String ModifyRecord()
        {
            String currentRecord = readRecord();
            CalcFields calculate = new CalcFields(inFile);

            int[] tmpPaddings = calculate.calcFieldDifference(currentRecord);
            StringBuilder sb = new StringBuilder();    
            String[] tmpSplitRecord;
            tmpSplitRecord = currentRecord.Split(",");
            int i = 0;
            String tmpStr;
            foreach (String field in tmpSplitRecord)
            {
                if(i >= 4)
                {
                    i = i - 1;
                }
                tmpStr = field.PadRight(tmpPaddings[i]);
                sb.Append(tmpStr);
                i++;
            }
            
            return sb.ToString();
        }

        public void WriteOutRecord(String x)
        {
            writer.AutoFlush = true;
            writer.WriteLine(x);
            writer.Flush();
            
        }
    }
}
