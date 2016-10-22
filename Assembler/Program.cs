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
            Parser parser = new Parser(".asm", ".hack",
                "no file found", args[0]);
            Console.ReadKey();
        }
    }
}
