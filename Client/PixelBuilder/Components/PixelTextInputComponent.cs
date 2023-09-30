using PixelBuilder.Abs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelBuilder.Components
{
    public class PixelTextInputComponent : PixelComponent
    {
        public string TEXT = "";

        private Dictionary<char, Bitmap> charMap = new Dictionary<char, Bitmap>();


        public PixelTextInputComponent(Dictionary<char, Bitmap> charMap, int maxLength)
        {

        }

        protected override void drawSelf(Graphics GRAPH)
        {

        }
    }
}
