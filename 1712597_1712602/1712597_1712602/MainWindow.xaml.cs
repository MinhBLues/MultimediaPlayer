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
            _player.MediaEnded += _player_MediaEnded;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;
            
        }
        MediaPlayer _player = new MediaPlayer();

        DispatcherTimer _timer;

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
                txbVol.Text = ((int)(vol.Value*100)).ToString();
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

        private void addPlayList(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Filter = "Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var item in screen.FileNames)
                {
                    SongLists.Add(new SongMode(item));
                }
            }
        }
        bool isPlay = false;
        bool isRunning = false;
        private void playMusic(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isPlay)
                {
                    _player.Pause();
                    piPlay.Kind = PackIconKind.PlayCircleOutline;
                    _timer.Stop();
                    // _lastIndex++;
                }
                else
                {
                    if (!isRunning)
                    {
                        PlaySelectedIndex(0);
                    }
                    else
                        _player.Play();

                    piPlay.Kind = PackIconKind.PauseCircleOutline;
                    _timer.Start();
                }
                isPlay = !isPlay;
            }
            catch { }
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

            isRunning = true;
            _player.Play();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (_player.Source != null)
            {
                var currentPos = _player.Position.ToString(@"mm\:ss");
                var duration = _player.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                timeStart.Text = currentPos;
                sTimerMusic.Value += 1;
            }
            else
                Title = "No file selected...";
        }
        private void _player_MediaEnded(object sender, EventArgs e)
        {
          //  _lastIndex++;
            PlaySelectedIndex(0);
        }

        private bool mouseCaptured = false;
        private void MouseMove(object sender, MouseEventArgs e)
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

        private void nextSong(object sender, RoutedEventArgs e)
        {
            index++;
            if (index > SongLists.Count - 1) index = 0;

            PlaySelectedIndex(index);
               
        }

        private void backSong(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0) index = SongLists.Count - 1;

            PlaySelectedIndex(index);
        }
    }
}
