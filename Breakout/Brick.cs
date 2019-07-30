using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Brick : RectangularShape
    {
        int width;
        private int heigth;
        private Point startPoint;
        private SolidBrush brickColour;
        private bool hitByBall;

        public Brick(int width, int heigth, Point startPoint, SolidBrush brickColour) : base(width, heigth, startPoint, brickColour)
        {
            this.width = width;
            this.heigth = heigth;
            this.startPoint = startPoint;
            this.brickColour = brickColour;

            hitByBall = false;
        }

        public bool getHitByBallStatus()
        {
            return hitByBall;
        }

        public void changeHitByBallStatus()
        {
            hitByBall = true;
        }
    }
}
