using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Igra
{
    public class Hangman
    {
        private static readonly List<string> _wordList = VratiListuRijeci(); 
        private int brojDozvoljenihGresaka;
        private int incorrectGuesses = 0;
        private HashSet<char> vecPogodjeno = new HashSet<char>();
        private char [] wordToDisplay;
        public static ReadOnlyCollection<string> WordList { get; } = new ReadOnlyCollection<string>(_wordList);
       
        public Player Player { get; }
        public string Word { get; }
        public Outcome Outcome { get; private set; }
        public int PreostalihGresaka
        {
            get{ return brojDozvoljenihGresaka - incorrectGuesses;}
        }

        public Hangman(Player player, int brojDozvoljenihGresaka = 7)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player), "ne može igrač biti nitko");
            if (brojDozvoljenihGresaka < 0)
                throw new ArgumentException("broj grešaka ne smije biti negativan", nameof(brojDozvoljenihGresaka));
            this.Player = player;
            this.brojDozvoljenihGresaka= brojDozvoljenihGresaka;
                      
            Outcome = Outcome.InProgress;
            Word = OdaberiRijec();
            wordToDisplay = new char[Word.Length];
            for (int i = 0; i < wordToDisplay.Length; i+= 1)
            wordToDisplay[i] = '_';

        }

        private string OdaberiRijec()
        {
            Debug.Assert(WordList.Count > 0);
            return WordList[Hangman.random.Next(WordList.Count)];
        }

        public Hangman() : this(new Player())
        {  }
        public void Guess(char letter)
        {
            if (Outcome != Outcome.InProgress)
                throw new InvalidOperationException("igra nije u tijeku");
            if (vecPogodjeno.Contains(letter))
            {
                Console.WriteLine("Ovo si već pogodio!");
            }
            vecPogodjeno.Add(letter);

            if (!Word.Contains(letter))
            {
                Console.WriteLine($"U toj riječi nema slova {letter}");
                incorrectGuesses += 1;

                if (incorrectGuesses > brojDozvoljenihGresaka)
                {
                    Outcome = Outcome.Loss;
                    Console.WriteLine("Popušio si!");
                }
                else
                {
                    Console.WriteLine($"Samo {PreostalihGresaka} grešaka ti je ostalo!");
                }
            }
            else
            {
                PopuniPogresneOdgovore(letter);
                Console.WriteLine($"Točno! {string.Join(" ", wordToDisplay)}");
                if (!wordToDisplay.Contains('_'))
                {
                    Outcome = Outcome.Win;
                    Console.WriteLine("Braavo, pobjednik si!");
                }
            }
        }

        private void PopuniPogresneOdgovore(char letter)
        {
            for (int i = 0; i < Word.Length; i += 1)
            {
                if (Word[i] == letter)
                {
                    wordToDisplay[i] = letter;
                }
            }
        }

        private static List<string> VratiListuRijeci()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var naziv = assembly.GetManifestResourceNames();
            using (var stream = assembly.GetManifestResourceStream(@"Igra.ListaRijeci.txt"))
            using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                var text = reader.ReadToEnd();

                string[] array = Regex.Split(text,"\n", RegexOptions.IgnoreCase);
                   return new List<string>(array);
            }           
        }
         private static Random random = new Random();

    }
}
