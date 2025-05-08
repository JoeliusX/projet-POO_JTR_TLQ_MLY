using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
   internal class Theater
    {
        private List<Movie> _movies;
        private List<Watcher> _watchers;

        public void AddMovie(Movie movie)
        {
            this._movies.Add(movie);
        }
        public int UniqueWatchersCount
        {
            get { return _watchers.Count; }
        }
        public List<Movie> GetLowWatchedMovies(int threshold)
        {
            List<Movie> badMovies = new List<Movie>();
            foreach (Movie wat in _movies)
            {
                if (wat.Watchers.Count < threshold)
                {
                    badMovies.Add(wat);
                }
            }
            return badMovies;
        }
        private List<Watcher> GetAllWatchers()
        {
            Console.WriteLine("Détails des spectateurs du cinéma : ");
            foreach (Watcher wat in _watchers)
            {
                Console.WriteLine(wat.ToString());
            }
            return _watchers;
        }
    }
}
