using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Utils
{
    internal static class PointExtensions
    {
        public static Point Add(this Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point Add(this Point p1, int X, int Y)
        {
            return new Point(p1.X + X, p1.Y + Y);
        }
    }
}
