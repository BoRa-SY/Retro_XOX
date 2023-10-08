using PixelBuilder.Abs;
using PixelBuilder.Components;
using PixelBuilder.Utils;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointConverter = PixelBuilder.Utils.PointConverter;

namespace PixelBuilder
{
    public static class Methods
    {
        public static int CID(HttpListenerContext ctx)
        {
            return 0;
        }

        public static Dictionary<int, Dictionary<string, object>> ClientDB;
        public static Dictionary<int, Dictionary<string, object>> ClientInfoDB;
    }



    public class PixelForm
    {
        private PixelComponent[] Components;

        // Expanded Size
        public Bitmap IMAGE { get; private set; }

        // Actual Size
        public Size formSize { get; private set; }

        // Expanded Size / Actual Size
        private int sizeMultiplier;

        public Color BGColor { get; private set; }

        public event EventHandler OnRedraw;

        private SolidBrush brush_BG;



        public PixelForm(Size formSize, int sizeMultiplier, Color backgroundColor, params PixelComponent[] components)
        {
            this.formSize = formSize;
            this.BGColor = backgroundColor;
            this.sizeMultiplier = sizeMultiplier;

            IMAGE = new Bitmap(formSize.Width * sizeMultiplier, formSize.Height * sizeMultiplier);

            brush_BG = new SolidBrush(this.BGColor);

            Components = components;
            Components.ToList().ForEach(x => x.SetParent(this));

            Redraw();

        }



        internal void Redraw()
        {

            Bitmap newBitmap = new Bitmap(formSize.Width, formSize.Height);

            using (Graphics GRAPH = Graphics.FromImage(newBitmap))
            {
                GRAPH.FillRectangle(brush_BG, 0, 0, formSize.Width, formSize.Height);


                foreach (PixelComponent component in Components)
                {
                    component.DrawSelf(GRAPH);
                }

            }

            IMAGE.Dispose();
            IMAGE = newBitmap.Expand(sizeMultiplier);


            if (OnRedraw != null) OnRedraw.Invoke(this, null);

        }


        public PixelComponent getComponentByCellPoint(Point cellP)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                PixelComponent comp = Components[i];
                if (comp.isInBounds(cellP) && (comp.Visible)) return comp;
            }

            return null;
        }





        PixelComponent currentHoveringComponent = null;

        public void TriggerMouseMove(Point location)
        {
            Point cellPoint = PointConverter.getCellPointFromPixelPoint(location, sizeMultiplier, sizeMultiplier);

            PixelComponent component = getComponentByCellPoint(cellPoint);

            if (component == currentHoveringComponent)
            {
                return;
            }
            else if (component == null)
            {
                bool redrawRequired = currentHoveringComponent.onMouseLeave(cellPoint);
                if (redrawRequired) Redraw();

                currentHoveringComponent = null;
            }
            else
            {
                bool redrawRequired = false;
                if (currentHoveringComponent != null)
                    redrawRequired = currentHoveringComponent.onMouseLeave(location.Subtract(component.Location)) ? true : redrawRequired;

                currentHoveringComponent = component;
                redrawRequired = component.onMouseEnter(location.Subtract(component.Location)) ? true : redrawRequired;

                if (redrawRequired) Redraw();
            }
        }

        public void TriggerMouseDown(Point location)
        {
            Point cellPoint = PointConverter.getCellPointFromPixelPoint(location, sizeMultiplier, sizeMultiplier);

            PixelComponent component = getComponentByCellPoint(cellPoint);

            if (component == null) return;

            bool redrawRequired = component.onMouseDown(cellPoint);

            if (redrawRequired) Redraw();
        }

        public void TriggerMouseUp(Point location)
        {
            Point cellPoint = PointConverter.getCellPointFromPixelPoint(location, sizeMultiplier, sizeMultiplier);

            PixelComponent component = getComponentByCellPoint(cellPoint);

            if (component == null) return;

            bool redrawRequired = component.onMouseUp(cellPoint);

            if (redrawRequired) Redraw();

        }



        #region Indexers
        public PixelComponent this[string name]
        {
            get { return Components.Where(x => x.Name == name).FirstOrDefault(); }
        }
        #endregion
    }
}
