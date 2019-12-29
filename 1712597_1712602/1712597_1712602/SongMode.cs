using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _1712597_1712602
{
    public class SongMode
    {
        private string path;
        private string name;
        private string time;
        private string artist;
        private string album;
        private string title;
        private int number;
        private PackIcon iconSong;
        private bool isFav;

        private Brush brush = Brushes.Transparent;
        public SongMode() { }

        public string Name { get => name; set => name = value; }
        public string Time { get => time; set => time = value; }
        public string Artist { get => artist; set => artist = value; }
        public string Album { get => album; set => album = value; }
        public string Title { get => title; set => title = value; }
        public string Path { get => path; set => path = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public int Number { get => number; set => number = value; }
        public PackIcon IconSong { get => iconSong; set => iconSong = value; }
        public int Timetotal { get => timetotal; set => timetotal = value; }
        public bool IsFav { get => isFav; set => isFav = value; }

        private Random rnd = new Random();

        private int timetotal;
        public SongMode(string path)
        {

            this.path = path;
            var fullname = new FileInfo(path);
            var converter = new NameConvert();
            this.name =  converter.Convert(fullname.Name, null, null, null).ToString();

            TagLib.File tagFile = TagLib.File.Create(path);
            this.artist = tagFile.Tag.FirstArtist;
            this.album = tagFile.Tag.Album;
            this.title = tagFile.Tag.Title;
            this.time = tagFile.Properties.Duration.ToString(@"mm\:ss");
            this.timetotal = tagFile.Properties.Duration.Hours * 3600 + tagFile.Properties.Duration.Minutes * 60 + tagFile.Properties.Duration.Seconds;
            if (this.title == null)
            {
                this.title = name;
            }

            Color randomColor = Color.FromArgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
            this.brush = new SolidColorBrush(randomColor);

            iconSong = new PackIcon();
            iconSong.Kind = PackIconKind.Play;

            this.isFav = false;
        }
        public SongMode(string name, string time)
        {
            this.name = name;
            this.time = time;
        }
    }
}
