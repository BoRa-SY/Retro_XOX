using PixelBuilder;
using PixelBuilder.Abs;
using PixelBuilder.Components;
using PixelBuilder.TextureUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOXClient.Properties;

namespace XOXClient
{
    public partial class FormMain : Form
    {
        PixelForm form;

        public FormMain()
        {
            InitializeComponent();

            var btn = new PixelButtonComponent("btn1", new Point(1, 1), Resources.button, Resources.buttonDown, Resources.buttonHover);
            btn.OnClick += Btn_OnClick;
            PixelComponent[] comps = new PixelComponent[]
            {
                btn,
                new PixelButtonComponent("btn2", new Point(1,12), Resources.button, Resources.buttonDown, Resources.buttonHover)
            };



            form = new PixelForm(new Size(50,60), 10, Color.Red, comps);
            form.OnRedraw += Form_OnRedraw;

            Form_OnRedraw(null, null);
        }

        private void Btn_OnClick(object sender, EventArgs e)
        {

        }

        private void Form_OnRedraw(object sender, EventArgs e)
        {
            pictureBox1.Image = form.IMAGE;
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            form.TriggerMouseDown(e.Location);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            form.TriggerMouseUp(e.Location);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            form.TriggerMouseMove(e.Location);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = form["btn1"];
            a.Visible = !a.Visible;
        }
    }
}
