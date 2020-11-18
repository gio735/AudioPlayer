using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class Program
    {

        static async Task Main(string[] args)
        {
            bool active = true;
            Task display = Task.Run(async () =>
            {
                while (active)
                {
                    await Display.GetScreen();
                }      
            }
            );
            Task control = Task.Run(() =>
            {
                while (active)
                {
                    string operation = Console.ReadLine().Trim().ToLower().Replace(" ", "");
                    int index = 0;
                    bool wasParsed = int.TryParse(operation, out index);
                    Commands.TryParse(operation, true, out Commands enumOperation);

                    if (Display.Mode == ScreenModes.List)
                    {
                        CommandsOfList(enumOperation, wasParsed, index);
                    }
                    else if (Display.Mode == ScreenModes.AudioPlaying)
                    {
                        Display.ToPause();
                    }
                    else if (Display.Mode == ScreenModes.AudioPaused)
                    {
                        if (enumOperation == Commands.Exit)
                        {
                            active = false;
                            Display.ToIdle(); 
                        }
                        else
                        {
                            CommandsOfPaused(enumOperation, operation);
                        }
                       
                    }
                    else if (Display.Mode == ScreenModes.Rewind)
                    {
                        if (wasParsed)
                        {
                            Rewind(index);
                            Display.ToPlay();
                        }
                        else
                        {
                            Console.CursorLeft = 0;
                            Console.CursorTop--;
                            Console.Write("                                   ");
                            Console.CursorLeft = 0;
                        }
                    }
                    
                }
            }
            );
            await Task.WhenAll(display, control);    
        }

        public static void CommandsOfPaused(Commands enumOperation, string operation)
        {
            if (operation == "")
            {
                Display.ToPlay();
            }
            else if (enumOperation == Commands.Back)
            {
                Display.ToList();
            }
            else if (enumOperation == Commands.Next)
            {
                if (Display.IndexOfCurrentAudio < AudioList.Count)
                {
                    Display.IndexOfCurrentAudio++;
                    Display.CurrentTime = 0;
                    Display.ToPlay();
                    Console.Clear();
                }
                else
                {
                    Display.IndexOfCurrentAudio = 1;
                    Display.CurrentTime = 0;
                    Display.ToPlay();
                    Console.Clear();
                }
            }
            else if (enumOperation == Commands.Prev)
            {
                if (Display.IndexOfCurrentAudio > 1)
                {
                    Display.IndexOfCurrentAudio--;
                    Display.CurrentTime = 0;
                    Display.ToPlay();
                    Console.Clear();
                }
                else
                {
                    Display.IndexOfCurrentAudio = AudioList.Count;
                    Display.CurrentTime = 0;
                    Display.ToPlay();
                    Console.Clear();
                }
            }
            else if (enumOperation == Commands.Rewind)
            {
                Console.Clear();
                Console.WriteLine("How much second u want to rewind? (negative to rewind back)");
                Display.ToRewind();

            }
            else
            {
                Console.CursorLeft = 0;
                Console.CursorTop--;
                Console.Write(new string(' ', Console.BufferWidth - 1));
                Console.CursorLeft = 0;
            }
        }
        public static void CommandsOfList(Commands enumOperation, bool wasParsed, int index)
        {
            bool songExist = wasParsed && AudioList.Count >= index && index > 0;
            if (songExist)
            {
                PlayAudio(index);
            }
            else if (enumOperation == Commands.AddSong)
            {
                SongAdder();
            }
            else if( enumOperation == Commands.AddMovie)
            {
                MovieAdder();
            }
            else
            {
                Console.CursorLeft = 0;
                Console.CursorTop--;
                Console.Write(new string(' ', Console.BufferWidth - 1));
                Console.CursorLeft = 0;
            }
        }
        public static void PlayAudio(int index)
        {
            Display.IndexOfCurrentAudio = (int)index;
            Display.CurrentTime = 0;
            Display.ToPlay();
        }
        public static void SongAdder()
        {
            Display.ToIdle();
            bool takingTime = true;

            int time = 0;

            string artist = "Unknown";
            string name = "Unknown";

            Console.Clear();
            Console.WriteLine("How long song is? (seconds)");
            while (takingTime)
            {
               
                bool parsed = int.TryParse(Console.ReadLine(), out time);
                if (parsed && time > 0)
                {
                    takingTime = false;
                }
                else
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop--;
                    Console.Write(new string(' ', Console.BufferWidth - 1));
                    Console.CursorLeft = 0;
                }
            }
            Console.Clear();
            Console.WriteLine("Whats artist name? Just press enter if unknown.");
            string artistChoice = Console.ReadLine();
            if (artistChoice != "")
            {
                artist = artistChoice;
            }
            Console.Clear();
            Console.WriteLine("Whats song name? Just press enter if unknown.");
            string songChoice = Console.ReadLine();
            if (songChoice != "")
            {
                name = songChoice;
            }
            Console.Clear();
            AudioList.Songs.Add(new Song(time, artist, name));
            Display.ToList();
        }
        public static void MovieAdder()
        {
            Display.ToIdle();
            bool takingTime = true;

            int time = 0;

            string director = "Unknown";
            string name = "Unknown";

            Console.Clear();
            Console.WriteLine("How long movie is? (seconds)");
            while (takingTime)
            {

                bool parsed = int.TryParse(Console.ReadLine(), out time);
                if (parsed && time > 0)
                {
                    takingTime = false;
                }
                else
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop--;
                    Console.Write(new string(' ', Console.BufferWidth - 1));
                    Console.CursorLeft = 0;
                }
            }
            Console.Clear();
            Console.WriteLine("Whats director name? Just press enter if unknown.");
            string artistChoice = Console.ReadLine();
            if (artistChoice != "")
            {
                director = artistChoice;
            }
            Console.Clear();
            Console.WriteLine("Whats movie name? Just press enter if unknown.");
            string songChoice = Console.ReadLine();
            if (songChoice != "")
            {
                name = songChoice;
            }
            Console.Clear();
            AudioList.Movies.Add(new Movie(time, director, name));
            Display.ToList();
        }

        public static void Rewind(int amount)
        {
            foreach (Song song in AudioList.Songs)
            {
                if (song.ID == Display.IndexOfCurrentAudio)
                {
                    Display.CurrentTime += amount;
                    if (Display.CurrentTime > song.Time)
                    {
                        Display.CurrentTime = song.Time - 1;
                        Display.ToPlay();
                        return;
                    }
                    if (Display.CurrentTime < 0)
                    {
                        Display.CurrentTime = 0;
                        Display.ToPlay();
                        return;
                    }
                }
            }
            foreach (Movie movie in AudioList.Movies)
            {
                if (movie.ID == Display.IndexOfCurrentAudio)
                {
                    Display.CurrentTime += amount;
                    if (Display.CurrentTime > movie.Time)
                    {
                        Display.CurrentTime = movie.Time - 1;
                        Display.ToPlay();
                        return;
                    }
                    if (Display.CurrentTime < 0)
                    {
                        Display.CurrentTime = 0;
                        Display.ToPlay();
                        return;
                    }
                }
            }
        }

        public enum Commands
        {
            None,
            AddSong,
            AddMovie,
            Back,
            Next,
            Prev,
            Rewind,
            Exit
        }
        
    }
}
