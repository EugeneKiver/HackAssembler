using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    enum Command
    {
        ERROR,
        A_COMMAND,
        B_COMMAND,
        L_COMMAND
    };

    class Parser
    {
        string[] lines;
        string srcExt;
        string destExt;
        string errMsg;
        string file;
        string path;
        int currentIndex;

        /// <summary>
        /// Initializes all vars and open's a file.</summary>
        public Parser(string src, string dest, string err, string arg)
        {
            srcExt = src;
            destExt = dest;
            errMsg = err;
            file = arg;
            currentIndex = -1;
       
            try
            {
                path = Path.GetFullPath(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(errMsg + path);
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
                return;
            }
            //path = path.Replace(".asm", ".hack");
            //Console.WriteLine("dest path: " + path);
            //System.IO.File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// Are there more commands in the input?</summary>
        public bool HasMoreCommands()
        {
            return false;
        }

        /// <summary>
        /// Reads the next command from the input and makes it the current command.</summary>
        /// <remarks>
        /// Should be called only if hasMoreCommands() is true. Initially there is no current command.</remarks>
        public void Advance()
        { }

        /// <summary>
        /// Returns the type of the current command.</summary>
        /// <remarks>
        /// - A_COMMAND for @Xxx where Xxx is either a symbol or a decimal number
        /// - C_COMMAND for dest=comp;jump
        /// - L_COMMAND (actually, pseudocommand) for (Xxx) where Xxx is a symbol.</remarks>
        public Command CommandType()
        {
            return Command.ERROR;
        }

        /// <summary>
        /// Returns the symbol or decimal Xxx of the current command @Xxx or (Xxx).</summary>
        /// <remarks>
        /// Should be called only when commandType() is A_COMMAND or L_COMMAND. </remarks>
        public string Symbol()
        {
            return "";
        }

        /// <summary>
        /// Returns the dest mnemonic in the current C-command (8 possibilities).</summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Dest()
        {
            return "";
        }

        /// <summary>
        /// Returns the comp mnemonic in the current C-command (28 possibilities).</summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Comp()
        {
            return "";
        }

        /// <summary>
        /// Returns the jump mnemonic in the current C-command (8 possibilities). </summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Jump()
        {
            return "";
        }

        /// <summary>
        /// TODO Returns the symbol or decimal Xxx of the current command @Xxx or (Xxx).</summary>
        /// <remarks>
        /// Should be called only when commandType() is A_COMMAND or L_COMMAND. </remarks>
        private string[] cleanCode(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            return lines;
        }


    }

}
