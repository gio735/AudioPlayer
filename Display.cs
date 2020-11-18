using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class Display
    {
        public static ScreenModes Mode { get; private set; }

        public static int CurrentTime { get; set; }
        public const int secInMin = 60;
        public static int IndexOfCurrentAudio { get; set; }
        public static void GetAudioInfo(int index)
        { 
            foreach (Song song in AudioList.Songs)
            {
                if (song.ID == index + 1)
                {
                    Console.WriteLine($"{index + 1}) \n{song.Type}:\n");
                    Console.WriteLine($"Artist: {song.Artist}.\n");
                    Console.WriteLine($"Song: {song.Name}.\n");
                    Console.WriteLine(new string('-', 80));
                    return;
                }
            }
            foreach (Movie movie in AudioList.Movies)
            {
                if (movie.ID == index + 1)
                {
                    Console.WriteLine($"{index + 1}) \n{movie.Type}:\n");
                    Console.WriteLine($"Director: {movie.Director}.\n");
                    Console.WriteLine($"Song: {movie.Name}.\n");
                    Console.WriteLine(new string('-', 80));
                    return;
                }

            }

            
        }
        public static async Task PlayAudio()
        {  
            while (Display.Mode == ScreenModes.AudioPlaying)
            {
                foreach (Song song in AudioList.Songs)
                {
                    if (song.ID == IndexOfCurrentAudio)
                    {
                        Console.Clear();
                        int sec = song.Time;
                        int min = sec / secInMin;
                        sec -= min * secInMin;
                        int currentMin = CurrentTime / secInMin;
                        int currentSec = CurrentTime - currentMin * secInMin;
                        Console.WriteLine(new string('-', 80));
                        Console.WriteLine($"[Playing - {song.Type}]\n");
                        Console.WriteLine($"Artist: {song.Artist}\n");
                        Console.WriteLine($"Song; {song.Name}\n");
                        Console.WriteLine($"{currentMin}:{currentSec} / {min}:{sec}");
                        Console.WriteLine(new string('-', 80));
                        Console.WriteLine("                              Press enter to pause.");
                        await Task.Delay(1000);
                        CurrentTime++;
                        bool songFinished = sec == currentSec && min == currentMin;
                        if (songFinished)
                        {
                            if (IndexOfCurrentAudio < AudioList.Count)
                            {
                                IndexOfCurrentAudio++;
                                CurrentTime = 0;
                                Console.Clear();
                                Console.WriteLine("Starting next audio");
                                await Task.Delay(3000);
                            }
                            else
                            {
                                IndexOfCurrentAudio = 1;
                                CurrentTime = 0;
                                Console.Clear();
                                Console.WriteLine("Starting next audio");
                                await Task.Delay(3000);
                            }
                        }
                    }
                }
                foreach (Movie movie in AudioList.Movies)
                {
                    if (movie.ID == IndexOfCurrentAudio)
                    {
                        if (movie.ID == IndexOfCurrentAudio)
                        {
                            Console.Clear();
                            int sec = movie.Time;
                            int min = sec / secInMin;
                            sec -= min * secInMin;
                            int currentMin = CurrentTime / secInMin;
                            int currentSec = CurrentTime - currentMin * secInMin;
                            Console.WriteLine(new string('-', 80));
                            Console.WriteLine($"[Playing - {movie.Type}]\n");
                            Console.WriteLine($"Director: {movie.Director}\n");
                            Console.WriteLine($"Song; {movie.Name}\n");
                            Console.WriteLine("        _______________________________________________");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                       __                      |");
                            Console.WriteLine("       |                 _____|  |___                  |");
                            Console.WriteLine("       |                |      __    |                 |");
                            Console.WriteLine("       |                |     |__|   |                 |");
                            Console.WriteLine("       |                |____________|                 |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |                                               |");
                            Console.WriteLine("       |_______________________________________________|");
                            Console.WriteLine($"                        {currentMin}:{currentSec} / {min}:{sec}");
                            Console.WriteLine("       =================================================");
                            Console.WriteLine($"");
                            Console.WriteLine(new string('-', 80));
                            Console.WriteLine("                              Press enter to pause.");
                            await Task.Delay(1000);
                            CurrentTime++;
                            bool songFinished = sec == currentSec && min == currentMin;
                            if (songFinished)
                            {
                                if (IndexOfCurrentAudio < AudioList.Count)
                                {
                                    IndexOfCurrentAudio++;
                                    CurrentTime = 0;
                                    Console.Clear();
                                    Console.WriteLine("Starting next audio");
                                    await Task.Delay(3000);
                                }
                                else
                                {
                                    IndexOfCurrentAudio = 1;
                                    CurrentTime = 0;
                                    Console.Clear();
                                    Console.WriteLine("Starting next audio");
                                    await Task.Delay(3000);
                                }
                            }
                        }
                    }
                }

                

            }
        }
        public static async Task Pause()
        {
            foreach (Song song in AudioList.Songs)
            {
                if (song.ID == IndexOfCurrentAudio)
                {
                    int sec = song.Time;
                    int min = sec / 60;
                    sec -= min * 60;
                    int currentMin = CurrentTime / 60;
                    int currentSec = CurrentTime - currentMin * 60;
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("[Paused]\n");
                    Console.WriteLine($"Artist: {song.Artist}\n");
                    Console.WriteLine($"Song; {song.Name}\n");
                    Console.WriteLine($"{currentMin}:{currentSec} / {min}:{sec}");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("back - returns u to list.    |   next - starts next audio. \nprev - starts previous audio. |   exit - end program.\nrewind - change some numbers\n                Press enter to continue song.");
                    while (Mode == ScreenModes.AudioPaused)
                    {
                        await Task.Delay(500);
                    }
                }
            }
            foreach (Movie movie in AudioList.Movies)
            {
                if (movie.ID == IndexOfCurrentAudio)
                {
                    int sec = movie.Time;
                    int min = sec / 60;
                    sec -= min * 60;
                    int currentMin = CurrentTime / 60;
                    int currentSec = CurrentTime - currentMin * 60;
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("[Paused]\n");
                    Console.WriteLine($"Director: {movie.Director}\n");
                    Console.WriteLine($"Song; {movie.Name}\n");
                    Console.WriteLine("        _______________________________________________");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                       __                      |");
                    Console.WriteLine("       |                 _____|  |___                  |");
                    Console.WriteLine("       |                |      __    |                 |");
                    Console.WriteLine("       |                |     |__|   |                 |");
                    Console.WriteLine("       |                |____________|                 |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine("       |                                               |");
                    Console.WriteLine($"                        |{currentMin}:{currentSec} / {min}:{sec}|");
                    Console.WriteLine("       =================================================");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("back - returns u to list.    |   next - starts next audio. \nprev - starts previous audio. |   exit - end program.\nrewind - change some numbers\n                Press enter to continue song.");
                    while (Mode == ScreenModes.AudioPaused)
                    {
                        await Task.Delay(500);
                    }
                }
            }
            
                
        }
        public static async Task GetScreen()
        {
            bool emptyList = AudioList.Count == 0 && Mode == ScreenModes.List;
            if (emptyList)
            {
                
                Console.Clear();
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("                            No audio have been found");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("              Write \"add song\" / \"add movie\" to add new song/movie");
                while (Mode == ScreenModes.List)
                {
                    await Task.Delay(500);
                }
                
            }
            else if (Mode == ScreenModes.List)
            {
                Console.Clear();
                Console.WriteLine(new string('-', 80));
                for (int index = 0; index < AudioList.Count; index++)
                {
                    GetAudioInfo(index);
                }
                Console.WriteLine("Write \"add song\" / \"add movie\" to add new song/movie or index of song/movie to play it. \"exit\" to end program.");
                while (Mode == ScreenModes.List)
                {
                    await Task.Delay(500);
                }
            }
            else if (Mode == ScreenModes.AudioPlaying)
            {
                await PlayAudio();
                
            }
            else if (Mode == ScreenModes.AudioPaused)
            {
                await Pause();
            }
            else
            {
                while (Mode == ScreenModes.Idle)
                {
                    await Task.Delay(500);
                }
            }
            
        }
        public static void ToList()
        {
            Mode = ScreenModes.List;
        }
        public static void ToPlay()
        {
            Mode = ScreenModes.AudioPlaying;
        }
        public static void ToPause()
        {
            Mode = ScreenModes.AudioPaused;
        }
        public static void ToIdle()
        {
            Mode = ScreenModes.Idle;
        }
        public static void ToRewind()
        {
            Display.Mode = ScreenModes.Rewind;
        }

    }
    public enum ScreenModes
    {
        List,
        AudioPlaying,
        AudioPaused,
        Idle,
        Rewind
    }
}
