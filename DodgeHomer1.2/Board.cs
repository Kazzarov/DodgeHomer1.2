using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;

namespace DodgeHomer1._2
{
    internal class Board
    {
        public Player Player { get; set; }
        public Grid MasterGrid { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Canvas cnvs { get; set; }
        int numberOfEnemies = 10;

        public Board(Grid MasterGrid)
        {
            this.MasterGrid = MasterGrid;
            cnvs = (Canvas)MasterGrid.FindName("cnvs");
            Enemies = new List<Enemy>();
            for(int i = 0; i < numberOfEnemies; i++)
            {
                Enemy enemy1 = new Enemy();
                CreatePiece(enemy1);
                Enemies.Add(enemy1);
            }

            Player = new Player();
            CreatePiece(Player);
        }

        void CreatePiece(GamePiece piece)
        {
            SetLoc(piece);
            if (SameLocation(piece))
            {
                SetLoc(piece);
            }
            else
            {
                cnvs.Children.Add(piece.Shape);
            }
        }

        //Takes a game piece and put it on the Canvas
        private void SetLoc(GamePiece piece)
        {
            Random rnd = new Random();

            piece.X = rnd.Next((int)(cnvs.ActualWidth - piece.Shape.Width));
            piece.Y = rnd.Next((int)(cnvs.ActualHeight - piece.Shape.Height));
        }

        public bool SameLocation(GamePiece piece)
        {
            foreach(Enemy enemy in Enemies)
            {
                if(piece == enemy)
                {
                    continue;
                }
                if(Math.Abs(enemy.X - piece.X) < piece.Shape.Width *0.65 &&
                    Math.Abs(enemy.Y - piece.Y) < piece.Shape.Height * 0.65)
                {
                    return true;
                }
            }
            return false;
        }
        public void PlayerGoOut()
        {
            if(Player.X < 0)
            {
                Player.X = 0;
            }
            if (Player.X > cnvs.ActualWidth - Player.Shape.Width)
            {
                Player.X = cnvs.ActualWidth - Player.Shape.Width;
            }

            if (Player.Y < 0)
            {
                Player.Y = 0;
            }
            if (Player.Y > cnvs.ActualHeight - Player.Shape.Height)
            {
                Player.Y = cnvs.ActualHeight - Player.Shape.Height;
            }
        }
    }
}
