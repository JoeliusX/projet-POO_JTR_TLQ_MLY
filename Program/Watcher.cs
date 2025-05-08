using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Watcher
    {
        private string _fullname;
        private DateTime _birthDate;

        public Watcher(string fullname, DateTime birthDate)
        {
            this._fullname = fullname;
            this._birthDate = birthDate;
        }
        public bool IsMajor()
        {
            TimeSpan age = DateTime.Today - this._birthDate;
            return age > TimeSpan.Zero;
        }
        public override string ToString()
        {
            return "Spectateur : " + _fullname;
        }
    }
}
