using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXServer.Communication.Packets
{
    public class Packet_GameStartBroadcast : BasePacket
    {
        public override byte ID => 7;
        public override int Length => 1;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            return new Packet_GameStartBroadcast();
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { PacketUtils.getFirstByteWithPacketID(ID, 3) };
        }
    }
}
