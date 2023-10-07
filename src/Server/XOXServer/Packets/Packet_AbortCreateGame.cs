using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXServer.Packets
{
    public class Packet_AbortCreateGame : BasePacket
    {
        public override byte ID => 2;
        public override int Length => 1;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            return new Packet_AbortCreateGame();
        }

        public override byte[] ToByteArray()
        {
            byte[] bytes = new byte[1];

            bytes[0] = PacketUtils.getFirstByteWithPacketID(ID, 3);

            return bytes;
        }
    }
}
