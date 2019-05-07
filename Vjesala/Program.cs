using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjesala
{
    class Program
    {
        static void Main()
        {
            var player = new Player("Darko");
            var game = new Hangman();

            var voce = Hangman.WordList;
            foreach (var item in voce)
            {
                Console.WriteLine(item);
            }

            
           // game.Guess('a');
        }
    }
}
