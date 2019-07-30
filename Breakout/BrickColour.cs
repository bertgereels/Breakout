using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class BrickColour
    {
        private SolidBrush greenColour;
        private SolidBrush yellowColour;
        private SolidBrush blueColour;
        private SolidBrush redColour;
        private SolidBrush purpleColour;
        private SolidBrush orangeColour;
        private SolidBrush pinkColour;
        private SolidBrush whiteColour;

        Random random = new Random();

        private List<SolidBrush> listOfColours = new List<SolidBrush>();
        public BrickColour()
        {
            greenColour = new SolidBrush(Color.FromArgb(191, 255, 0));
            listOfColours.Add(greenColour);
            yellowColour = new SolidBrush(Color.FromArgb(255, 255, 0));
            listOfColours.Add(yellowColour);
            blueColour = new SolidBrush(Color.FromArgb(0, 0, 255));
            listOfColours.Add(blueColour);
            redColour = new SolidBrush(Color.FromArgb(255, 0, 0));
            listOfColours.Add(redColour);
            purpleColour = new SolidBrush(Color.FromArgb(128, 0, 128));
            listOfColours.Add(purpleColour);
            orangeColour = new SolidBrush(Color.FromArgb(255, 140, 0));
            listOfColours.Add(orangeColour);
            pinkColour = new SolidBrush(Color.FromArgb(255, 0, 255));
            listOfColours.Add(pinkColour);
            whiteColour = new SolidBrush(Color.FromArgb(255, 255, 255));
            listOfColours.Add(whiteColour);
        }

        public SolidBrush getGreenColour()
        {
            return greenColour;
        }

        public SolidBrush getRandomColour()
        {
            return listOfColours[random.Next(listOfColours.Count)];
        }
    }
}
