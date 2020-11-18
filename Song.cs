using System;
using System.Collections.Generic;
using System.Text;


namespace AudioPlayer
{
    public class Song
    {
        public int ID { get; private set; }

        public AudioList.Type Type { get; private set; }
        public string Artist { get; private set; }
        public string Name { get; private set; }
        public int Time { get; private set; }

        public Song(int time, string artist = "Unknown", string name = "Unknown")
        {
            ID = AudioList.GiveID();
            Artist = artist;
            Name = name;
            Time = time;
            Type = AudioList.Type.Song;
        }
    }
}
