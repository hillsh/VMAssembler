using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMAssembler
{
    class Parser
    {
        public enum comType { C_ARITHMETIC, C_PUSH, C_POP, C_LABEL, C_GOTO, C_IF, C_FUNCTION, C_RETURN, C_CALL, C_FAULT };
        public Codewriter theWriter;

        public Parser(String theName)
        {
            theWriter = new Codewriter(theName);
        }

        public void ReadTheFile()
        {
            String line, inputInstruction;
            comType ct;


            line = Program.inFile.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                //process the line
                line.Trim();
                if (!(line.StartsWith("//") || line.Length == 0)) //Skip whitespace
                {
                    String[] strVals = new string[] { "//" };
                    String[] strSplit = line.Split(strVals, StringSplitOptions.RemoveEmptyEntries);
                    if (strSplit.Count() > 0)
                    {
                        inputInstruction = "// " + strSplit[0].Trim();
                        String inStr = strSplit[0].Trim();
                        String[] parts = inStr.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Count() > 0)
                        {
                            ct = getTheType(parts[0]);
                            switch (ct)
                            {
                                case comType.C_ARITHMETIC:
                                    theWriter.doArithmetic(parts[0], inputInstruction);
                                    break;
                                case comType.C_PUSH:
                                    theWriter.doPush(parts[1], parts[2], inputInstruction);
                                    break;
                                case comType.C_POP:
                                    theWriter.doPop(parts[1], parts[2], inputInstruction);
                                    break;
                            }
                        }
                    }
                }

                //Read the next line
                line = Program.inFile.ReadLine();
            }
        }
        private comType getTheType(String instr)
        {
            switch (instr)
            {
                case "add":
                case "sub":
                case "neg":
                case "eq":
                case "gt":
                case "lt":
                case "and":
                case "or":
                case "not":
                    return comType.C_ARITHMETIC;
                case "push":
                    return comType.C_PUSH;
                case "pop":
                    return comType.C_POP;
                default:
                    Console.WriteLine("Unidentified instruction: " + instr);
                    break;
            }
            return comType.C_FAULT;
        }
    }
}
