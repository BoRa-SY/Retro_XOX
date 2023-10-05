using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOXClient
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("Enter");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            MessageBox.Show("Leave");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = !button1.Visible;
        }
    }
}
