using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Paddle : RectangularShape
    {
        private int movingSpeed;
        public Paddle(int width, int heigth, Point startPoint, SolidBrush brickColour, int movingSpeed) : base(width, heigth, startPoint, brickColour)
        {
            this.movingSpeed = movingSpeed;
        }

        public int getMovingSpeed()
        {
            return movingSpeed;
        }

    }
}
