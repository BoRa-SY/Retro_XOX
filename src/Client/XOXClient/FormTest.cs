using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketCommunication;
using XOXClient.Communication.Packets;

namespace XOXClient
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();

        }

        private void Client_packetReceived(object sender, BasePacket e)
        {
            if (e is Packet_CreateGameResponse)
            {
                var packet = e as Packet_CreateGameResponse;
                richTextBox1.Text += $"\n Received create game response (Room Code: {packet.RoomCode.ToUpper()})";
            }
            else if (e is Packet_JoinGameResponse)
            {
                var packet = e as Packet_JoinGameResponse;
                richTextBox1.Text += $"\n Received join game response (Room State: {Enum.GetName(typeof(Packet_JoinGameResponse.RoomState), packet.roomState)})";
            }
            else if(e is Packet_NewMoveBroadcast)
            {
                var packet = e as Packet_NewMoveBroadcast;
                richTextBox1.Text += $"\nReceived new move BC // Cell Index: {packet.CellIndex} // Played By: {Enum.GetName(typeof(Packet_NewMoveBroadcast.PlayerState), packet.playerState)}";
            }
            else if (e is Packet_GameStartBroadcast) richTextBox1.Text += $"\n Game Started";
            else richTextBox1.Text += "Unknown Packet";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Communication.Client.Connect("127.0.0.1", 1234);

           // Communication.Client.packetReceived += Client_packetReceived;

            buttonConnect.Enabled = false;

            richTextBox1.Text += $"Connected";
        }

        private void buttonCreateRoom_Click(object sender, EventArgs e)
        {
            Packet_CreateGame packet = new Packet_CreateGame();
            Communication.Client.SendPacket(packet);
        }

        private void buttonAbortCreateRoom_Click(object sender, EventArgs e)
        {
            Packet_AbortCreateGame packet = new Packet_AbortCreateGame();

            Communication.Client.SendPacket(packet);
        }

        private void buttonJoinRoom_Click(object sender, EventArgs e)
        {
            Packet_JoinGame packet = new Packet_JoinGame() { RoomCode = textBoxRoomCode.Text };

            Communication.Client.SendPacket(packet);
        }

        private void buttonMakeMove_Click(object sender, EventArgs e)
        {
            Packet_MakeMove packet = new Packet_MakeMove() { CellIndex = int.Parse(textBoxCellIndex.Text) };
            Communication.Client.SendPacket(packet);
        }
    }
}
