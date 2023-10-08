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
using XOXClient.Communication;
using XOXClient.Communication.Packets;

namespace XOXClient.UCs
{
    public partial class UC_JoinGame : UserControl
    {
        PixelTextInputComponent textInput = new PixelTextInputComponent("textInput", new Point(7,31), Textures.Chars, 6, 1, true, 1, 1, Color.White, true, 2, 2);
        PixelButtonComponent buttonBack = new PixelButtonComponent("buttonBack", new Point(1, 1), Textures.Buttons.btn_Back, Textures.Buttons.btn_Back, Textures.Buttons.btn_Back__Hover);
        PixelButtonComponent buttonJoin = new PixelButtonComponent("buttonJoin", new Point(9, 47), Textures.Buttons.btn_Join, Textures.Buttons.btn_Join, Textures.Buttons.btn_Join__Hover);
        PixelImageComponent label = new PixelImageComponent("label", new Point(4, 13), Textures.Texts.EnterRoomCode);

        


        public UC_JoinGame(Size gameSize, int sizeMultiplier, Callback SETUC_Main)
        {
            InitializeComponent();

            this.SETUC_Main = SETUC_Main;

            pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
            pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
            pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;

            buttonBack.OnClick += ButtonBack_OnClick;

            buttonJoin.OnClick += ButtonJoin_OnClick;

            PixelComponent[] components = new PixelComponent[]
            {
                buttonBack,
                label,
                textInput,
                buttonJoin
            };

            PForm = new PixelForm(gameSize, sizeMultiplier, Textures.Colors.backgroundColor, components);

            buttonJoin.Visible = false;


            PForm.OnRedraw += PForm_OnRedraw;

            PForm_OnRedraw(null, null);
        }

        private async void ButtonJoin_OnClick(object sender, EventArgs e)
        {
            buttonJoin.Enabled = false;

            string roomCode = textInput.Text;

            var response = await Client.SendPacketAndWaitForResponse<Packet_JoinGameResponse>(new Packet_JoinGame() { RoomCode = roomCode}, 3);
            if(response == null)
            {
                MessageBox.Show("Timeout");
                buttonJoin.Enabled = true;
                return;
            }

            switch(response.roomState)
            {
                case Packet_JoinGameResponse.RoomState.RoomFull: MessageBox.Show("Room Full"); break;
                case Packet_JoinGameResponse.RoomState.IncorrectRoomCode: MessageBox.Show("Incorrect Room Code"); break;
            }

            buttonJoin.Enabled = true;
        }

        Callback SETUC_Main;

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


        public void OnKeyPress(char pressedChar)
        {
            textInput.triggerCharInput(pressedChar);

            if (textInput.Text.Length == 6) buttonJoin.Visible = true;
            else buttonJoin.Visible = false;
        }



        private void ButtonBack_OnClick(object sender, EventArgs e)
        {
            SETUC_Main.Invoke();
        }
    }
}
