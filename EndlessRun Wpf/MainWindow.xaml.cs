using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EndlessRun_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;
        int force = 20;
        int speed = 5;

        Random rnd = new Random();
        bool gameOver;


        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        int score = 0;



        public MainWindow()
        {
            InitializeComponent();
            Myconvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bc3.gif"));
            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;
            StartGame();
            
        }

        private void GameEngine(object sender, EventArgs e)
        {
            if (score >= 5)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background) - 8);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 8);
            }
           else if(score >= 10)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background) - 15);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 15);
            }
            else
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
                Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);
            }
           

            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background.Width);
            }
            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }




            Canvas.SetTop(Player, Canvas.GetTop(Player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            scoreText.Content = "Score " + score;

            playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width - 15, Player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(Player, Canvas.GetTop(ground) - Player.Height);
                jumping = false;
                spriteIndex += .5;

                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }

                Runsprite(spriteIndex);

            }

            if (jumping == true)
            {
                speed = -9;
                force -= 1;

            }
            else
            {
                speed = 12;
            }
            if (force < 0)
            {
                jumping = false;
            }

            if (Canvas.GetLeft(obstacle) < -50)
            { if (score >= 5)
                {
                    Canvas.SetLeft(obstacle, 750);
                    Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);

                }
                else
                {
                    Canvas.SetLeft(obstacle, 950);
                    Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);
                }


                score += 1;
            }
        

            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();

            }
            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                Player.Stroke = Brushes.Red;
                Player.StrokeThickness = 1;

                scoreText.Content = "Score " + score + " please press enter to restart...";
                    }
            else
            {
                Player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;

            }
              
            
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Space && jumping ==false && Canvas.GetTop(Player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_02.gif"));
            }
        }
        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);

            Canvas.SetLeft(Player, 110);
            Canvas.SetTop(Player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            Runsprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));
            obstacle.Fill = obstacleSprite;


            jumping = false;
            gameOver = false;
            score = 0;
            scoreText.Content = "Score " + score;

            gameTimer.Start();

        }

        private void Runsprite(double i)
        {
            switch (i){
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_01.gif"));
                    break;

                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_02.gif"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_03.gif"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_04.gif"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_05.gif"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_06.gif"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_07.gif"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/newRunner_08.gif"));
                    break;
               
            }

            Player.Fill = playerSprite;
        }
    }
}
