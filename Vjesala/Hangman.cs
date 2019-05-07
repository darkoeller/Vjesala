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
        static private List<string> wordList = VratiListuRijeci(); 
        
        public Player Player { get; }
        static public ReadOnlyCollection<string> WordList { get; } = new ReadOnlyCollection<string>(wordList);
        public string Word { get; }
        public Outcome Outcome { get; private set; }
        public Hangman(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            Player = player;
            //_wordList = VratiListuRijeci();
            //WordList = new ReadOnlyCollection<string>(_wordList);
        }
        public Hangman() : this(new Player())
        {  }
        public void Guess(char letter)
        {

        }
        static private List<string> VratiListuRijeci()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(@"Vjesala.ListaRijeci.txt"))
            using (var reader = new StreamReader(stream))
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
