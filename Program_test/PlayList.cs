using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_test
{
    //les playlist qui contieneront les videos
    internal class PlayList
    {
        private string _name;
        private List<Video> _titles;
        private int _fullDuration;
        private int _averageDuration;

        public PlayList(string name, int fullDuration, int averageDuration)
        {
            this._name = name;
            this._fullDuration = fullDuration;
            this._averageDuration = averageDuration;
            this._titles = new List<Video>();
        }

        //ajouter une vidéo dans la liste
        public void AddVideo(Video video)
        {
            if (video == null)
            {
                this._titles.Add(video);
            }
        }

        public string Name
        {
            get{ return _name; }
        }

        public List<Video> Titles
        { 
            get{ return _titles; } 
        }

        //avoir la durée totale de la playlist
        public int GetFullDuration()
        {
            int fullDuration = 0;
            foreach (Video video in _titles)
            {
                fullDuration += Video.time;
            }
            return fullDuration;
        }

        //avoir la durée moyenne de la playlist
        public int GetAverageDuration()
        {
            int nb_videos = 0;
            int _averageDuration = 0;
            int fullDuration = GetFullDuration();
            foreach (Video video in _titles)
            {
                nb_videos++;
            }
            _averageDuration = fullDuration/nb_videos;
            return _averageDuration;
        }

        //afficher chaque caracteristique de la playlist
        public override string ToString()
        {
            return "Le nom de la playlist est  " + _name + ", elle contien les musiques suivantes: " + _titles + ", la durée totale est de " + _fullDuration + " et la durée en moyenne est de " + _averageDuration;
        }
    }
}
