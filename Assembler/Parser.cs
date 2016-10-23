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
        int currentIndex;


        /// <summary>
        /// Initializes all vars and opens a file.</summary>
        public Parser(string[] inputCode)
        {  
            currentIndex = -1;
            lines = inputCode;
            code = new List<string>();      
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
                //Console.WriteLine("currentIndex=" + currentIndex);
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
            string line = code[currentIndex];
            if (line.StartsWith("@")) { return Command.A_COMMAND; }
            else { return Command.B_COMMAND; }
            //return Command.ERROR;
        }

        /// <summary>
        /// Returns the symbol or decimal Xxx of the current command @Xxx or (Xxx).</summary>
        /// <remarks>
        /// Should be called only when commandType() is A_COMMAND or L_COMMAND. </remarks>
        public string Symbol()
        {
            string line = code[currentIndex];
            line = line.TrimStart(new char[] { '@' });
            //Console.WriteLine("symbol is " + line);
            return line;
        }

        /// <summary>
        /// Returns the dest mnemonic in the current C-command (8 possibilities).</summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Dest()
        {
            string curLine = code[currentIndex];
            string[] parts = curLine.Split(new char[] { '=', ';' });
            int index = curLine.IndexOf('=');
            if (index < 0) { curLine = ""; }
            else { curLine = parts[0]; }
            return curLine;
        }

        /// <summary>
        /// Returns the comp mnemonic in the current C-command (28 possibilities).</summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Comp()
        {
            string curLine = code[currentIndex];
            string[] parts = curLine.Split(new char[] { '=', ';' });
            int index = curLine.IndexOf('=');
            if (index < 0) { curLine = parts[0]; }
            else { curLine = parts[1]; }
            return curLine;
        }

        /// <summary>
        /// Returns the jump mnemonic in the current C-command (8 possibilities). </summary>
        /// <remarks>
        ///  Should be called only when commandType() is C_COMMAND.</remarks>
        public string Jump()
        {
            string curLine = code[currentIndex];
            string[] parts = curLine.Split(new char[] { '=', ';' });
            if (parts.Length == 3) { curLine = parts[2]; }
            else 
            if (curLine.IndexOf(';') > -1) 
            {
                    int index1 = curLine.IndexOf('=');
                    curLine = curLine = parts[1];
            } else { curLine = ""; }
            return curLine;
        }

        /// <summary>
        /// Removes comments and white spaces.</summary>
        public void RemoveComments()
        {
            //if (error > 0) { return; }
            foreach (string line in lines)
            {
                string curLine = line;
                if (curLine.StartsWith("//")) { continue; }
                int index = curLine.IndexOf("//");
                if (index > -1) { curLine = curLine.Remove(index); }
                curLine = curLine.Trim();
                if (curLine == "") { continue; }
                code.Add(curLine);
            }
            //currentIndex = 0;
            //Console.WriteLine("code length is: " + code.Count);
            if (code.Count < 1)
            {
                //error = 1;
                Console.WriteLine("file doesn't contain any code");
            }
            for (int i = 0; i < code.Count; i++)
            {
                Console.WriteLine(i + ": " + code[i]);
            }
        }


    }

}
