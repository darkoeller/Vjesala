using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjesala
{
    class Player
    {

        public string Ime { get; set; }
        public Player(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Ime = name;
        }
        public Player() : this("Anonymous")
        { }
    }
}
