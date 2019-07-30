using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Ball : RectangularShape
    {
        private int lives;
        private int xSpeed;
        private int ySpeed;
        public Ball(int width, int heigth, Point startPoint, SolidBrush brickColour, int amountOfLives, int xSpeed, int ySpeed) : base(width, heigth, startPoint, brickColour)
        {
            lives = amountOfLives;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
        }

        public void decreaseLives()
        {
            if (lives > 0)
            {
                lives -= 1;
            }
        }

        public int getLives()
        {
            return lives;
        }

        public int getxSpeed()
        {
            return xSpeed;
        }

        public void setxSpeed(int newXSpeed)
        {
            xSpeed = newXSpeed;
        }

        public int getySpeed()
        {
            return ySpeed;
        }

        public void setySpeed(int newYSpeed)
        {
            ySpeed = newYSpeed;
        }
    }
}
