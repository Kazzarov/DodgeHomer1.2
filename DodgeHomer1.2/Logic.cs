using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.ApplicationModel.Background;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace DodgeHomer1._2
{
    internal class Logic
    {
        public Board brd { get; set; }

        public DispatcherTimer timer { get; set; }
        bool isUp, isDown, isLeft, isRight;

        public Logic(Grid MasterGrid)
        {
            brd = new Board(MasterGrid);
            isUp=isDown=isLeft=isRight=false;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0,10);
            timer.Tick += Timer_Tick;
            timer.Start();
           
        }

        private void Timer_Tick(object sender, object e)
        {
            PlayerMove();
            EnemyMove();
            CheckCollision();
        }

        void PlayerMove()
        {
            if(isUp)
            {
                brd.Player.Y -= brd.Player.Speed;
            }
            if(isDown)
            {
                brd.Player.Y += brd.Player.Speed;
            }
            if (isLeft)
            {
                brd.Player.X -= brd.Player.Speed;
            }
            if (isRight)
            {
                brd.Player.X += brd.Player.Speed;
            }
            brd.PlayerGoOut();
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    isUp = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    isDown = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    isLeft = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    isRight = false;
                    break;

            }
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    isUp =true;
                    break;
                case Windows.System.VirtualKey.Down:
                    isDown =true;
                    break;
                case Windows.System.VirtualKey.Left:
                    isLeft =true;
                    break;
                case Windows.System.VirtualKey.Right:
                    isRight =true;
                    break;

            }
        }

        void EnemyMove()
        {
            foreach(Enemy enemy in brd.Enemies)
            {
                double goX = brd.Player.X - enemy.X;
                double goY = brd.Player.Y - enemy.Y;
                if(goX > 0 )
                {
                    enemy.X += enemy.Speed;
                }
                else
                {
                    enemy.X -= enemy.Speed;
                }
                if (goY > 0)
                {
                    enemy.Y += enemy.Speed;
                }
                else
                {
                    enemy.Y -= enemy.Speed;
                }
            }
        }

        private void Play(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(fileName));
            mediaPlayer.Play();
        }


        void CheckCollision()
        {
            if(brd.SameLocation(brd.Player))
            {
                Play("ms-appx:///Assets/Hmmdonuts.mp3");
                PrintMessage("Game Over", "You Lose");
                
            }
            else
            {
                for (int i = 0; i < brd.Enemies.Count;i++)
                {
                    if (brd.SameLocation(brd.Enemies[i]))
                    {
                        brd.cnvs.Children.Remove(brd.Enemies[i].Shape);
                        brd.Enemies.Remove(brd.Enemies[i]);
                        Play("ms-appx:///Assets/HomerDoe2.mp3");
                        brd.Enemies.ForEach(enemy => enemy.Speed += 0.3);
                        if(brd.Enemies.Count <= 1)
                        {
                            Play("ms-appx:///Assets/Wohoo.mp3");
                            PrintMessage("Game Over", "You Win");  
                        }
                    }
                }
            }
        }
        async void PrintMessage(string head, string text)
        {
            timer.Stop();
            MessageDialog message = new MessageDialog(text, head);
            await message.ShowAsync();
        }
        public async void SaveFile()
        {
            StorageFolder saveFolder = ApplicationData.Current.LocalFolder;
            StorageFile saveFile = await saveFolder.CreateFileAsync("SaveGame.txt", CreationCollisionOption.ReplaceExisting);

            foreach(Enemy enemy in brd.Enemies)
            {
                await FileIO.AppendTextAsync(saveFile, $"{enemy.X}|{enemy.Y}|{enemy.Speed}{Environment.NewLine}");
            }
            await FileIO.AppendTextAsync(saveFile, $"{brd.Player.X}|{brd.Player.Y}|{brd.Player.Speed}");
            PrintMessage("Game Saved", "Close to Continue");
        }

        public async void LoadGame()
        {
            StorageFolder saveFolder = ApplicationData.Current.LocalFolder;
            StorageFile saveFile = await saveFolder.GetFileAsync("SaveGame.txt");
            string allText = await FileIO.ReadTextAsync(saveFile);
            ReadFile(allText);
            PrintMessage("Game Loaded", "Close to Continue");
            timer.Start();
        }
        void ReadFile(string allText)
        {
            string[] lines = allText.Split("\n");
            string[] line;

            brd.cnvs.Children.Clear();
            brd.Enemies.Clear();
            for(int i = 0; i < lines.Length-1; i++)
            {
                line = lines[i].Split("|");
                Enemy enemy1 = new Enemy();
                RecreatePiece(line, enemy1);
                brd.Enemies.Add(enemy1);
            }
            
            line = lines[lines.Length-1].Split("|");
            RecreatePiece(line, brd.Player);

        }

        private void RecreatePiece(string[] line, GamePiece piece)
        {
            piece.X = double.Parse(line[0]);
            piece.Y = double.Parse(line[1]);
            piece.Speed = double.Parse(line[2]);

            brd.cnvs.Children.Add(piece.Shape);
        }
    }
}
