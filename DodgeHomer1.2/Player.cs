using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeHomer1._2
{
    internal class Player : GamePiece
    {
        public Player()
        {
            Shape.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/donut.png"));
            Speed = 5;
            Lives = 3;
        }
        public int Lives { get; set ; }
    }
}
