using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcExt = ".asm";
            string destExt = ".hack";
            string errMsg = "no file found: ";
            string file = args[0] ;
            string path = "";
            int error = 0;
            int commandLength = 16;
            string[] lines;
            List<string> binaryCode = new List<string>();
            SymbolTable table = SymbolTable.Instance;

            try
            {
                path = Path.GetFullPath(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(errMsg + path);
                error = 1;
                return;
            }

            if (path.EndsWith(srcExt, System.StringComparison.CurrentCultureIgnoreCase))
            {
                lines = System.IO.File.ReadAllLines(path);
                /*foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }*/
            }
            else
            {
                Console.WriteLine(errMsg + path);
                error = 1;
                return;
            }
            
            Parser parser = new Parser(lines);
            parser.RemoveComments();
            Code coder = new Code();

            int iteration = 0;
            int variableAddress = 16;
            while (true)
            {      
                iteration++;
                if(parser.HasMoreCommands())
                {
                    int symbol;
                    string command = "";
                    parser.Advance();
                    switch (parser.CommandType())
                    {
                        case Command.A_COMMAND:

                            symbol = Int32.Parse(parser.Symbol());
                            string binary = Convert.ToString(symbol, 2);
                            for (int i = 0; i < (commandLength - binary.Length); i++)
                            {
                                command += "0";
                            }
                            command += binary;
                            Console.WriteLine((iteration - 1) + ": " + command + " |A-instruction");
                            break;
                        case Command.B_COMMAND:
                            string dest = parser.Dest();


                            string destCode = coder.Dest(dest);
                            string comp = parser.Comp();
                            string compCode = coder.Comp(comp);
                            string jump = parser.Jump();
                            string jumpCode = coder.Jump(jump);

                            command = "111" + compCode + destCode + jumpCode;

                            Console.WriteLine((iteration - 1) + ": " + command + " |dest: " + dest + " " + destCode + "|comp: " + comp + " " + compCode + "|jump: " + jump + " " + jumpCode);

                            break;
                        case Command.L_COMMAND:
                            symbol = 0;
                            if (!table.Contains(parser.Symbol()))
                            {
                                table.AddEntry(parser.Symbol(), variableAddress);
                                Console.WriteLine("variable added " + parser.Symbol() + " at " + variableAddress);
                                variableAddress++;
                            }

                            symbol = table.GetAddress(parser.Symbol());                           
                            binary = Convert.ToString(symbol, 2);
                            for (int i = 0; i < (commandLength - binary.Length); i++)
                            {
                                command += "0";
                            }
                            command += binary;
                            Console.WriteLine((iteration - 1) + ": " + command + " |L-instruction");
                            break;
                        default:
                            break;
                    }
                    binaryCode.Add(command);

                    //Console.WriteLine("command: " + parser.CommandType().ToString());
                }
                else { break; }
            }
            string[] binaryArray = binaryCode.ToArray();
            path = path.Replace(srcExt, destExt);
            Console.WriteLine("dest path: " + path);
            System.IO.File.WriteAllLines(path, binaryArray);

            Console.WriteLine("\nFinal code");
            foreach (string line in binaryCode)
            {
                Console.WriteLine(line);
            }
            Console.ReadKey();
        }
    }
}
