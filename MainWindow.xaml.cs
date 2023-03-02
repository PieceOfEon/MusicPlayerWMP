using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Threading;
using System.Reflection;
using FilePath = System.IO.Path;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Emit;

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
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
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
            
            if (Playerr.Position.TotalMilliseconds>100)
            {
                try
                {
                    SliderSong.Maximum = Playerr.NaturalDuration.TimeSpan.TotalSeconds;
                    
                    tomer.Content=Playerr.Position.ToString(@"mm\:ss");

                }
                catch (Exception q) { MessageBox.Show(q.Message); }
            }
            SliderSong.Value = Playerr.Position.TotalSeconds;
            Img.X= 120+frame*90;
            Img.Y = 200;
            if (frame < 7)
            {
                frame++;
            }
            else
            {
                frame = 1;
            }
            if(Playerr.Source==null)
            {
                timer.Stop();
            }
        }

        public void RunningName(object sender, EventArgs e)
        {
            if (One.Content.ToString().Count()>0)
            {
                One.Content = One.Content.ToString() + One.Content.ToString()[0];
                One.Content= One.Content.ToString().Remove(0, 1);
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

        private void ButtonAddSong_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            
            openFileDialog2.Filter = "mp3 files (*.mp3)|*.mp3";
            openFileDialog2.Multiselect = true;
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileNames.Length>0)
            {
                //openFileDialog1 = openFileDialog2;
                MessageBox.Show("Uspeh");

                openFileDialog1 = openFileDialog2;
                BoxTt.Items.Clear();
                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    BoxTt.Items.Add(openFileDialog1.SafeFileNames[i]);
                }
            }
            else
            {
                return;
            }
           
            MessageBox.Show(openFileDialog1.SafeFileName);
            
           
        }

       
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            
            if (BoxTt.SelectedIndex!=-1)
            {
                Playerr.Source = new Uri(openFileDialog1.FileNames[BoxTt.SelectedIndex].ToString());
                timer.Start();
                timer2.Stop();
                timer2.Start();
                One.Content = openFileDialog1.SafeFileNames[BoxTt.SelectedIndex];
            }
            
        }

        private void TexBoxPlaylist_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SliderSong.Maximum = Playerr.NaturalDuration.TimeSpan.TotalSeconds;
                MessageBox.Show(Playerr.NaturalDuration.TimeSpan.TotalSeconds.ToString()); 
                
            }
            catch (Exception q ) { MessageBox.Show(q.Message); }
            
            //if (Playerr.CanPause)
            //    Playerr.Pause();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Playerr.Source = null;
            timer2.Stop();

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            if (openFileDialog1.FileName == "")
            {
                return;
            }
            else
            {
                if (BoxTt.SelectedIndex + 1 == BoxTt.Items.Count)
                {
                    MessageBox.Show("next");
                    Playerr.Source = new Uri(openFileDialog1.FileNames[0].ToString());
                    BoxTt.SelectedIndex = 0;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = openFileDialog1.SafeFileNames[BoxTt.SelectedIndex];
                }
                else if (BoxTt.SelectedIndex + 1 < BoxTt.Items.Count)
                {
                    Playerr.Source = new Uri(openFileDialog1.FileNames[BoxTt.SelectedIndex + 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex + 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = openFileDialog1.SafeFileNames[BoxTt.SelectedIndex];
                }
            }
            
                
        }

        private void ButtonDelSong_Click(object sender, RoutedEventArgs e)
        {

            BoxTt.Items.Remove(openFileDialog1.FileName[1]);
        }
        
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            if (openFileDialog1.FileName == "")
            {
                return;
            }
            else
            {
                if (BoxTt.SelectedIndex - 1 < 0)
                {
                    MessageBox.Show("prev");
                    Playerr.Source = new Uri(openFileDialog1.FileNames[BoxTt.Items.Count - 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.Items.Count - 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = openFileDialog1.SafeFileNames[BoxTt.SelectedIndex];
                }
                else if (BoxTt.SelectedIndex - 1 >= 0)
                {
                    Playerr.Source = new Uri(openFileDialog1.FileNames[BoxTt.SelectedIndex - 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex - 1;
                    timer2.Stop();
                    timer2.Start();
                    One.Content = openFileDialog1.SafeFileNames[BoxTt.SelectedIndex];
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
                    SliderVolume.Value = Playerr.Volume*100;
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
                    SliderVolume.Value = Playerr.Volume*100;
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
    }
}
