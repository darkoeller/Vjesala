using System;
using System.Collections.Generic;
using Igra;

namespace Vjesala
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Dobro ti ušao!");
            Console.WriteLine("Reci mi svoje ime?");
            string ime = Console.ReadLine();
            var igrac = new Player(ime);
            Console.WriteLine($"haj {igrac.Ime}");
            var game = new Hangman();
            game.DupliPogotci += OnDupliPogotci;
            game.PogresniPogotci += OnPogresniPogotci;
            game.IspravniPogotci += OnIspravniPogotci;
            game.Pobjeda += OnPobjeda;
            game.Poraz += OnPoraz;
            while (game.Outcome == Outcome.InProgress)
            {
                Console.WriteLine(string.Join(" ", (IEnumerable<char>)game.wordToDisplay));
                Console.WriteLine();
                Console.WriteLine("Upiši slovo: ");
                var keyinfo = Console.ReadKey(true);
                game.Guess(keyinfo.KeyChar);
            }
            Console.ReadKey();
            
        }

        private static void OnPoraz(object sender, char e)
        {
            var hangman = (Hangman)sender;
            Console.WriteLine($"Riječ ne sadrži slovo: '{e}'.");
            Console.WriteLine($"Popušio si! Riječ je bila: '{hangman.Word}'  .  ");
        }

        private static void OnPobjeda(object sender, char e)
        {
             var hangman = (Hangman)sender;
            Console.WriteLine($"Pobijedio si! Bila je to riječ: '{hangman.Word}' .  ");
        }

        private static void OnIspravniPogotci(object sender, char e)
        {
            Console.WriteLine("Ispravno!");
        }

        private static void OnPogresniPogotci(object sender, char e)
        {
            var hangman = (Hangman)sender;
            Console.WriteLine($"Riječ ne sadrži slovo: '{e}'.");
            Console.WriteLine($"Preostalo ti je: {hangman.PreostalihGresaka} .  ");
        }

        private static void OnDupliPogotci(object sender, char e)
        {
                Console.WriteLine($"Ovo slovo {e} si već pogodio, probaj ponovo!");
        }
    }
}
