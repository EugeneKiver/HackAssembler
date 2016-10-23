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
        List<string> code;
        string srcExt;
        string destExt;
        string errMsg;
        string file;
        string path;
        int currentIndex;
        int error;

        /// <summary>
        /// Initializes all vars and open's a file.</summary>
        public Parser(string src, string dest, string err, string arg)
        {
            srcExt = src;
            destExt = dest;
            errMsg = err;
            file = arg;
            currentIndex = -1;
            error = 0;
            code = new List<string>();

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
            //path = path.Replace(".asm", ".hack");
            //Console.WriteLine("dest path: " + path);
            //System.IO.File.WriteAllLines(path, lines);
            RemoveComments();
        }

        /// <summary>
        /// Are there more commands in the input?</summary>
        public bool HasMoreCommands()
        {
            if ((code.Count-1) - currentIndex > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads the next command from the input and makes it the current command.</summary>
        /// <remarks>
        /// Should be called only if hasMoreCommands() is true. Initially there is no current command.</remarks>
        public void Advance()
        {
            if(HasMoreCommands())
            {
                currentIndex++;
                Console.WriteLine("currentIndex=" + currentIndex);
            }
        }

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
        /// Removes comments and white spaces.</summary>
        private void RemoveComments()
        {
            if (error > 0) { return; }
            foreach (string line in lines)
            {
                string curLine = line;
                if (curLine.StartsWith("//")) { continue; }
                int index = curLine.IndexOf("//");
                if (index > -1) { curLine = curLine.Remove(index); }
                curLine = curLine.Trim();
                if (curLine == "") { continue; }
                code.Add(curLine);
                Console.WriteLine(curLine);
            }
            //currentIndex = 0;
            Console.WriteLine("code length is: " + code.Count);
            if (code.Count < 1)
            {
                error = 1;
                Console.WriteLine("file doesn't contain any code");
            }
        }


    }

}
