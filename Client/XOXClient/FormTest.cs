using PixelBuilder;
using PixelBuilder.Abs;
using PixelBuilder.Components;
using PixelBuilder.TextureUtils;
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
    public partial class FormTest : Form
    {
        PixelForm form;


        PixelTextInputComponent inpComp;
        
        public FormTest()
        {
            InitializeComponent();

            inpComp = new PixelTextInputComponent("textinput1", new Point(1, 40), getCharmap(), 6, 1, true, 1, 1, Color.White);

            PixelComponent[] comps = new PixelComponent[]
            {
                new PixelXOXGridComponent("xox", new Point(1,1), Resources.X, Resources.O, Color.White)
            };

            form = new PixelForm(new Size(50,60), 10, Color.FromArgb(0,188,212), comps);
            form.OnRedraw += Form_OnRedraw;

            Form_OnRedraw(null, null);
        }

        Dictionary<char, Bitmap> getCharmap()
        {
            const int charsLength = 35;

            TextureFile textureFile = new TextureFile(Resources.chars, charsLength, new Size(5, 7));

            Dictionary<char, Bitmap> map = new Dictionary<char, Bitmap>();

            Bitmap[] bitmaps = new Bitmap[charsLength];
            string chars = "abcdefghijklmnopqrstuvwxyz123456789";

            for (int i = 0; i < charsLength; i++)
            {
                Bitmap bmp = textureFile.getTextureByIndex(i);
                bitmaps[i] = bmp;
            }

            for(int i = 0; i < charsLength; i++)
            {
                map.Add(chars[i], bitmaps[i]);
            }

            return map;
        }

        #region Essential Events

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


        #endregion



        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormTest_KeyPress(object sender, KeyPressEventArgs e)
        {
            inpComp.triggerCharInput(e.KeyChar);
        }
    }
}
