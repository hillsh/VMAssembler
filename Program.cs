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
        public static int labelPtr = 0;
        static void Main(string[] args)
        {
            Parser theParser;
            String line;

            Console.WriteLine("Enter the name of the file with VM code, or a directory name containing VM files ");
            Console.WriteLine("The file name should be the complete path including the file type field (.vm} if it is a VM file.");
            Console.WriteLine("Enter the name->");
            line = Console.ReadLine();
         
            if (line.Contains(".vm"))
            {
                // Get the filename without extension from the entry
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
            {
                Console.WriteLine("You have entered a directory name");
                line = "E:\\Learning\\Coursera\\nand2tetris\\projects\\08\\FunctionCalls\\" + line;
                String[] strVals = new string[] { "\\" };
                String[] strSplit = line.Split(strVals, StringSplitOptions.RemoveEmptyEntries);
                String fname = (strSplit[strSplit.Count() - 1]).Trim();
                DirectoryInfo d = new DirectoryInfo(line);
                FileInfo[] fI = d.GetFiles("*.vm");
                Boolean outFileDone = false;
                if (fI.Length > 0)
                {
                    if (fI.Length > 1)
                    {
//                        outFile = new StreamWriter(line + ".asm");
                        outFileDone = true;
                    }
                    foreach (var file in fI)
                    {
                        Console.WriteLine("processing file " + file.Name);
                        if (!outFileDone)
                        {
                            strSplit = file.Name.Split(strVals, StringSplitOptions.RemoveEmptyEntries);
                            fname = (strSplit[strSplit.Count() - 1]).Trim();
                            fname = fname.Substring(0, fname.IndexOf("."));
                            String tmp = file.Name.Substring(0,file.Name.LastIndexOf(".") + 1);
//                            outFile = new StreamWriter(tmp = "asm");
                            outFileDone = true;
                        }
//                        theParser = new Parser(fname);
//                        inFile = new StreamReader(file.Name);
//                        theParser.ReadTheFile();
                   }
                }
                else
                 Console.WriteLine("There are no VM files in the directory!");

            }

            Console.WriteLine("Press any key to close this console.");
            Console.ReadKey();

        }
    }
}

