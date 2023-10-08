using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication
{
    public static class PacketUtils
    {
        public static byte getFirstByteWithPacketID(int packetID, int packetIDBitLength)
        {
            byte b = (byte)(packetID << (8 - packetIDBitLength));
            return b;
        }
    }
}
