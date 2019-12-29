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
using System.IO;

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
            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var writer = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\History.txt");
                writer.WriteLine(SongLists.Count); // số lượng bài hát
                writer.WriteLine(index);// bài hát đang phát
                writer.WriteLine(sTimerMusic.Value); // Time hát

                for (int i = 0; i < SongLists.Count; i++)
                {
                    writer.Write($"{SongLists[i].Path}");
                    writer.WriteLine("");
                }

                writer.Close();
            }
            catch { }
        }

        MediaPlayer _player = new MediaPlayer();

        DispatcherTimer _timer;

        private IKeyboardMouseEvents _hook;

        int index = 0;
        #region Binding

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
        #endregion
      
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
        private void PlayMusic_Click(object sender, RoutedEventArgs e) => Play(index);
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
            _timer.Start();
            isRunning = true;
            _player.Play();
        }
        private void NextSong_Click(object sender, RoutedEventArgs e) => NextSong(1); 
        void NextSong(int i )
        {
            try
            {
                if (isShuffe && !isLoopOne)
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
                if (lastIndex.Count > 1)
                {
                    int j = lastIndex[0];
                    SongLists[j].IconSong.Kind = PackIconKind.Play;
                    lastIndex.RemoveAt(0);
                }
                SongLists[index].IconSong.Kind = PackIconKind.Poll;
                PlaySelectedIndex(index);
            }
            catch { }
        }
        void Play(int i)
        {
            try
            {
                if (isShuffe && !isLoopOne)
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
                        
                    }
                    else
                        _player.Play();

                    piPlay.Kind = PackIconKind.PauseCircleOutline;
                    SongLists[index].IconSong.Kind = PackIconKind.Poll;
                    _timer.Start();
                }
                isPlay = !isPlay;
            }
            catch { }
        }
        private void BackSong_Click(object sender, RoutedEventArgs e)=> NextSong(-1);

        List<int> lastIndex = new List<int>();
        private void playSingleMusic(object sender, RoutedEventArgs e)
        {
            SongLists[index].IconSong.Kind = PackIconKind.Play;
            lastIndex.Remove(index);
            index = dgListSong.SelectedIndex;
            piPlay.Kind = PackIconKind.PauseCircleOutline;
            SongLists[index].IconSong.Kind = PackIconKind.Poll;
            PlaySelectedIndex(index);
            lastIndex.Add(index);

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
            if (isShuffe)
            {
                piShuffle.Kind = PackIconKind.ShuffleDisabled;
                piShuffle.Foreground = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                piShuffle.Kind = PackIconKind.Shuffle;
                piShuffle.Foreground = new SolidColorBrush(Colors.Orange);
            }
            isShuffe = !isShuffe;
        }

        #endregion

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
        int totaltime = 0;
        private void addPlayList(object sender, RoutedEventArgs e)
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Filter = "Music (.mp3)|*.mp3|ALL Files (*.*)|*.*";
            screen.Multiselect = true;
            try
            {
                if (screen.ShowDialog() == true)
                {
                    foreach (var item in screen.FileNames)
                    {
                        SongLists.Add(new SongMode(item));
                        SongLists[number].Number = number + 1;
                        totaltime += SongLists[number].Timetotal;
                        number++;
                    }
                    txbQuatitySong.Text = number + " Songs /";

                    int hour = totaltime / 3600;
                    int min = (totaltime - hour * 3600) / 60;
                    int sec = totaltime - hour * 3600 - min * 60;

                    txbTotalTime.Text = hour + ":" + min + ":" + sec;
                }
            }
            catch { }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            bool isS = false;
            if (dgListSong.SelectedIndex >= 0)
            {
                for (int i = dgListSong.SelectedItems.Count - 1; i >= 0; i--) 
                {
                    if (SongLists[index] == dgListSong.SelectedItems[i] as SongMode)
                    {
                        isS = true;
                    }
                    totaltime -= (dgListSong.SelectedItems[i] as SongMode).Timetotal;
                   SongLists.Remove(dgListSong.SelectedItems[i] as SongMode);
                }
            }
            if (isS)
            {
                lastIndex.Remove(index);
                var j = (new Random()).Next(SongLists.Count);
                index = j;
                PlaySelectedIndex(index);
                lastIndex.Add(index);
                SongLists[index].IconSong.Kind = PackIconKind.Poll;
                
            }
           
            for (int j = 0; j < SongLists.Count; j++)
            {
                SongLists[j].Number = j + 1;
                number++;
            }

            txbQuatitySong.Text = number + " Songs /";

            int hour = totaltime / 3600;
            int min = (totaltime - hour * 3600) / 60;
            int sec = totaltime - hour * 3600 - min * 60;

            txbTotalTime.Text = hour + ":" + min + ":" + sec;
            btnDelete.Visibility = Visibility.Hidden;


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timer_Tick;
            _hook = Hook.GlobalEvents();
            _hook.KeyUp += KeyUp_hook;
            btnDelete.Visibility = Visibility.Hidden;
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}PlayList"))  Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}PlayList");
            LoadListHistory();
            LoadWishList();
        }
        private void MouseUp_DataGrid(object sender, MouseButtonEventArgs e)
        {
            if (dgListSong.SelectedIndex >= 0) btnDelete.Visibility = Visibility.Visible;
        }
        
        #region Check File
        private void Check_Click(object sender, RoutedEventArgs e)  => uncheckCheckBoxes(sender);
        private void uncheckCheckBoxes(object sender)
        {
            if (checkTxt.IsChecked == true || checkXml.IsChecked == true)
            {
               System.Windows.Controls.CheckBox currentcheckbox = (System.Windows.Controls.CheckBox)sender;

                foreach (var checkBox in ManyCheckBoxes.Children.OfType<System.Windows.Controls.CheckBox>().Where(cb => (bool)cb.IsChecked))
                {
                    if (!currentcheckbox.Name.Equals(checkBox.Name))
                    {
                        checkBox.IsChecked = false;
                    }
                }
            }
        }
        #endregion

        #region Load/Save Play List
        private void Load_Click(object sender, RoutedEventArgs e) 
        {

            var screen = new Microsoft.Win32.OpenFileDialog();

            screen.InitialDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\";

            screen.Filter = "Xml (.xml)|*.xml|Txt (.txt)|*.txt|ALL Files (*.*)|*.*";
            try
            {
                if (screen.ShowDialog() == true)
                {
                    var filename = screen.SafeFileName;
                    LoadList(filename);
                }
            }
            catch { }
        }
        private void LoadWishList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                number = 0;
                totaltime = 0;
                SongLists.Clear();
                foreach (var item in WishLists)
                {
                    SongLists.Add(item);
                    totaltime += item.Timetotal;
                    number++;
                }
                txbQuatitySong.Text = number + " Songs /";

                int hour = totaltime / 3600;
                int min = (totaltime - hour * 3600) / 60;
                int sec = totaltime - hour * 3600 - min * 60;

                txbTotalTime.Text = hour + ":" + min + ":" + sec;
            }
            catch { }
        }
        private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs) { }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (txbNewName.Text.Length > 0 && SongLists.Count > 0)
            {
                string filename = $"{txbNewName.Text.ToString()}.";
                if (checkTxt.IsChecked == true) filename += "txt";
                else filename += "xml";
                SaveList(filename);
            }
        }
        private void SaveList (string filename)
        {
            try
            {
                var writer = new StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\{filename}");
                writer.WriteLine(SongLists.Count);
                for (int i = 0; i < SongLists.Count; i++)
                {
                    writer.Write($"{SongLists[i].Path}");
                    writer.WriteLine("");
                }
                writer.Close();
            }
            catch { }
        }

        private List<SongMode> WishLists = new List<SongMode>();
        private void LoadList(string filename)
        {
            try
            {
                var reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\{filename}");
                var firstLine = reader.ReadLine();
                int Length = int.Parse(firstLine);
                SongLists.Clear();
                totaltime = 0;
                number = 0;
                for (int i = 0; i < Length; i++)
                {
                    firstLine = reader.ReadLine();
                    
                    SongLists.Add(new SongMode(firstLine));
                    SongLists[number].Number = number + 1;
                    
                    totaltime += SongLists[number].Timetotal;
                    number++;
                    
                }
                txbQuatitySong.Text = number + " Songs /";

                int hour = totaltime / 3600;
                int min = (totaltime - hour * 3600) / 60;
                int sec = totaltime - hour * 3600 - min * 60;

                txbTotalTime.Text = hour + ":" + min + ":" + sec;
            }
            catch { }
        }
        private void LoadListHistory()
        {
            try
            {
                var reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\History.txt");
                var firstLine = reader.ReadLine();
                int Length = int.Parse(firstLine);
                firstLine = reader.ReadLine();
                index = int.Parse(firstLine);
                firstLine = reader.ReadLine();
                int timePlay = int.Parse(firstLine);
              
                SongLists.Clear();
                totaltime = 0;
                number = 0;
                for (int i = 0; i < Length; i++)
                {
                    firstLine = reader.ReadLine();

                    SongLists.Add(new SongMode(firstLine));
                    SongLists[number].Number = number + 1;
                    if(number == index)
                    {
                        isRunning = !isRunning;
                        PlaySelectedIndex(index);
                        _player.Pause();
                        _timer.Stop();
                        timeEnd.Text = SongLists[index].Time;
                        int h = timePlay / 3600;
                        int minutes = (timePlay - 3600 * h) / 60;
                        int seconds = timePlay - 3600 * h - 60 * minutes;
                        var testDuration = new TimeSpan(h, minutes, seconds);
                        _player.Position = testDuration;
                        sTimerMusic.Value = timePlay;
                        timeStart.Text = _player.Position.ToString(@"mm\:ss");
                    }
                    totaltime += SongLists[number].Timetotal;
                    number++;

                }
                txbQuatitySong.Text = number + " Songs /";

                int hour = totaltime / 3600;
                int min = (totaltime - hour * 3600) / 60;
                int sec = totaltime - hour * 3600 - min * 60;

                txbTotalTime.Text = hour + ":" + min + ":" + sec;
            }
            catch { }
        }
        private void LoadWishList()
        {
            try
            {
                var reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}PlayList\\WishList.txt");
                var firstLine = reader.ReadLine();
                int Length = int.Parse(firstLine);
                WishLists.Clear();
                number = 0;
                for (int i = 0; i < Length; i++)
                {
                    firstLine = reader.ReadLine();
                    WishLists.Add(new SongMode(firstLine));
                    WishLists[i].IsFav = true;
                    WishLists[i].Number = i + 1;

                }

            }
            catch { }
        }
        #endregion
        private void Wish_Click(object sender, RoutedEventArgs e)
        {
            int indexWish = dgListSong.SelectedIndex;

            try
            {
                if (!SongLists[indexWish].IsFav)
                {
                    WishLists.RemoveAt(indexWish);
                }
                else
                {
                    WishLists.Add(SongLists[indexWish]);
                    WishLists[WishLists.Count - 1].Number = WishLists.Count;
                }
            }
            catch { }
        }
        private void UnWish_Click(object sender, RoutedEventArgs e)
        {
            int indexWish = dgListSong.SelectedIndex;

            try
            {
                if (SongLists[indexWish].IsFav)
                {
                    WishLists.RemoveAt(indexWish);
                }
                else
                {
                    WishLists.Add(SongLists[indexWish]);
                    WishLists[WishLists.Count - 1].Number = WishLists.Count;
                }
            }
            catch { }
        }

    }

}
