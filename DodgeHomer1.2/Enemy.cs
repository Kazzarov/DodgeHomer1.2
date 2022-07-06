using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace DodgeHomer1._2
{
    internal class Enemy : GamePiece
    {
        public Enemy()
        {
            Shape.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/homerhead.png"));
            Speed = 1;
        }
    }
}
