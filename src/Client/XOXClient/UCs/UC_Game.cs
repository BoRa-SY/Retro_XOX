using PixelBuilder.Abs;
using PixelBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixelBuilder.Components;
using XOXClient.Communication.Packets;
using XOXClient.Communication;

namespace XOXClient.UCs
{
    public partial class UC_Game : UserControl
    {
        PixelXOXGridComponent XOXGrid = new PixelXOXGridComponent("xoxgrid", new Point(11, 2), Textures.XO.GridX, Textures.XO.GridO, Color.White);
        PixelImageComponent labelNextPlayer = new PixelImageComponent("labelNextPlayer", new Point(8, 33), Textures.Texts.NextPlayer);

        PixelImageComponent nextPlayerX = new PixelImageComponent("nextPlayerX", new Point(20, 49), Textures.XO.IconX);
        PixelImageComponent nextPlayerO = new PixelImageComponent("nextPlayerO", new Point(20, 49), Textures.XO.IconO);

        public UC_Game(Size gameSize, int sizeMultiplier)
        {
            InitializeComponent();

            pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
            pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
            pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;

            XOXGrid.OnCellClicked += XOXGrid_OnCellClicked;

            PixelComponent[] components = new PixelComponent[]
            {
                XOXGrid,
                labelNextPlayer,
                nextPlayerX,
                nextPlayerO
            };

            PForm = new PixelForm(gameSize, sizeMultiplier, Textures.Colors.backgroundColor, components);
            nextPlayerO.Visible = false;

            PForm.OnRedraw += PForm_OnRedraw;

            PForm_OnRedraw(null, null);
        }

        PixelForm PForm;

        #region Essential Events

        private void PForm_OnRedraw(object sender, EventArgs e)
        {
            pictureBoxMain.Image = PForm.IMAGE;
        }

        private void PictureBoxMain_MouseUp(object sender, MouseEventArgs e)
        {
            PForm.TriggerMouseUp(e.Location);
        }

        private void PictureBoxMain_MouseDown(object sender, MouseEventArgs e)
        {
            PForm.TriggerMouseDown(e.Location);
        }

        private void PictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            PForm.TriggerMouseMove(e.Location);
        }
        #endregion

        public Player currentPlayer;

        Player nextPlayer = Player.X;

        public void NewMoveReceived(Packet_NewMoveBroadcast newMove)
        {
            XOXGrid.setCell(newMove.CellIndex, newMove.playerState == Packet_NewMoveBroadcast.PlayerState.X ? PixelXOXGridComponent.CellState.X : PixelXOXGridComponent.CellState.O);
            nextPlayer = newMove.playerState == Packet_NewMoveBroadcast.PlayerState.X ? Player.O : Player.X;

            if(nextPlayer == Player.X)
            {
                nextPlayerX.Visible = true;
                nextPlayerO.Visible = false;
            }
            else
            {
                nextPlayerX.Visible = false;
                nextPlayerO.Visible = true;

            }
        }

        private async void XOXGrid_OnCellClicked(object sender, int e)
        {
            if (nextPlayer != currentPlayer) return;

            await Client.SendPacket(new Packet_MakeMove() { CellIndex = e});
        }

        public enum Player
        {
            X = 0,
            O = 1
        }
    }
}
