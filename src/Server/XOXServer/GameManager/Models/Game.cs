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

        public Player nextPlayer = Player.X;

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

            GameStarted = true;


            await player1.SendPacketAsync(new Packet_GameStartBroadcast() { player = Packet_GameStartBroadcast.Player.X });
            await player2.SendPacketAsync(new Packet_GameStartBroadcast() { player = Packet_GameStartBroadcast.Player.O });

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

                if (nextPlayer == Player.X && client == player1) { cells[cellIndex] = CellState.X; }
                else if (nextPlayer == Player.O && client == player2) { cells[cellIndex] = CellState.O; }
                else return;

                WinState winState = CheckWin();


                Packet_NewMoveBroadcast bc = new Packet_NewMoveBroadcast()
                {
                    CellIndex = cellIndex,
                    playerState = (Packet_NewMoveBroadcast.PlayerState)(int)nextPlayer,
                    winState = (Packet_NewMoveBroadcast.WinState)(int)winState
                };

                nextPlayer = nextPlayer == Player.X ? Player.O : Player.X;


                await Broadcast(bc);

                if (winState != WinState.None) GameManager.Games.RemoveGame(this);
            }
        }


        private async Task Broadcast(BasePacket packet)
        {
            await player1.SendPacketAsync(packet);
            await player2.SendPacketAsync(packet);
        }

        /*
         * 
         * 0 / 1 / 2
         * 3 / 4 / 5
         * 6 / 7 / 8
         * 
         */

        /*
         * 0 1 2
         * 3 4 5
         * 6 7 8
         * 
         * 0 3 6
         * 1 4 7
         * 2 5 8
         * 
         * 0 4 8
         * 2 4 6
         */
        private WinState CheckWin()
        {
            int[] WinCombinations = new int[]
            {
              //  012345678
                0b111000000,
                0b000111000,
                0b000000111,
                0b100100100,
                0b010010010,
                0b001001001,
                0b100010001,
                0b001010100,
            };

            int player1Map = getPlayerMap(Player.X);
            int player2Map = getPlayerMap(Player.O);

            bool player1State = checkPlayerWin(player1Map);
            bool player2State = checkPlayerWin(player2Map);

            if (player1State) return WinState.X;
            else if (player2State) return WinState.O;
            else if (cells.Where(x => x == CellState.Empty).Count() == 0) return WinState.Tie;
            else return WinState.None;

            bool checkPlayerWin(int playerMap)
            {
                for(int i = 0; i < WinCombinations.Length; i++)
                {
                    if ((WinCombinations[i] & playerMap) == WinCombinations[i]) return true;
                }
                return false;
            }

            int getPlayerMap(Player player)
            {
                CellState requiredCellState = (CellState)((int)player + 1);

                int playerMap = 0;

                for(int i = 0; i < cells.Length; i++)
                {
                    int state = cells[i] == requiredCellState ? 1 : 0;

                    if (state == 0) continue;

                    playerMap |= (1 << (8 - i));
                }

                return playerMap;
            }
        }


        private enum WinState
        {
            None = 0,
            X,
            O,
            Tie
        }

        public enum Player
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
