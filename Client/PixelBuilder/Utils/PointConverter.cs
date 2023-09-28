using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Utils
{
    internal static class PointConverter
    {
        public static Point getCellPointFromPixelPoint(Point pixelPoint, int cellWidth, int cellHeight)
        {
            return new Point(pixelPoint.X / cellWidth, pixelPoint.Y / cellHeight);
        }
    }
}
