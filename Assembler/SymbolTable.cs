using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class SymbolTable
    {
        /// <summary>
        /// Create empty table</summary>
        SymbolTable()
        {

        }

        /// <summary>
        /// Adds the pair (symbol, address) to the table.</summary>
        public void AddEntry(string symbol, int address)
        {
        }

        /// <summary>
        /// Does the symbol table contain the given symbol?</summary>
        public bool Contains(string symbol)
        {
            return false;
        }

        /// <summary>
        /// Returns the address associated with the symbol.</summary>
        public int GetAddress(string symbol)
        {
            return -1;
        }
    }
}
