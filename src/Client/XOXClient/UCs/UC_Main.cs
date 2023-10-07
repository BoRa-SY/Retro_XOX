using PixelBuilder;
using PixelBuilder.Abs;
using PixelBuilder.Components;
using PixelBuilder.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXClient.UCs
{
    public partial class UC_Main : UserControl
    {
        PixelButtonComponent buttonCreate = new PixelButtonComponent("buttonCreate", new Point(9, 24), Textures.Buttons.btn_Create, Textures.Buttons.btn_Create, Textures.Buttons.btn_Create__Hover);
        PixelButtonComponent buttonJoin = new PixelButtonComponent("buttonJoin", new Point(9, 41), Textures.Buttons.btn_Join, Textures.Buttons.btn_Join, Textures.Buttons.btn_Join__Hover);
        PixelImageComponent imageXOX = new PixelImageComponent("imageXOX", new Point(11, 5), Textures.XOXLogo);


        public UC_Main(Size gameSize, int sizeMultiplier, Callback SETUC_Create, Callback SETUC_Join)
        {

            InitializeComponent();

            this.SETUC_Create = SETUC_Create;
            this.SETUC_Join = SETUC_Join;

            pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
            pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
            pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;

            buttonCreate.OnClick += ButtonCreate_OnClick;
            buttonJoin.OnClick += ButtonJoin_OnClick;

            PixelComponent[] components = new PixelComponent[]
            {
                buttonCreate,
                buttonJoin,
                imageXOX
            };

            PForm = new PixelForm(gameSize, sizeMultiplier, Textures.Colors.backgroundColor, components);

            PForm.OnRedraw += PForm_OnRedraw;

            PForm_OnRedraw(null, null);

        }

        Callback SETUC_Create;
        Callback SETUC_Join;

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


        private void ButtonCreate_OnClick(object sender, EventArgs e)
        {
            SETUC_Create.Invoke();
        }

        private void ButtonJoin_OnClick(object sender, EventArgs e)
        {
            SETUC_Join.Invoke();
        }
    }

}
