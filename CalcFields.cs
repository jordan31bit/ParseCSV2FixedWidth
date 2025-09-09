using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseCSV2FixedWidth
{
    internal class CalcFields
    {
        private String dataset { get; set; }
        private int fieldLength;
        private int recCount;
        private int colCount;
        public int[] fieldLenCollection;
        
        // Constructor
        public CalcFields()
        {

        }
        public CalcFields(String _dataset)
        {
            dataset = _dataset;
            fieldLength = 0;
            colCount = CountColumns();
            recCount = CountRecords();
            fieldLenCollection = new int[colCount];
            fieldLenCollection = findFieldLength();
        }


        private int CountRecords()
        {
            StreamReader reader = new StreamReader(dataset);
            while(!reader.EndOfStream) {
                reader.ReadLine();
                recCount++;
            }
            reader.Close();
            return this.recCount;
        }

        public int CountColumns()
        {
            StreamReader reader = new StreamReader(dataset);
            String tmpStr = reader.ReadLine();
            if (tmpStr != null)
            {
                String[] subString = tmpStr.Split(",");
                this.colCount = subString.Length;
            }
            reader.Close();
            return this.colCount;
        }

        public int[] findFieldLength()
        {
            StreamReader reader = new StreamReader(dataset);
            String tmpString;
            int columnNum = 1;
            int tmpLength = 0;
            String[] subStr;
            // loop to control going through columns
            while(columnNum <= colCount)
            {
                for (int i = 0; i < recCount; i++)
                {
                    // read record to first delimiter
                    tmpString = reader.ReadLine();

                    if (tmpString != null)
                    {

                        subStr = tmpString.Split(",");
                        tmpLength = subStr[columnNum - 1].Length;

                        if (tmpLength > fieldLength)
                        {
                            fieldLength = tmpLength;
                            fieldLenCollection[columnNum - 1] = tmpLength;
                        }
                    }
                }
                columnNum++;
                reader.BaseStream.Position = 0;
                reader.DiscardBufferedData();
            }
            reader.Close();
            for (int i = 0; i < fieldLenCollection.Length; i++)
            {
                Console.WriteLine(fieldLenCollection[i]);
            }
            return fieldLenCollection;
        }

        private int getFieldLength(int lineNumber)
        {
            StreamReader reader = new StreamReader(dataset);
            for (int i = 0; i <= lineNumber; i++)
            {
                reader.ReadLine();
                if(i == lineNumber)
                {
                    String tmpString = reader.ReadLine();
                    return tmpString.Split(",").Length;
                }
            }
            // Break stuff
            
            return -1000;
        }


        private int getFieldLength(String currentRecord, int index)
        {
            String[] tmpStr;
            tmpStr = currentRecord.Split(",");
            
            return tmpStr[index].Length;
        }



        // This is how much padding to add to each record in output file.
        public int[] calcFieldDifference(String currentRecord)
        {
            int[] fieldPaddings = new int[colCount];
            for (int i = 0; i < fieldPaddings.Length; i++)
            {
                //fieldPaddings[i] = fieldLenCollection[i] - getFieldLength(currentRecord, i);
                fieldPaddings[i] = fieldLenCollection[i]; 
            }
            return fieldPaddings;
        }
    }
}
