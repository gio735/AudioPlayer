using System;
using System.Collections.Generic;
using System.Text;

namespace AudioPlayer
{
    public static class AudioList
    {
        public static int IdMaker { get; private set; }
        public static int Count => Songs.Count + Movies.Count;
        public static List<Song> Songs { get; private set; }
        public static List<Movie> Movies { get; private set; }

        static AudioList()
        {
            Songs = new List<Song>();
            Movies = new List<Movie>();
        }
        public static int GiveID()
        {
            return ++IdMaker;
        }

        public enum Type
        {
            Song,
            Movie
        }
    }
}
