using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMAssembler
{
    class Codewriter
    {
        private String fName;

        public Codewriter(String s)
        {
             fName = s;
        }

        public void doArithmetic(String arg, String instr)
        {
            String label1, label2;

            Program.outFile.WriteLine(instr);
            switch (arg)
            {
                case "add":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=D+M");
                    break;
                case "and":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=D&M");
                    break;
                case "sub":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=M-D");
                    break;
                case "or":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=D|M");
                    break;
                case "neg":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=-M");
                    break;
                case "not":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=!M");
                    break;
                case "eq":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("MD=M-D");
                    label1 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label1);
                    Program.outFile.WriteLine("D;JEQ");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=0");
                    label2 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label2);
                    Program.outFile.WriteLine("0;JMP");
                    Program.outFile.WriteLine("(" + label1 + ")");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=-1");
                    Program.outFile.WriteLine("(" + label2 + ")");
                    break;
                case "lt":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("MD=M-D");
                    label1 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label1);
                    Program.outFile.WriteLine("D;JLT");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=0");
                    label2 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label2);
                    Program.outFile.WriteLine("0;JMP");
                    Program.outFile.WriteLine("(" + label1 + ")");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=-1");
                    Program.outFile.WriteLine("(" + label2 + ")");
                    break;
                case "gt":
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("AM=M-1");
                    Program.outFile.WriteLine("D=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("MD=M-D");
                    label1 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label1);
                    Program.outFile.WriteLine("D;JGT");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=0");
                    label2 = "lbl" + Program.labelPtr.ToString();
                    Program.labelPtr++;
                    Program.outFile.WriteLine("@" + label2);
                    Program.outFile.WriteLine("0;JMP");
                    Program.outFile.WriteLine("(" + label1 + ")");
                    Program.outFile.WriteLine("@SP");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("A=A-1");
                    Program.outFile.WriteLine("M=-1");
                    Program.outFile.WriteLine("(" + label2 + ")");
                    break;
            }

        }

        public void doPush(String segType, String val, String instr)
        {
            Program.outFile.WriteLine(instr);
            switch (segType)
            {
                case "static":
                    Program.outFile.WriteLine("@" + fName + val);
                    Program.outFile.WriteLine("D=M");
                    pushToStack();

                    break;
                case "constant":
                    Program.outFile.WriteLine("@" + val);
                    Program.outFile.WriteLine("D=A");
                    pushToStack();
                    break;
                case "local":
                    Program.outFile.WriteLine("@LCL");
                    finishPush(val);
                    break;
                case "temp":
                    Program.outFile.WriteLine("@5");
                    Program.outFile.WriteLine("D=A");   
                    Program.outFile.WriteLine("@" + val);
                    Program.outFile.WriteLine("A=D+A");
                    Program.outFile.WriteLine("D=M");   // D now has the value to be pushed on to the stack
                    pushToStack();
                    break;
                case "argument":
                    Program.outFile.WriteLine("@ARG");
                    finishPush(val);
                    break;
                case "pointer":
                    switch (val)
                    {
                        case "0":
                            Program.outFile.WriteLine("@THIS");
                            Program.outFile.WriteLine("D=M");
                            pushToStack();
                           break;
                        case "1":
                            Program.outFile.WriteLine("@THAT");
                            Program.outFile.WriteLine("D=M");
                            pushToStack();
                            break;
                    }
                    break;
                case "this":
                    Program.outFile.WriteLine("@THIS");
                    finishPush(val);
                    break;
                case "that":
                    Program.outFile.WriteLine("@THAT");
                    finishPush(val);
                    break;
            }

        }

        private void finishPush(String v)
        {
            Program.outFile.WriteLine("D=M");
            Program.outFile.WriteLine("@" + v);
            Program.outFile.WriteLine("A=D+A");
            Program.outFile.WriteLine("D=M");   // D now has the value to be pushed on to the stack
            pushToStack();
        }
        private void finishPop(String v)
        {
            Program.outFile.WriteLine("D=M");
            Program.outFile.WriteLine("@" + v);
            Program.outFile.WriteLine("D=D+A");
            Program.outFile.WriteLine("@R13");
            Program.outFile.WriteLine("M=D");   // R13 holds the address to put the data from the stack
            popFromStack();
            Program.outFile.WriteLine("@R13");
            Program.outFile.WriteLine("A=M");
            Program.outFile.WriteLine("M=D");   // and put it in the appropriate address
        }

        private void popFromStack()
        {
            Program.outFile.WriteLine("@SP");
            Program.outFile.WriteLine("M=M-1");
            Program.outFile.WriteLine("A=M");
            Program.outFile.WriteLine("D=M");   // Get the value from the stack in D register
        }

        private void pushToStack()
        {
            Program.outFile.WriteLine("@SP");
            Program.outFile.WriteLine("A=M");
            Program.outFile.WriteLine("M=D");   // The value is now on the stack
            Program.outFile.WriteLine("@SP");   // Adjust the stack pointer
            Program.outFile.WriteLine("M=M+1");
        }
        public void doPop(String segType, String val, String instr)
        {
            Program.outFile.WriteLine(instr);
            switch (segType)
            {
                case "static":
                    popFromStack();
                    Program.outFile.WriteLine("@" + fName + val);
                    Program.outFile.WriteLine("M=D");

                    break;
                case "local":
                    Program.outFile.WriteLine("@LCL");
                    finishPop(val);
                    break;
                case "argument":
                    Program.outFile.WriteLine("@ARG");
                    finishPop(val);
                    break;
                case "this":
                    Program.outFile.WriteLine("@THIS");
                    finishPop(val);
                    break;
                case "that":
                    Program.outFile.WriteLine("@THAT");
                    finishPop(val);
                    break;
                case "temp":
                    Program.outFile.WriteLine("@5");
                    Program.outFile.WriteLine("D=A");
                    Program.outFile.WriteLine("@" + val);
                    Program.outFile.WriteLine("D=D+A");
                    Program.outFile.WriteLine("@R13");
                    Program.outFile.WriteLine("M=D");   // R13 holds the address to put the data from the stack
                    popFromStack();
                    Program.outFile.WriteLine("@R13");
                    Program.outFile.WriteLine("A=M");
                    Program.outFile.WriteLine("M=D");   // and put it in the appropriate address

                    break;
                case "pointer":
                    switch (val)
                    {
                        case "0":
                            popFromStack();
                            Program.outFile.WriteLine("@THIS");
                            Program.outFile.WriteLine("M=D");

                            break;
                        case "1":
                            popFromStack();
                            Program.outFile.WriteLine("@THAT");
                            Program.outFile.WriteLine("M=D");
                            break;

                    }
                    break;
            }
        }
    }
}
