using MinefieldEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minefield
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter(new Game());

            while(true)
            {
                var input = Console.ReadLine();

                var output = interpreter.HandleInput(input);

                Console.WriteLine(output);
            }
        }
    }
}
