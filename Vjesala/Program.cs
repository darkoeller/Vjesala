using System;
using Igra;

namespace Vjesala
{
    class Program
    {
        static void Main()
        {
            var game = new Hangman();

            while (game.Outcome == Outcome.InProgress)
            {
                Console.WriteLine("Upiši slovo: ");
                var keyinfo = Console.ReadKey(true);
                game.Guess(keyinfo.KeyChar);
            }
            Console.ReadKey();
            
        }
    }
}
