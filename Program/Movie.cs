using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Movie
    {
        private string _title;
        private DateTime _puplishedAt;
        private List<Watcher> _watchers;
        
        public Movie(string title, DateTime puplishedAt)
        {
            this._title = title;
            this._puplishedAt = puplishedAt;
            this._watchers = new List<Watcher>();
        }
        public void AddWatcher(Watcher who)
        {
            if (_watchers != null)
            {
                if (who == null)
                {
                    this._watchers.Add(who);
                }
            }
        }
        public int PublishedYear
        {
            get
            {
                return _puplishedAt.Year;
            }
        }
        public List<Watcher> Watchers
        {
            get { return _watchers; }
        }
        /* 
        public int GetCountMovieWatcher()
        { 
            return _watchers.Count; 
        } */
        public override string ToString()
        {
            return _title + " - publié le " + _puplishedAt.ToShortDateString();
        }
    }
}
