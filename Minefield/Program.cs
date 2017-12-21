using MinefieldEngine;
using System;
using System.Threading;

namespace Minefield
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var interpreter = new Interpreter(game);

            while(!(interpreter.ExitRequested || game.Over))
            {
                var input = Console.ReadLine();

                var output = interpreter.HandleInput(input);

                Console.WriteLine(output);
            }

            Console.WriteLine("Bye...");
            Thread.Sleep(1500);
        }
    }
}
