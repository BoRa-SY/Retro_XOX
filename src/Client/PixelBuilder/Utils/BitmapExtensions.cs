using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Utils
{
    internal static class BitmapExtensions
    {
        public static unsafe Bitmap Expand(this Bitmap bmp, int multiplier)
        {
            if (multiplier == 0) return bmp;

            int newWidth = bmp.Width * multiplier;
            int newHeight = bmp.Height * multiplier;
            Bitmap resizedBitmap = new Bitmap(newWidth, newHeight);

            Rectangle srcRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            Rectangle destRect = new Rectangle(0, 0, newWidth, newHeight);

            BitmapData srcData = bmp.LockBits(srcRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = resizedBitmap.LockBits(destRect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            for (int y_index = 0; y_index < bmp.Height; y_index++)
            {
                byte* srcRow = (byte*)srcData.Scan0 + y_index * srcData.Stride;
                byte* destRow = (byte*)destData.Scan0 + y_index * multiplier * destData.Stride;

                for (int x_index = 0; x_index < bmp.Width; x_index++)
                {
                    byte* srcPixel = srcRow + x_index * 4; // Assuming 32bpp format (4 bytes per pixel)

                    for (int y = 0; y < multiplier; y++)
                    {
                        byte* destPixelRow = destRow + y * destData.Stride;

                        for (int x = 0; x < multiplier; x++)
                        {
                            byte* destPixel = destPixelRow + (x_index * multiplier + x) * 4;
                            *(int*)destPixel = *(int*)srcPixel;
                        }
                    }
                }
            }

            bmp.UnlockBits(srcData);
            resizedBitmap.UnlockBits(destData);

            return resizedBitmap;
        }
    }
}
