using PixelBuilder.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.TextureUtils
{
    public class TextureFile
    {
        private Bitmap img;
        private int texturesPerRow;
        private Size textureSize;

        public TextureFile(Bitmap mainImage, int texturesPerRow, Size textureSize)
        {
            this.img = mainImage;
            this.texturesPerRow = texturesPerRow;

            this.textureSize = textureSize;
        }


        public Bitmap getTextureByIndex(int index)
        {
            int totalPixelOffset = index * textureSize.Width;

            int yindex = (totalPixelOffset / img.Width) * textureSize.Height;
            int xindex = totalPixelOffset - (yindex * texturesPerRow);

            return getTextureByLocation(new Point(xindex, yindex));
        }


        private Bitmap getTextureByLocation(Point location)
        {
            Bitmap texture = new Bitmap(textureSize.Width, textureSize.Height);

            using(Graphics g = Graphics.FromImage(texture))
            {
                g.DrawImage(img, new Rectangle(new Point(0, 0), textureSize), new Rectangle(location, textureSize), GraphicsUnit.Pixel);
            }

            return texture.Expand(10);
        }
    }
}
