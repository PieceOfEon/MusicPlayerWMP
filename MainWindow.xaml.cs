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

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public List<string> path = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();
        int frame = 1;
        public string[] mas=new string[2];
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public MainWindow()
        {
            InitializeComponent();
            Playerr.Volume = 0.1;
            SliderVolume.Value = 10;
            timer.Tick += new EventHandler(Update);
            timer.Interval = TimeSpan.FromMilliseconds(200);
            

            Playerr.MediaEnded += ButtonNext_Click;

             
            
            

        }

        public void Update(object sender, EventArgs e)
        {
            if(Playerr.Position.TotalMilliseconds>100)
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

      
        private void SliderSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderSong.SelectionEnd = Convert.ToInt32(e.NewValue);
          
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderVolume.SelectionEnd = Convert.ToInt32(e.NewValue);
            Playerr.Volume = e.NewValue / 100;
            
            
        }

        private void ButtonAddSong_Click(object sender, RoutedEventArgs e)
        {
            
            openFileDialog1.Filter = "mp3 files (*.mp3)|*.mp3";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            
            MessageBox.Show(openFileDialog1.SafeFileName);
            if (openFileDialog1.FileName=="")
            {
                return;
            }
            else
            {
                //BoxTt.Items.Add(mas[1].);
                BoxTt.Items.Add(openFileDialog1.SafeFileName);
                //BoxTt.ItemsSource+ = openFileDialog1.FileName;
            }
        }

       
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if(BoxTt.SelectedIndex!=-1)
            {
                Playerr.Source = new Uri(openFileDialog1.FileName.ToString());
                timer.Start();
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
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (BoxTt.SelectedIndex + 1 == BoxTt.Items.Count)
            {
                MessageBox.Show("next");
                Playerr.Source = new Uri(BoxTt.Items[0].ToString());
                BoxTt.SelectedIndex = 0;
            }
            else if(BoxTt.SelectedIndex + 1< BoxTt.Items.Count)
            {
                    Playerr.Source = new Uri(openFileDialog1.FileName[i].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex + 1;
            }
                
        }

        private void ButtonDelSong_Click(object sender, RoutedEventArgs e)
        {
            BoxTt.Items.Remove(BoxTt.SelectedItem);
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {

            if (BoxTt.SelectedIndex - 1 < 0)
            {
                MessageBox.Show("prev");
                Playerr.Source = new Uri(BoxTt.Items[BoxTt.Items.Count - 1].ToString());
                BoxTt.SelectedIndex = BoxTt.Items.Count - 1;
            }
            else if (BoxTt.SelectedIndex - 1 >= 0)
            {
                Playerr.Source = new Uri(BoxTt.Items[BoxTt.SelectedIndex - 1].ToString());
                BoxTt.SelectedIndex = BoxTt.SelectedIndex - 1;
            }

        }
    }
}
