using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOXClient.UCs;

namespace XOXClient
{
    public delegate void Callback();

    public partial class FormMain : Form
    {

        public FormMain()
        {
            InitializeComponent();

        }

        Size gridSize = new Size(50, 60);
        int sizeMultiplier = 10;

        #region UC
        UC_Main UCMain;

        UC_JoinGame UCJoinGame;
        UC_CreateGame UCCreateGame;
        UC_Game UCGame;

        private void setUCVisible(UserControl UC)
        {
            foreach(Control item in panelMain.Controls)
            {
                item.Visible = item == UC;
            }
        }
        #endregion

        #region Callbacks
        private void CB_SetToMainPage()
        {
            setUCVisible(UCMain);
        }

        private void CB_SetToJoinGame()
        {
            setUCVisible(UCJoinGame);
        }

        private void CB_SetToCreateGame()
        {
            setUCVisible(UCCreateGame);
        }
        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            Textures.Init();


            UCMain = new UC_Main(gridSize, sizeMultiplier, CB_SetToCreateGame, CB_SetToJoinGame);
            UCJoinGame = new UC_JoinGame(gridSize, sizeMultiplier, CB_SetToMainPage);
            UCCreateGame = new UC_CreateGame(gridSize, sizeMultiplier, CB_SetToMainPage);
            UCGame = new UC_Game(gridSize, sizeMultiplier);

            UCMain.Size = panelMain.Size;
            UCJoinGame.Size = panelMain.Size;
            UCCreateGame.Size = panelMain.Size;
            UCGame.Size = panelMain.Size;

            panelMain.Controls.AddRange(new Control[] { UCMain, UCJoinGame, UCCreateGame, UCGame });

            setUCVisible(UCMain);

        }

        private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (UCJoinGame.Visible)
            {
                UCJoinGame.OnKeyPress(e.KeyChar);
                e.Handled = true;
            }
        }
    }
}
