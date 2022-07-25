using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Background;
using Windows.Media.Playback;
using Windows.Media.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DodgeHomer1._2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Logic lgc { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            startGrd.Visibility = Visibility.Collapsed;
            cnvs.Children.Clear();
            lgc = new Logic(MasterGrid ,livesTbl);
        }

        private void Pause_Tapped(object sender, TappedRoutedEventArgs e)
        {
            pauseBtn.Content = ((string)pauseBtn.Content == "Pause") ? "Resume" : "Pause";
            if((string)pauseBtn.Content == "Pause")
            {
                lgc.timer.Start();
            }
            else if((string)pauseBtn.Content == "Resume")
            {
                lgc.timer.Stop();
            }
        }

        private void Save_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lgc.SaveFile();
        }

        private void loadBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            lgc.LoadGame();
        }

        private void restartBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            cnvs.Children.Clear();
            lgc = new Logic(MasterGrid, livesTbl);
        }

        private void gohomeBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            startGrd.Visibility = Visibility.Visible;
        }

    }
}
