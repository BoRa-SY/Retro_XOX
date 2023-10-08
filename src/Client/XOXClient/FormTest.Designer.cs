namespace XOXClient
{
    partial class FormTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonCreateRoom = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonAbortCreateRoom = new System.Windows.Forms.Button();
            this.buttonJoinRoom = new System.Windows.Forms.Button();
            this.textBoxRoomCode = new System.Windows.Forms.TextBox();
            this.textBoxCellIndex = new System.Windows.Forms.TextBox();
            this.buttonMakeMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(121, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonCreateRoom
            // 
            this.buttonCreateRoom.Location = new System.Drawing.Point(12, 66);
            this.buttonCreateRoom.Name = "buttonCreateRoom";
            this.buttonCreateRoom.Size = new System.Drawing.Size(121, 23);
            this.buttonCreateRoom.TabIndex = 1;
            this.buttonCreateRoom.Text = "Create Room";
            this.buttonCreateRoom.UseVisualStyleBackColor = true;
            this.buttonCreateRoom.Click += new System.EventHandler(this.buttonCreateRoom_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(490, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(298, 426);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // buttonAbortCreateRoom
            // 
            this.buttonAbortCreateRoom.Location = new System.Drawing.Point(12, 95);
            this.buttonAbortCreateRoom.Name = "buttonAbortCreateRoom";
            this.buttonAbortCreateRoom.Size = new System.Drawing.Size(121, 23);
            this.buttonAbortCreateRoom.TabIndex = 3;
            this.buttonAbortCreateRoom.Text = "Abort Create Room";
            this.buttonAbortCreateRoom.UseVisualStyleBackColor = true;
            this.buttonAbortCreateRoom.Click += new System.EventHandler(this.buttonAbortCreateRoom_Click);
            // 
            // buttonJoinRoom
            // 
            this.buttonJoinRoom.Location = new System.Drawing.Point(12, 134);
            this.buttonJoinRoom.Name = "buttonJoinRoom";
            this.buttonJoinRoom.Size = new System.Drawing.Size(121, 23);
            this.buttonJoinRoom.TabIndex = 4;
            this.buttonJoinRoom.Text = "Join Room";
            this.buttonJoinRoom.UseVisualStyleBackColor = true;
            this.buttonJoinRoom.Click += new System.EventHandler(this.buttonJoinRoom_Click);
            // 
            // textBoxRoomCode
            // 
            this.textBoxRoomCode.Location = new System.Drawing.Point(139, 137);
            this.textBoxRoomCode.Name = "textBoxRoomCode";
            this.textBoxRoomCode.Size = new System.Drawing.Size(72, 20);
            this.textBoxRoomCode.TabIndex = 5;
            // 
            // textBoxCellIndex
            // 
            this.textBoxCellIndex.Location = new System.Drawing.Point(139, 183);
            this.textBoxCellIndex.Name = "textBoxCellIndex";
            this.textBoxCellIndex.Size = new System.Drawing.Size(72, 20);
            this.textBoxCellIndex.TabIndex = 7;
            // 
            // buttonMakeMove
            // 
            this.buttonMakeMove.Location = new System.Drawing.Point(12, 180);
            this.buttonMakeMove.Name = "buttonMakeMove";
            this.buttonMakeMove.Size = new System.Drawing.Size(121, 23);
            this.buttonMakeMove.TabIndex = 6;
            this.buttonMakeMove.Text = "Make Move";
            this.buttonMakeMove.UseVisualStyleBackColor = true;
            this.buttonMakeMove.Click += new System.EventHandler(this.buttonMakeMove_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxCellIndex);
            this.Controls.Add(this.buttonMakeMove);
            this.Controls.Add(this.textBoxRoomCode);
            this.Controls.Add(this.buttonJoinRoom);
            this.Controls.Add(this.buttonAbortCreateRoom);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonCreateRoom);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonCreateRoom;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonAbortCreateRoom;
        private System.Windows.Forms.Button buttonJoinRoom;
        private System.Windows.Forms.TextBox textBoxRoomCode;
        private System.Windows.Forms.TextBox textBoxCellIndex;
        private System.Windows.Forms.Button buttonMakeMove;
    }
}