using PixelBuilder;
using PixelBuilder.Abs;
using PixelBuilder.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOXClient.Properties;

namespace XOXClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<PixelComponent> components = new List<PixelComponent>()
            {
                new PixelButtonComponent("btn1", new Point(1,1), Resources.button, Resources.buttonDown, Resources.buttonHover),
                new PixelButtonComponent("btn2", new Point(1,15), Resources.button, Resources.buttonDown, Resources.buttonHover),
            };

            (components[0] as PixelButtonComponent).OnClick += btn_OnClick;

            form = new PixelForm(new Size(50, 60), 10, Color.FromArgb(0,188,212), components.ToArray());

            form.OnRedraw += Form_OnRedraw;

            pictureBox1.Image = form.IMAGE;




            form.TriggerMouseMove(new Point(10,10));
            form.TriggerMouseMove(new Point(150,150));
        }

        private void btn_OnClick(object sender, EventArgs e)
        {
            MessageBox.Show(this,"AD");
        }

        private void Form_OnRedraw(object sender, EventArgs e)
        {
            pictureBox1.Image = form.IMAGE;
        }

        PixelForm form;


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            form.TriggerMouseDown(e.Location);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            form.TriggerMouseMove(e.Location);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            form.TriggerMouseUp(e.Location);
        }
    }
}
