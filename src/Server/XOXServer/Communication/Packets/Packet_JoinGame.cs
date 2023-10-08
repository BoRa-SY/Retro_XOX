using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXServer.Communication.Packets
{
    public class Packet_JoinGame : BasePacket
    {
        public override byte ID => 3;
        public override int Length => 7;

        public string RoomCode;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            byte[] roomCodeBytes = new byte[6];

            bytes.CopyTo(roomCodeBytes, 0, 1, 6);

            return new Packet_JoinGame()
            {
                RoomCode = Encoding.UTF8.GetString(roomCodeBytes)
            };
        }

        public override byte[] ToByteArray()
        {
            if (RoomCode.Length != 6) throw new Exception("Incorrect Room Code Length");
            byte[] bytes = new byte[7];
            byte b1 = PacketUtils.getFirstByteWithPacketID(ID, 3);
            bytes[0] = b1;
            Encoding.UTF8.GetBytes(RoomCode).CopyTo(bytes, 1);

            return bytes;
        }
    }
}
