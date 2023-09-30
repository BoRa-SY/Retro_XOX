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

            PixelComponent[] comps = new PixelComponent[]
            {
                new PixelButtonComponent("btn1", new Point(1,1), Resources.button, Resources.buttonDown, Resources.buttonHover),
                new PixelButtonComponent("btn2", new Point(1,12), Resources.button, Resources.buttonDown, Resources.buttonHover)
            };



            form = new PixelForm(new Size(50,60), 10, Color.Red, comps);
            form.OnRedraw += Form_OnRedraw;

            Form_OnRedraw(null, null);



            tf.getTextureByIndex(0);
            tf.getTextureByIndex(1);
            tf.getTextureByIndex(2);
            tf.getTextureByIndex(3);
            tf.getTextureByIndex(4);
            tf.getTextureByIndex(5);
            tf.getTextureByIndex(6);
            tf.getTextureByIndex(7);
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

        int index = 0;
        TextureFile tf = new TextureFile((Bitmap)Bitmap.FromFile(@"D:\texture.png"), 4, new Size(10, 10));

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = tf.getTextureByIndex(index++);
        }
    }
}
