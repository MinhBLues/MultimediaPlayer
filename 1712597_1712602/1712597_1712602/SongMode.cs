using System;
using System.Collections.Generic;
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

        private Brush brush = Brushes.Transparent;
        public SongMode() { }

        public string Name { get => name; set => name = value; }
        public string Time { get => time; set => time = value; }
        public string Artist { get => artist; set => artist = value; }
        public string Album { get => album; set => album = value; }
        public string Title { get => title; set => title = value; }
        public string Path { get => path; set => path = value; }
        public Brush Brush { get => brush; set => brush = value; }

        private Random rnd = new Random();
        public SongMode(string path)
        {

            this.path = path;
            TagLib.File tagFile = TagLib.File.Create(path);
            this.artist = tagFile.Tag.FirstAlbumArtist;
            this.album = tagFile.Tag.Album;
            this.title = tagFile.Tag.Title;
            this.time = tagFile.Properties.Duration.ToString(@"mm\:ss");
            if(this.title == null)
            {
                this.title = name;
            }

            Color randomColor = Color.FromArgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
            this.brush = new SolidColorBrush(randomColor);
        }
        public SongMode(string name, string time)
        {
            this.name = name;
            this.time = time;
        }
    }
}
