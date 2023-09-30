using PixelBuilder.Abs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelBuilder.Components
{
    public class PixelTextInputComponent : PixelComponent
    {

        public string Text
        {
            get { return _text; }
            set
            {
                if (value.Length > textLength) return;
                for(int i = 0; i < value.Length; i++)
                {
                    if (!charMap.ContainsKey(value[i])) return;
                }
                _text = value;
                ParentForm.Redraw();
            }
        }
        private string _text = "";
        private int textLength;
        private int charPadding;
        private Size charSize;

        private Dictionary<char, Bitmap> charMap = new Dictionary<char, Bitmap>();

        private bool underline;
        private Size underlineSize;
        private int underlineOffset;
        private Color underlineColor;

        private SolidBrush underlineBrush;



        public PixelTextInputComponent(string name, Point location, Dictionary<char, Bitmap> charMap, int textLength, int charPadding, bool underline, int underlineOffset, int underlineThickness, Color underlineColor, string text = "")
        {
            Size baseSize = charMap.Count != 0 ? charMap.First().Value.Size : throw new ArgumentException("charMap length 0");
            
            foreach(var size in charMap.Values.Select(x => x.Size))
            {
                if (size != baseSize) throw new ArgumentException("All the character textures must have the same size");
            }


            this.Name = name;
            this.Location = location;


            this.charMap = charMap;
            this.textLength = textLength;
            this.charPadding = charPadding;
            this.charSize = charMap.First().Value.Size;
            this.underline = underline;
            this.underlineOffset = underlineOffset;
            this.underlineSize = new Size(charSize.Width, underlineThickness);
            this.underlineColor = underlineColor;

            this.underlineBrush = new SolidBrush(underlineColor);



            if(text.Length <= textLength)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (!charMap.ContainsKey(text[i])) return;
                }

                _text = text;
            }
        }

        protected override void drawSelf(Graphics GRAPH)
        {
            if(underline)
            {
                for(int i = 0; i < textLength; i++)
                {
                    drawUnderline(i);
                }
            }

            for(int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];
                if (!charMap.ContainsKey(c)) continue;
                drawChar(charMap[c], i);
            }



            void drawUnderline(int index)
            {
                int X = Location.X + (index * (charSize.Width + charPadding));
                int Y = Location.Y + charSize.Height + underlineOffset;

                GRAPH.FillRectangle(underlineBrush, new Rectangle(new Point(X, Y), underlineSize));
            }

            void drawChar(Bitmap bmp, int index)
            {
                int X = Location.X + (index * (charSize.Width + charPadding));

                GRAPH.DrawImage(bmp, new Rectangle(X, Location.Y, bmp.Size.Width, bmp.Size.Height), new Rectangle(new Point(0, 0), bmp.Size), GraphicsUnit.Pixel);
            }
        }

        public void triggerCharInput(char c)
        {
            if(c == '\b')
            {
                if (_text.Length == 0) return;

                _text = _text.Remove(_text.Length - 1, 1);
                ParentForm.Redraw();
                return;
            }

            if (_text.Length == textLength || !charMap.ContainsKey(c)) return;

            _text += c;

            ParentForm.Redraw();

            return;
        }
    }
}
