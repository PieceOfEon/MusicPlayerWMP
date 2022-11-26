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

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string a;
        public MainWindow()
        {
            InitializeComponent();
            Playerr.Volume = 0.1;
            SliderVolume.Value = 10;
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "mp3 files (*.mp3)|*.mp3";
            openFileDialog1.ShowDialog();
            //a = openFileDialog1.SafeFileName+"\n";
            //MessageBox.Show(a);
            BoxTt.Items.Add(openFileDialog1.FileName);

        }

       
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if(BoxTt.SelectedIndex!=-1)
            {
                Playerr.Source = new Uri(BoxTt.SelectedItem.ToString());
            }
         
        }

        private void TexBoxPlaylist_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("q");
                Playerr.Source = new Uri(BoxTt.Items[0].ToString());
                BoxTt.SelectedIndex = 0;
            }
            if (BoxTt.SelectedIndex + 1< BoxTt.Items.Count)
            {
                    Playerr.Source = new Uri(BoxTt.Items[BoxTt.SelectedIndex + 1].ToString());
                    BoxTt.SelectedIndex = BoxTt.SelectedIndex + 1;
                
            }
                
        }

        private void ButtonDelSong_Click(object sender, RoutedEventArgs e)
        {
            BoxTt.Items.Remove(BoxTt.SelectedItem);
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (BoxTt.SelectedIndex - 1 >= 0)
            {
                Playerr.Source = new Uri(BoxTt.Items[BoxTt.SelectedIndex -1].ToString());
                BoxTt.SelectedIndex = BoxTt.SelectedIndex -1;
            }
        }
    }
}
