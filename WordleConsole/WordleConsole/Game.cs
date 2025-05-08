using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WordleConsole
{
    public class Game
    {
        private string _pseudo;
        private int _nbTries;
        private bool _succeed;

        public string Pseudo
        {
            set { _pseudo = value; }
        }

        public int NbTries
        {
            set { _nbTries = value; }
        }

        public bool Succeed
        {
            get { return _succeed; }
            set { _succeed = value; }    
        }

        public override string ToString()
        {
            string success = "";
            if (_succeed)
                success = "a réussi";
            else
                success = "n'a pas réussi";
            return _pseudo + " " + success + " en " + _nbTries + " coups.";
        }

    }
}
