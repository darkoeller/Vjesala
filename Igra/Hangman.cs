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
        public event EventHandler<char> DupliPogotci;
        public event EventHandler<char> PogresniPogotci;
        public event EventHandler<char> IspravniPogotci;
        public event EventHandler<char> Poraz;
        public event EventHandler<char> Pobjeda;

        private static readonly List<string> _wordList = VratiListuRijeci(); 
        private int brojDozvoljenihGresaka;
        private int incorrectGuesses = 0;
        private HashSet<char> vecPogodjeno = new HashSet<char>();
        public char [] wordToDisplay;
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
                DupliPogotci?.Invoke(this, letter);

                return;
            }
            vecPogodjeno.Add(letter);

            if (!Word.Contains(letter))
            {
                Console.WriteLine($"U toj riječi nema slova {letter}");
                incorrectGuesses += 1;

                if (incorrectGuesses > brojDozvoljenihGresaka)
                {
                    Outcome = Outcome.Loss;
                    Poraz?.Invoke(this, letter);
                }
                else
                {
                    PogresniPogotci?.Invoke(this, letter);
                }
            }
            else
            {
                PopuniPogresneOdgovore(letter);
                Console.WriteLine($"Točno! {string.Join(" ", wordToDisplay)}");
                if (!wordToDisplay.Contains('_'))
                {
                    Outcome = Outcome.Win;
                    Pobjeda?.Invoke(this, letter);
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
