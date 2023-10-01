using PixelBuilder.Abs;
using PixelBuilder.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelBuilder.Components
{
    public class PixelXOXGridComponent : PixelComponent
    {
        private Bitmap Ximg;
        private Bitmap Oimg;
        int cellSideLength;

        private CellState[] cells = new CellState[9];
        private Point[] cellLocations = new Point[9];

        private Pen gridPen;

        public PixelXOXGridComponent(string name, Point location, Bitmap Ximg, Bitmap Oimg, Color gridColor)
        {
            if (Ximg.Size != Oimg.Size) throw new ArgumentException("X and O images must have the same size");
            if (Ximg.Size.Width != Ximg.Size.Height) throw new ArgumentException("X and O images must be squares");
            
            this.Name = name;
            this.Location = location;

            this.Ximg = Ximg;
            this.Oimg = Oimg;
            this.cellSideLength = Ximg.Width;

            this.gridPen = new Pen(gridColor, 1);

            calculateCellLocations();

            setAllCells(CellState.X);
        }

        private void calculateCellLocations()
        {
            // Was lazy to create a for loop with the relations

            cellLocations[0] = Location.Add(1 + (cellSideLength + 1) * 0, 1 + (cellSideLength + 1) * 0);
            cellLocations[1] = Location.Add(1 + (cellSideLength + 1) * 1, 1 + (cellSideLength + 1) * 0);
            cellLocations[2] = Location.Add(1 + (cellSideLength + 1) * 2, 1 + (cellSideLength + 1) * 0);

            cellLocations[3] = Location.Add(1 + (cellSideLength + 1) * 0, 1 + (cellSideLength + 1) * 1);
            cellLocations[4] = Location.Add(1 + (cellSideLength + 1) * 1, 1 + (cellSideLength + 1) * 1);
            cellLocations[5] = Location.Add(1 + (cellSideLength + 1) * 2, 1 + (cellSideLength + 1) * 1);

            cellLocations[6] = Location.Add(1 + (cellSideLength + 1) * 0, 1 + (cellSideLength + 1) * 2);
            cellLocations[7] = Location.Add(1 + (cellSideLength + 1) * 1, 1 + (cellSideLength + 1) * 2);
            cellLocations[8] = Location.Add(1 + (cellSideLength + 1) * 2, 1 + (cellSideLength + 1) * 2);

        }

        protected override void drawSelf(Graphics GRAPH)
        {
            drawGrid();
            drawCells();

            void drawGrid()
            {

                #region Outer Square
                int outerSquareSideLength = (cellSideLength * 3) + 3;
                Rectangle outerSquare = new Rectangle(Location, new Size(outerSquareSideLength, outerSquareSideLength));

                GRAPH.DrawRectangle(gridPen, outerSquare);
                #endregion

                #region Vertical Lines

                for(int c = 1; c <= 2; c++)
                {
                    int offset = (c * (cellSideLength + 1));

                    Point startLoc = new Point(Location.X + offset, Location.Y);

                    Point endLoc = new Point(startLoc.X, startLoc.Y + (cellSideLength * 3) + 3);

                    GRAPH.DrawLine(gridPen, startLoc, endLoc);
                }

                #endregion

                #region Horizontal Lines

                for (int c = 1; c <= 2; c++)
                {
                    int offset = (c * (cellSideLength + 1));

                    Point startLoc = new Point(Location.X, Location.Y + offset);

                    Point endLoc = new Point(startLoc.X + (cellSideLength * 3) + 3, startLoc.Y);

                    GRAPH.DrawLine(gridPen, startLoc, endLoc);
                }

                #endregion
            }

            void drawCells()
            {
                for(int i = 0; i < cells.Length; i++)
                {
                    drawCellByIndex(i);
                }


                void drawCellByIndex(int index)
                {
                    Point cellLoc = cellLocations[index];
                    CellState state = cells[index];

                    switch(state)
                    {
                        case CellState.Empty: break;
                        case CellState.X: GRAPH.DrawImage(Ximg, new Rectangle(cellLoc, Ximg.Size), new Rectangle(Point.Empty, Ximg.Size), GraphicsUnit.Pixel); break;
                        case CellState.O: GRAPH.DrawImage(Oimg, new Rectangle(cellLoc, Oimg.Size), new Rectangle(Point.Empty, Oimg.Size), GraphicsUnit.Pixel); break;
                    }
                }

            }

        }


        private void setAllCells(CellState state)
        {
            for(int i = 0; i < cells.Length; i++)
            {
                cells[i] = state;
            }
        }

        public void setCell(int index, CellState state)
        {
            if (index < 0 || index >= cells.Length) throw new ArgumentOutOfRangeException("incorrect cell index");

            cells[index] = state;

            ParentForm.Redraw();
        }

        public CellState getCell(int index)
        {
            if(index < 0 || index >= cells.Length) throw new ArgumentOutOfRangeException("incorrect cell index");

            return cells[index];
        }



        public enum CellState
        {
            Empty = 0,
            X,
            O
        }
    }
}
