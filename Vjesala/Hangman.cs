using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vjesala
{
    class Hangman
    {
        private static readonly List<string> _wordList = VratiListuRijeci(); 
        
        public Player Player { get; }
        public static ReadOnlyCollection<string> WordList { get; } = new ReadOnlyCollection<string>(_wordList);
        public string Word { get; }
        public Outcome Outcome { get; private set; }
        public Hangman(Player player)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            //_wordList = VratiListuRijeci();
            //WordList = new ReadOnlyCollection<string>(_wordList);
        }
        public Hangman() : this(new Player())
        {  }
        public void Guess(char letter)
        {

        }
        private static List<string> VratiListuRijeci()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(@"Vjesala.ListaRijeci.txt"))
            using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                var text = reader.ReadToEnd();
                string[] array = text.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries);
                   return new List<string>(array);
            }
           
        }
    }
}
