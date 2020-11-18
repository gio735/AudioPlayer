using System;
using System.Collections.Generic;
using System.Text;

namespace AudioPlayer
{
    public class Movie
    {
        public int ID { get; private set; }
        public AudioList.Type Type { get; private set; }
        public string Director { get; private set; }
        public string Name { get; private set; }
        public int Time { get; private set; }

        public Movie(int time, string director = "Unknown", string name = "Unknown")
        {
            ID = AudioList.GiveID();
            Director = director;
            Name = name;
            Time = time;
            Type = AudioList.Type.Movie;
        }
    }
}
