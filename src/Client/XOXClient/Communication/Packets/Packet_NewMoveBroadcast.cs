using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXClient.Communication.Packets
{
    public class Packet_NewMoveBroadcast : BasePacket
    {
        public override byte ID => 6;
        public override int Length => 2;

        public int CellIndex;
        public PlayerState playerState;
        public WinState winState;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            int _cellIndex = bytes[0].GetBits(3, 4);
            PlayerState _playerState = (PlayerState)bytes[0].GetBits(7, 1);
            WinState _winState = (WinState)bytes[1];

            return new Packet_NewMoveBroadcast()
            {
                CellIndex = _cellIndex,
                winState = _winState,
                playerState = _playerState
            };
        }

        public override byte[] ToByteArray()
        {
            if (CellIndex < 0 || CellIndex > 8) throw new Exception("Incorrect cell index");

            byte[] bytes = new byte[2];

            byte b1 = PacketUtils.getFirstByteWithPacketID(ID, 3);
            b1 |= (byte)(CellIndex << 1);
            b1 |= (byte)playerState;
            byte b2 = (byte)winState;

            bytes[0] = b1;
            bytes[1] = b2;

            return bytes;
        }



        public enum WinState
        {
            None = 0b00, // 0
            XWon = 0b01, // 1
            OWon = 0b10, // 2
            Tie = 0b11 // 3
        }

        public enum PlayerState
        {
            X = 0b0,
            O = 0b1
        }
    }
}
