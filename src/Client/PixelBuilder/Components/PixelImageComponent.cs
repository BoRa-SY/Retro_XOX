using PixelBuilder.Abs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Components
{
    public class PixelImageComponent : PixelComponent
    {
        private Image img;

        public PixelImageComponent(string name, Point location, Image image)
        {
            this.Name = name;
            this.Location = location;
            this.img = image;
        }

        protected override void drawSelf(Graphics GRAPH)
        {
            GRAPH.DrawImage(img, new Rectangle(Location, img.Size), new Rectangle(new Point(0, 0), img.Size), GraphicsUnit.Pixel);
        }
    }
}
