using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace _1712597_1712602
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;

            _hook = Hook.GlobalEvents();
            _hook.KeyUp += KeyUp_hook;

        }
        MediaPlayer _player = new MediaPlayer();

        DispatcherTimer _timer;

        private IKeyboardMouseEvents _hook;

        int index = 0;

        private ObservableCollection<SongMode> SongLists = new ObservableCollection<SongMode>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<SongMode> Playlists
        {
            get => SongLists;
            set
            {
                SongLists = value;
                NotifyPropertyChanged("Playlists");
            }
        }

        private void volumeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                txbVol.Text = ((int)(vol.Value * 100)).ToString();
                if (vol.Value == 0)
                {
                    piVol.Kind = PackIconKind.VolumeOff;
                }
                else
                {
                    if (vol.Value < 0.5)
                    {
                        piVol.Kind = PackIconKind.VolumeMedium;
                    }
                    else
                        piVol.Kind = PackIconKind.VolumeHigh;
                }
                _player.Volume = e.NewValue;
            }
            catch { }
        }

        int number = 0;
        private void addPlayList(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Filter = "Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var item in screen.FileNames)
                {
                    SongLists.Add(new SongMode(item));
                    SongLists[number].Number = number + 1;
                    number++;
                }
            }
        }

        bool isPlay = false;

        bool isRunning = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (_player.Source != null)
            {
                var currentPos = _player.Position.ToString(@"mm\:ss");
                var duration = _player.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                timeStart.Text = currentPos;
                sTimerMusic.Value += 1;
                if (sTimerMusic.Value == sTimerMusic.Maximum)
                {
                    if (isLoopOff && index == SongLists.Count - 1)
                    {
                        _player.Stop();
                        _timer.Stop();
                    }
                    else
                    {
                        if (isLoopInf) NextSong(1);
                        else if (isLoopOne) NextSong(0);
                        else NextSong(1);
                    }
                }
            }
            else
                Title = "No file selected...";
        }

        #region Silder Timer
        private bool mouseCaptured = false;
        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mouseCaptured)
            {
                var x = e.GetPosition(sTimerMusic).X;
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseCaptured = true;
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseCaptured = false;
            var x = (int)e.GetPosition(sTimerMusic).X;
            x = (int)(x * (float)sTimerMusic.Maximum / 300);
            sTimerMusic.Value = x;
            int hour = x / 3600;
            int minutes = (x - 3600 * hour) / 60;
            int seconds = x - 3600 * hour - 60 * minutes;
            var testDuration = new TimeSpan(hour, minutes, seconds);
            _player.Position = testDuration;
        }

        #endregion

        #region Play Music
        private void PlayMusic_Click(object sender, RoutedEventArgs e)
        {
            Play(index);
        }
        private void PlaySelectedIndex(int i)
        {

            string filename = SongLists[i].Path;

            _player.Open(new Uri(filename, UriKind.Absolute));

            System.Threading.Thread.Sleep(500);
            var duration = _player.NaturalDuration.TimeSpan;

            _player.Position = new TimeSpan(0, 0, 0);
            timeEnd.Text = SongLists[i].Time;
            sTimerMusic.Maximum = duration.Hours * 60 * 60 + duration.Minutes * 60 + duration.Seconds;
            sTimerMusic.Value = 0;

            txbSongTitle.Text = SongLists[i].Title;
            txbNameArtist.Text = SongLists[i].Artist;

            piPlay.Kind = PackIconKind.PauseCircleOutline;
            _timer.Start();
            isRunning = true;
            _player.Play();
        }
        private void NextSong_Click(object sender, RoutedEventArgs e)
        {
            NextSong(1);

        }
        void NextSong(int i )
        {
            
            if (isShuffe)
            {
                Random r = new Random();
                index = r.Next(SongLists.Count - 1);
            }
            else
            {
                index += i;
                if (index > SongLists.Count - 1) index = 0;
                if (index < 0) index = SongLists.Count - 1;
            }
            lastIndex.Add(index);
            SongLists[index].IconSong.Kind = PackIconKind.Poll;
            if (lastIndex.Count > 1)
            {
                int j = lastIndex[0];
                SongLists[j].IconSong.Kind = PackIconKind.PlayCircleOutline;
                lastIndex.RemoveAt(0);
            }
            PlaySelectedIndex(index);
        }
        void Play(int i)
        {
            if (isShuffe)
            {
                Random r = new Random();
                index = r.Next(SongLists.Count - 1);
            }
            else index = i;


            if (isPlay)
            {
                _player.Pause();
                piPlay.Kind = PackIconKind.PlayCircleOutline;
                _timer.Stop();
            }
            else
            {
                if (!isRunning)
                {
                    PlaySelectedIndex(index);
                    lastIndex.Add(index);
                    SongLists[index].IconSong.Kind = PackIconKind.Poll;
                }
                else
                    _player.Play();
            }
            isPlay = !isPlay;

        }
    
        private void BackSong_Click(object sender, RoutedEventArgs e)
        {
            NextSong(-1);
        }

        List<int> lastIndex = new List<int>();

        private void playSingleMusic(object sender, RoutedEventArgs e)
        {
            index = dgListSong.SelectedIndex;
            bool i = false;
            if (isShuffe) isShuffe = i = false;
            NextSong(0);
            if (i) isShuffe = true;
        }
        #endregion

        #region Hook
        private void KeyUp_hook(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.P)) Play(index);
            if (e.Alt && (e.KeyCode == Keys.N)) NextSong(1);
            if (e.Alt && (e.KeyCode == Keys.B)) NextSong(-1);
        }

        #endregion

        #region Loop Music

        bool isLoopOne = false;
        bool isLoopInf = true;
        bool isLoopOff = false;
        private void Loop_Click(object sender, RoutedEventArgs e)
        {
            if (isLoopInf)
            {
                piRepeat.Kind = PackIconKind.RepeatOne;
                isLoopOne = true;
                isLoopInf = false;
            }
            else
            {
                if (isLoopOne)
                {
                    piRepeat.Kind = PackIconKind.RepeatOff;
                    isLoopOne = false;
                    isLoopOff = true;
                }
                else
                {
                    piRepeat.Kind = PackIconKind.Repeat;
                    isLoopOff = false;
                    isLoopInf = true;
                }
            }

        }

        #endregion

        #region Shuffe
        bool isShuffe = false;
        private void Shuffle_Click(object sender, RoutedEventArgs e)

        {

            isShuffe = !isShuffe;
        }

        #endregion
    }

}
