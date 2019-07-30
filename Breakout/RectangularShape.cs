using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class RectangularShape
    {
        int width;
        private int heigth;
        private Point startPoint;
        private SolidBrush brickColour;

        public RectangularShape(int width, int heigth, Point startPoint, SolidBrush brickColour)
        {
            this.width = width;
            this.heigth = heigth;
            this.startPoint = startPoint;
            this.brickColour = brickColour;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeigth()
        {
            return heigth;
        }

        public void setStartPoint(int xCoord, int yCoord)
        {
            startPoint.X = xCoord;
            startPoint.Y = yCoord;
        }

        public Point getStartPoint()
        {
            return startPoint;
        }

        public SolidBrush getBrickColour()
        {
            return brickColour;
        }
    }
}
