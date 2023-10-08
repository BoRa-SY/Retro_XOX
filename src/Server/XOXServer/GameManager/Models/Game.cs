using PacketCommunication;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOXServer.Communication.Packets;

namespace XOXServer.GameManager.Models
{
    internal class Game
    {
        public Client player1;
        public Client player2 = null;

        public NextPlayer nextPlayer = NextPlayer.X;

        public CellState[] cells = new CellState[9];


        public string RoomCode;

        public bool GameStarted = false;

        public Game(Client player1, string roomCode)
        {
            this.player1 = player1;
            this.RoomCode = roomCode;
        }

        public async Task SetPlayer2(Client client)
        {
            player2 = client;
            var bc = new Packet_GameStartBroadcast();

            GameStarted = true;

            await Broadcast(bc);

        }

        public async Task HandlePacket(BasePacket packet, Client client)
        {
            if (!GameStarted) return;

            if(packet is Packet_MakeMove)
            {
                Packet_MakeMove movePacket = packet as Packet_MakeMove;
                if (movePacket.CellIndex < 0 || movePacket.CellIndex > 8) return;

                int cellIndex = movePacket.CellIndex;

                if (cells[cellIndex] != CellState.Empty) return;

                if (nextPlayer == NextPlayer.X && client == player1) { cells[cellIndex] = CellState.X; }
                else if (nextPlayer == NextPlayer.O && client == player2) { cells[cellIndex] = CellState.O; }
                else return;

                Packet_NewMoveBroadcast bc = new Packet_NewMoveBroadcast()
                {
                    CellIndex = cellIndex,
                    playerState = (Packet_NewMoveBroadcast.PlayerState)(int)nextPlayer,
                    winState = Packet_NewMoveBroadcast.WinState.None
                };

                nextPlayer = nextPlayer == NextPlayer.X ? NextPlayer.O : NextPlayer.X;


                await Broadcast(bc);

            }
        }


        private async Task Broadcast(BasePacket packet)
        {
            await player1.SendPacketAsync(packet);
            await player2.SendPacketAsync(packet);

        }


        public enum NextPlayer
        {
            X = 0,
            O
        }

        public enum CellState
        {
            Empty = 0,
            X,
            O
        }
    }
}
