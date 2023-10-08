using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXServer.Communication.Packets
{
    public class Packet_JoinGameResponse : BasePacket
    {
        public override byte ID => 4;
        public override int Length => 1;

        public RoomState roomState;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            byte roomStateByte = bytes[0].GetBits(3, 2);

            return new Packet_JoinGameResponse
            {
                roomState = (RoomState)roomStateByte
            };
        }

        public override byte[] ToByteArray()
        {
            byte[] bytes = new byte[1];
            byte b1 = PacketUtils.getFirstByteWithPacketID(ID, 3);
            b1 |= (byte)((int)roomState << 3);
            bytes[0] = b1;
            return bytes;
        }

        public enum RoomState
        {
            Success = 0b00, // 0
            RoomFull = 0b01, // 1
            IncorrectRoomCode = 0b10 // 2
        }
    }
}
