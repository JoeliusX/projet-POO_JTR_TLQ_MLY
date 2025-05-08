namespace Program_test
{
    //création des videos et playlist
    internal class Program
    {
        static void Main(string[] args)
        {
            //création des vidéos
            Video video1 = new Video("Salut", 1240);
            Video video2 = new Video("À tous", 2450);
            Video video3 = new Video("Le monde", 1536);

            //création de la playlist
            PlayList playlist1 = new PlayList("MyPlaylist1", 5226, 1742);

            //ajouter chaque vidéos dans la playlist
            playlist1.AddVideo(video1);
            playlist1.AddVideo(video2);
            playlist1.AddVideo(video3);

            //affichage des titres, durée totale et durée moyenne de la playlist
            Console.WriteLine("Les vidéos qui a la playlist1 sont: " + playlist1.Titles);
            Console.WriteLine("La durée totale de la playlist1 est de: " + playlist1.GetFullDuration());
            Console.WriteLine("La durée moyenne de la playlist1 est de: " + playlist1.GetAverageDuration());
        }
    }
}
