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

        public Player player;

        protected override BasePacket fromByteArray(byte[] bytes)
        {
            return new Packet_GameStartBroadcast()
            {
                player = (Player)bytes[0].GetBits(7, 1)
            };
        }

        public override byte[] ToByteArray()
        {
            return new byte[] { (byte)(PacketUtils.getFirstByteWithPacketID(ID, 3) | (int)player )};
        }

        public enum Player
        {
            X = 0b0,
            O = 0b1
        }
    }
}
