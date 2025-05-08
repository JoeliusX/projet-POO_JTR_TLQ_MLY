using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_test
{
    //les videos qui seront ajouter plus tart dans une playlist
    internal class Video
    {
        private string _name;
        private int _time;

        public Video(string name, int time)
        {
            this._name = name;
            this._time = time;
        }

        public static int time { get; internal set; }

        public string Name
        { 
            get { return _name; } 
        }

        public int Time
        { 
            get { return _time; }
        }

        //afficher chaque caracteristique de la video
        public override string ToString()
        {
            return "Le nom de la vidéo est  " + _name + " et la durée est de ";
        }
}
}
