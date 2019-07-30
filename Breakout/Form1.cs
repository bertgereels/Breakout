using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    enum Movements {Left, Right, Stop};
    public partial class Form1 : Form
    {
        private BrickColour brickColours;
        private Paddle paddle;
        private Movements paddleMovement;
        private List<Brick> bricksToDestroy = new List<Brick>();
        private Ball ball;
        public Form1()
        {
            InitializeComponent();

            gameOverLabel.Visible = false;

            brickColours = new BrickColour();
            paddle = new Paddle(120, 20, new Point(0,0), brickColours.getGreenColour(), 20);

            AddBricksToBrickList();
            CalculateBrickStartingPoints();

            ball = new Ball(20, 20, new Point(0, 0), brickColours.getGreenColour(), 3, 0, 0);

            livesLabel.Text = ball.getLives().ToString();

            gameTimer.Interval = 1000 / 20;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();
        }

        private void StartGame()
        {
            Point paddleStartingPoint = CalculatePaddleStartPosition();
            paddle.setStartPoint(paddleStartingPoint.X, paddleStartingPoint.Y);

            Point ballStartingPosition = CalculateBallStartPosition();
            ball.setStartPoint(ballStartingPosition.X, ballStartingPosition.Y);

            ball.setxSpeed(5);
            ball.setySpeed(5);
        }

        private void UpdateScreen(Object sender, EventArgs e)
        {
            if (Input.KeyPress(Keys.Right))
            {
                paddleMovement = Movements.Right;
            }else if (Input.KeyPress(Keys.Left))
            {
                paddleMovement = Movements.Left;
            }
            else
            {
                paddleMovement = Movements.Stop;
            }

            MoveBall();
            MovePaddle();

            pictureBox1.Invalidate();
        }

        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            DrawPaddle(canvas);
            DrawBricks(canvas);
            DrawBall(canvas);
        }

        private void DrawPaddle(Graphics g)
        {
            g.FillRectangle(paddle.getBrickColour(), new Rectangle(
                                                        paddle.getStartPoint().X,
                                                        paddle.getStartPoint().Y,
                                                        paddle.getWidth(),
                                                        paddle.getHeigth()));
        }

        private void MovePaddle()
        {
            if (paddleMovement == Movements.Right && (paddle.getStartPoint().X + paddle.getWidth() + 10) < pictureBox1.Width)
            {
                paddle.setStartPoint(
                    paddle.getStartPoint().X + paddle.getMovingSpeed(),
                    paddle.getStartPoint().Y);
            }
            else if (paddleMovement == Movements.Left && paddle.getStartPoint().X > 0)
            {
                paddle.setStartPoint(
                    paddle.getStartPoint().X - paddle.getMovingSpeed(),
                    paddle.getStartPoint().Y);
            }
            else if (paddleMovement == Movements.Stop)
            {
                paddle.setStartPoint(
                    paddle.getStartPoint().X,
                    paddle.getStartPoint().Y);
            }
        }

        private Point CalculatePaddleStartPosition()
        {
            Point newPoint = new Point(0,0);

            int canvasMaxWidth = pictureBox1.Size.Width;
            int canvasMaxHeigth = pictureBox1.Size.Height;

            int widthMiddle = canvasMaxWidth / 2;
            newPoint.X = widthMiddle - paddle.getWidth() / 2;
            newPoint.Y = canvasMaxHeigth - 30;

            return newPoint;
        }

        private void AddBricksToBrickList()
        {
            for (int i = 0; i <= 43; i++)
            {
                bricksToDestroy.Add(new Brick(60, 20, new Point(0, 0), brickColours.getRandomColour()));
            }
        }

        private void CalculateBrickStartingPoints()
        {
            int brickCounter = 0;
            int startX = 10;
            int startY = 10;
            int verticalSpacingBetweenBricks = 10;
            int horizontalSpacingBetweenBricks = 20;

            foreach (Brick brick in bricksToDestroy)
            {
                if (brickCounter == 11)
                {
                    brickCounter = 0;

                    startX = 10;
                    startY = startY + horizontalSpacingBetweenBricks + brick.getHeigth();
                }

                brick.setStartPoint(startX, startY);
                startX += verticalSpacingBetweenBricks + brick.getWidth();

                brickCounter++;
            }
        }

        private void DrawBricks(Graphics g)
        {
            foreach (Brick brick in bricksToDestroy)
            {
                if (!brick.getHitByBallStatus())
                {
                    g.FillRectangle(brick.getBrickColour(), new Rectangle(
                                                brick.getStartPoint().X,
                                                brick.getStartPoint().Y,
                                                brick.getWidth(),
                                                brick.getHeigth()));
                }
            }
        }

        private void DrawBall(Graphics g)
        {
            DetectWallBallCollision();
            DetectBrickBallCollision();
            DetectPaddleBallCollision();

            g.FillRectangle(ball.getBrickColour(), new Rectangle(
                                                                ball.getStartPoint().X,
                                                                ball.getStartPoint().Y,
                                                                ball.getWidth(),
                                                                ball.getHeigth()));

        }

        private Point CalculateBallStartPosition()
        {
            Point newPoint = new Point(0, 0);

            int canvasMaxWidth = pictureBox1.Size.Width;
            int canvasMaxHeigth = pictureBox1.Size.Height;

            int widthMiddle = canvasMaxWidth / 2;
            newPoint.X = widthMiddle - ball.getWidth() / 2;
            newPoint.Y = canvasMaxHeigth - 60;

            return newPoint;
        }

        private void MoveBall()
        {
            ball.setStartPoint(ball.getStartPoint().X + ball.getxSpeed(), ball.getStartPoint().Y + ball.getySpeed());
        }

        private void DetectPaddleBallCollision()
        {
            if (new Rectangle(ball.getStartPoint().X, ball.getStartPoint().Y, ball.getWidth(), ball.getHeigth()).IntersectsWith(new Rectangle(paddle.getStartPoint().X, paddle.getStartPoint().Y, paddle.getWidth(), paddle.getHeigth())))
            {
                ball.setySpeed(-ball.getySpeed());
            }
        }

        private void DetectWallBallCollision()
        {
            int canvasLeftBorder = 2;
            int canvasTopBorder = 2;
            int canvasRightBorder = pictureBox1.Size.Width;
            int canvasBottomBorder = pictureBox1.Size.Height;

            Console.WriteLine("Ball X and Y: " + ball.getStartPoint().X + " " + ball.getStartPoint().Y);

            if (ball.getStartPoint().Y <= canvasTopBorder)
            {
                ball.setySpeed(-ball.getySpeed());
            }

            if (ball.getStartPoint().X <= canvasLeftBorder)
            {
                ball.setxSpeed(-ball.getxSpeed());
            }

            if (ball.getStartPoint().X + ball.getWidth() >= canvasRightBorder)
            {
                ball.setxSpeed(-ball.getxSpeed());
            }

            if (ball.getStartPoint().Y + ball.getHeigth() >= canvasBottomBorder)
            {
                Die();
            }
        }

        private void DetectBrickBallCollision()
        {
            List<Brick> copyOfbricksToDestroy = bricksToDestroy.ToList();

            foreach (Brick brick in copyOfbricksToDestroy)
            {
                if (new Rectangle(ball.getStartPoint().X, ball.getStartPoint().Y, ball.getWidth(), ball.getHeigth()).IntersectsWith(new Rectangle(brick.getStartPoint().X, brick.getStartPoint().Y, brick.getWidth(), brick.getHeigth())))
                {
                    brick.changeHitByBallStatus();
                    bricksToDestroy.Remove(brick);
                    int oldScore = Int32.Parse(scoreLabel.Text);
                    scoreLabel.Text = (oldScore + 100).ToString();
                }
                
                if (bricksToDestroy.Count == 0)
                {
                    EndOfGame();
                }
            }
        }

        private void Die()
        {
            if (ball.getLives() > 0)
            {
                Console.WriteLine("Meer dan 0 levens!");
                ball.decreaseLives();

                StartGame();
            }
            else
            {
                GameOver();
            }

            livesLabel.Text = ball.getLives().ToString();
        }

        private void GameOver()
        {
            gameTimer.Stop();

            paddleMovement = Movements.Stop;
            gameOverLabel.Visible = true;
        }

        private void EndOfGame()
        {
            ball.setxSpeed(0);
            ball.setySpeed(0);

            gameOverLabel.Text = "YOU WIN!";
            gameOverLabel.Visible = true;

            paddleMovement = Movements.Stop;
        }

        private void HandleInput(object sender, KeyEventArgs e)
        {
            Console.WriteLine("User pressed: " + e.KeyCode);

            Input.changeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

    }
}
