using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMAssembler
{
    class Program
    {
        public static StreamReader inFile;
        public static StreamWriter outFile;
        static void Main(string[] args)
        {
            Parser theParser;
            String line;

            Console.WriteLine("Enter the name of the file with VM code");
            Console.WriteLine("The file name should be the complete path including the file type field (.vm}.");
            Console.WriteLine("Enter the name->");
            line = Console.ReadLine();
         
            if (line.Contains(".vm"))
            {
                // Get the finename without extension from the entry
                String[] strVals = new string[] {"\\" };
                String[] strSplit = line.Split(strVals, StringSplitOptions.RemoveEmptyEntries);
                String fname = (strSplit[strSplit.Count() - 1]).Trim(); ;
                fname = fname.Substring(0, fname.IndexOf(".") + 1);
                theParser = new Parser(fname);
                line = "E:\\Learning\\Coursera\\nand2tetris\\projects\\07\\MemoryAccess\\" + line;
                inFile = new StreamReader(line);
                int indx = line.LastIndexOf(".");
                line = line.Substring(0, indx + 1) + "asm";
                outFile = new StreamWriter(line);
                theParser.ReadTheFile();
                //sr.DiscardBufferedData();
                //sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                //theProcessor.FinalPass(sr, theTable, sw);
                //close the Streamreader and Streamwriter
                inFile.Close();
                outFile.Close();
            }
            else
                Console.WriteLine("The file name must include the .asm extension");

            Console.WriteLine("Press any key to close this console.");
            Console.ReadKey();

        }
    }
}

