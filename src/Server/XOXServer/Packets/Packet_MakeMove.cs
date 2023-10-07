using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOXServer.Packets
{
    public class Packet_MakeMove : BasePacket
    {
        public override byte ID => 5;
        public override int Length => 1;

        public int CellIndex;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            byte cellIndexByte = bytes[0].GetBits(3, 4);
            return new Packet_MakeMove()
            {
                CellIndex = cellIndexByte
            };
        }

        public override byte[] ToByteArray()
        {
            if (CellIndex < 0 || CellIndex > 8) throw new Exception("Incorrect cell index");

            byte[] bytes = new byte[1];
            byte b1 = PacketUtils.getFirstByteWithPacketID(ID, 3);
            b1 |= (byte)(CellIndex << 1);
            bytes[0] = b1;
            return bytes;
        }
    }
}
