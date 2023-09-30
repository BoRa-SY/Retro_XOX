using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Abs
{
    public abstract class PixelComponent
    {
        public PixelForm ParentForm { get; private set; }
        public Point Location { get; protected set; }
        public Size Size { get; protected set; }
        public string Name { get; protected set; }

        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                ParentForm.Redraw();
            }
        }


        public void DrawSelf(Graphics GRAPH)
        {
            if (!Visible) return;

            drawSelf(GRAPH);
        }

        public void SetParent(PixelForm parent) => ParentForm = parent;


        protected abstract void drawSelf(Graphics GRAPH);

        public virtual bool onMouseDown()
        {
            return false;
        }

        public virtual bool onMouseUp()
        {
            return false;
        }

        public virtual bool onMouseEnter()
        {
            return false;
        }

        public virtual bool onMouseLeave()
        {
            return false;
        }

        public bool isInBounds(Point cellPoint)
        {
            return (cellPoint.X <= Location.X + Size.Width - 1 && cellPoint.X >= Location.X) && (cellPoint.Y <= Location.Y + Size.Height - 1 && cellPoint.Y >= Location.Y);
        }

    }
}
