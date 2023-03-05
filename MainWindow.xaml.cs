using Microsoft.Win32;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System;
using System.Collections.Generic;


namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        int frame = 1;
        
        public MainWindow()
        {
            InitializeComponent();

            Playerr.Volume = 0.1;
            SliderVolume.Value = 10;
            timer.Tick += new EventHandler(Update);
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer2.Tick += new EventHandler(RunningName);
            timer2.Interval = TimeSpan.FromMilliseconds(500);
            Playerr.MediaEnded += ButtonNext_Click;

            MouseWheel += SliderVolume_MouseWheel;


        }

        public void Update(object sender, EventArgs e)
        {

            if (Playerr.Position.TotalMilliseconds > 100)
            {
                try
                {
                    SliderSong.Maximum = Playerr.NaturalDuration.TimeSpan.TotalSeconds;

                    tomer.Content = Playerr.Position.ToString(@"mm\:ss");

                }
                catch (Exception q) { MessageBox.Show(q.Message); }
            }
            SliderSong.Value = Playerr.Position.TotalSeconds;
            Img.X = 120 + frame * 90;
            Img.Y = 200;
            if (frame < 7)
            {
                frame++;
            }
            else
            {
                frame = 1;
            }
            if (Playerr.Source == null)
            {
                timer.Stop();
            }
        }

        public void RunningName(object sender, EventArgs e)
        {
            if (One.Content.ToString().Count() > 0)
            {
                One.Content = One.Content.ToString() + One.Content.ToString()[0];
                One.Content = One.Content.ToString().Remove(0, 1);
            }
        }
        private void SliderSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderSong.SelectionEnd = Convert.ToInt32(e.NewValue);
            Playerr.Position = TimeSpan.FromSeconds(SliderSong.Value);

        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderVolume.SelectionEnd = Convert.ToInt32(e.NewValue);
            Playerr.Volume = e.NewValue / 100;


        }
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        List<string> Songs = new List<string>();

        private void ButtonAddSong_Click(object sender, RoutedEventArgs e)
        {

            //OpenFileDialog openFileDialog2 = new OpenFileDialog();

            openFileDialog1.Filter = "mp3 files (*.mp3)|*.mp3";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();//ne propadai


            

            if (openFileDialog1.FileNames.Length > 0)
            {


                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {

                    Songs.Add(openFileDialog1.FileNames[i]);

                    //MessageBox.Show(openFileDialog1.FileName.ToString());
                    BoxTt.Items.Add(openFileDialog1.SafeFileNames[i]);
                }
            }
            else
            {
                return;
            }

            //MessageBox.Show(openFileDialog1.SafeFileName);


        }


        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {

            if (BoxTt.SelectedIndex != -1)
            {
                Playerr.Source = new Uri(Songs[BoxTt.SelectedIndex].ToString());
                Playerr.Play();
                timer.Start();
                timer2.Stop();
                timer2.Start();
                One.Content = BoxTt.Items[BoxTt.SelectedIndex];
            }

        }

        private void TexBoxPlaylist_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        bool b = true;

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Playerr.Source!=null)
                {
                    if (b == true)
                    {
                        if (BoxTt.SelectedIndex != -1)
                        {
                            //Playerr.Source = new Uri(Songs[BoxTt.SelectedIndex].ToString());

                            Playerr.Pause();
                            timer.Stop();
                            timer2.Stop();

                            //One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                            b = false;
                        }
                    }
                    else
                    {
                        if (BoxTt.SelectedIndex != -1)
                        {


                            Playerr.Play();
                            timer.Start();

                            timer2.Start();
                            //One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                            b = true;

                        }
                    }
                }
                


            }
            catch (Exception q) { MessageBox.Show(q.Message); }

        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Playerr.Source = null;
            timer2.Stop();

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
           
            if (Songs.Count == 0)
            {
                Playerr.Source = null;
                timer.Stop();
                timer2.Stop();
                One.Content = "";

                tomer.Content = Playerr.Position.ToString(@"mm\:ss");

                SliderSong.Value = 0;


                return;
            }
            else
            {
                if (BoxTt.SelectedIndex + 1 == BoxTt.Items.Count)
                {
                    //MessageBox.Show("next");
                    Playerr.Source = new Uri(Songs[0].ToString());
                    BoxTt.SelectedIndex = 0;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                }
                else if (BoxTt.SelectedIndex + 1 < BoxTt.Items.Count)
                {
                    Playerr.Source = new Uri(Songs[BoxTt.SelectedIndex + 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex + 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                }
            }


        }

        private void ButtonDelSong_Click(object sender, RoutedEventArgs e)
        {
            if(Songs.Count>0)
            {
                int p = BoxTt.SelectedIndex;
                Songs.RemoveAt(BoxTt.SelectedIndex);
                BoxTt.Items.RemoveAt(BoxTt.SelectedIndex);
                BoxTt.SelectedIndex = p - 1;
                timer.Start();
                if (Songs.Count == 0)
                {
                    Playerr.Source = null;
                    timer.Stop();
                    timer2.Stop();
                    One.Content = "";
                    
                    tomer.Content = Playerr.Position.ToString(@"mm\:ss");

                    SliderSong.Value = 0;

                    return;
                }
               

            }
            
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            
            if (Songs.Count == 0)
            {
                Playerr.Source = null;
                timer.Stop();
                timer2.Stop();
                One.Content = "";

                tomer.Content = Playerr.Position.ToString(@"mm\:ss");

                SliderSong.Value = 0;


                return;
            }
            else
            {
                if (BoxTt.SelectedIndex - 1 < 0)
                {
                    //MessageBox.Show("prev");
                    Playerr.Source = new Uri(Songs[BoxTt.Items.Count - 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.Items.Count - 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                }
                else if (BoxTt.SelectedIndex - 1 >= 0)
                {
                    Playerr.Source = new Uri(Songs[BoxTt.SelectedIndex - 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex - 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = BoxTt.Items[BoxTt.SelectedIndex];
                }
            }

        }

        private void SliderVolume_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (Playerr.Volume < 0)
                {
                    Playerr.Volume = 0;
                    return;
                }
                else
                {
                    Playerr.Volume += 0.025;
                    SliderVolume.Value = Playerr.Volume * 100;
                }
            }
            else if (e.Delta < 0)
            {

                if (Playerr.Volume > 1)
                {
                    Playerr.Volume = 1;
                    return;
                }
                else
                {
                    Playerr.Volume -= 0.025;
                    SliderVolume.Value = Playerr.Volume * 100;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BoxTt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BoxTt.SelectedIndex != -1)
            {
                Playerr.Source = new Uri(Songs[BoxTt.SelectedIndex].ToString());
                Playerr.Play();
                timer.Start();
                timer2.Stop();
                timer2.Start();
                One.Content = BoxTt.Items[BoxTt.SelectedIndex];
            }
        }
    }
}
