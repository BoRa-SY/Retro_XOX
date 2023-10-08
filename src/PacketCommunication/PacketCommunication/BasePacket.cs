using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication
{
    public abstract class BasePacket
    {
        public abstract byte ID { get; }
        public abstract int Length { get; }

        public abstract byte[] ToByteArray();


        public BasePacket FromByteArray(byte[] bytes)
        {
            byte _id = bytes[0].GetBits(0, 3);
            if (ID != _id || bytes.Length != Length) throw new Exception("Incorrect Packet Passed");
            return fromByteArray(bytes);
        }

        protected abstract BasePacket fromByteArray(byte[] bytes);
    }
}