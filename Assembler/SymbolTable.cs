using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class SymbolTable
    {
        private static SymbolTable instance;       
        Dictionary<string, int> table;

        /// <summary>
        /// Create empty table</summary>
        private SymbolTable()
        {
            table = new Dictionary<string, int>();
            table.Add("SP", 0);
            table.Add("LCL", 1);
            table.Add("ARG", 2);
            table.Add("THIS", 3);
            table.Add("THAT", 4);

            table.Add("R0", 0);
            table.Add("R1", 1);
            table.Add("R2", 2);
            table.Add("R3", 3);
            table.Add("R4", 4);
            table.Add("R5", 5);
            table.Add("R6", 6);
            table.Add("R7", 7);
            table.Add("R8", 8);
            table.Add("R9", 9);
            table.Add("R10", 10);
            table.Add("R11", 11);
            table.Add("R12", 12);
            table.Add("R13", 13);
            table.Add("R14", 14);
            table.Add("R15", 15);

            table.Add("SCREEN", 16384);
            table.Add("KBD", 24576);
        }

        public static SymbolTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SymbolTable();
                }
                return instance;
            }
        }

        /// <summary>
        /// Adds the pair (symbol, address) to the table.</summary>
        public void AddEntry(string symbol, int address)
        {
            table.Add(symbol, address);
        }

        /// <summary>
        /// Does the symbol table contain the given symbol?</summary>
        public bool Contains(string symbol)
        {
            return table.ContainsKey(symbol);
        }

        /// <summary>
        /// Returns the address associated with the symbol.</summary>
        public int GetAddress(string symbol)
        {
            return table[symbol];
        }
    }
}
