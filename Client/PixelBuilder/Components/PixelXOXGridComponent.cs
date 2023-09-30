using PixelBuilder.Abs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Components
{
    public class PixelXOXGridComponent : PixelComponent
    {
        public PixelXOXGridComponent(string name, Point location)
        {
            this.Name = name;
            this.Location = location;
        }

        protected override void drawSelf(Graphics GRAPH)
        {

        }



        [Flags]
        public enum CellState
        {
            Empty = 0b00,
            X     = 0b01,
            O     = 0b10
        }
    }
}
