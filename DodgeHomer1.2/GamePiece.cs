﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DodgeHomer1._2
{
    internal class GamePiece
    {
        private double _x;

        public double X
        {
            get { return _x; }
            set 
            {
                _x = value;
                Canvas.SetLeft(Shape, _x);
            }
        }
        private double _y;
        public double Y
        {
            get { return _y; }
            set 
            { _y = value;
                Canvas.SetTop(Shape, _y);
            }
        }


        public Image Shape { get; set; }

        public double Speed { get; set; }

        public Canvas cnvs { get; set; }

        public GamePiece()
        {
            Shape = new Image();
            Shape.Width = 50;
            Shape.Height = 50;
        }
    }
}
