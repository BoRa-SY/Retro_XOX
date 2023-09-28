using PixelBuilder.Abs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Components
{
    public class PixelButtonComponent : PixelComponent
    {
        public Bitmap texture { get; private set; }
        public Bitmap textureDown { get; private set; }
        public Bitmap textureHover { get; private set; }

        internal CurrentState buttonState { get; set; } = CurrentState.Up;


        public PixelButtonComponent(string name, Point location, Bitmap texture, Bitmap textureDown, Bitmap textureHover)
        {
            if (texture.Size != textureDown.Size || texture.Size != textureHover.Size) throw new Exception("Texture sizes must be the same");

            this.Name = name;

            this.Location = location;
            this.Size = texture.Size;

            this.texture = texture;
            this.textureDown = textureDown;
            this.textureHover = textureHover;
        }

        protected override void drawSelf(Graphics GRAPH)
        {
            switch(buttonState)
            {
                case CurrentState.Up:
                    GRAPH.DrawImage(texture, Location);
                    break;

                case CurrentState.Down:
                    GRAPH.DrawImage(textureDown, Location);
                    break;

                case CurrentState.Hover:
                    GRAPH.DrawImage(textureHover, Location);
                    break;
            }
        }



        public override bool onMouseDown()
        {
            if (buttonState == CurrentState.Down) return false;

            buttonState = CurrentState.Down;

            return true;
        }

        public override bool onMouseUp()
        {
            if (buttonState == CurrentState.Up) return false;

            buttonState = CurrentState.Up;

            return true;
        }

        public override bool onMouseEnter()
        {
            if (buttonState == CurrentState.Hover) return false;

            buttonState = CurrentState.Hover;

            return true;
        }

        public override bool onMouseLeave()
        {
            if (buttonState == CurrentState.Up) return false;

            buttonState = CurrentState.Up;

            return true;
        }



        internal enum CurrentState
        {
            Up,
            Down,
            Hover
        }
    }
}
