using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXClient.Communication.Packets
{
    public class Packet_CreateGame : BasePacket
    {
        public override byte ID => 0;
        public override int Length => 1;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            return new Packet_CreateGame();
        }

        public override byte[] ToByteArray()
        {
            byte[] bytes = new byte[1];

            bytes[0] = PacketUtils.getFirstByteWithPacketID(ID, 3);

            return bytes;
        }
    }
}
