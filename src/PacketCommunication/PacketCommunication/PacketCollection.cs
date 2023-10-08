using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication
{
    public class PacketCollection
    {
        Dictionary<byte, Type> packets;

        internal int packetIDBitLength;

        public PacketCollection(int packetIDBitLength, params Type[] types)
        {
            if (packetIDBitLength < 1 || packetIDBitLength > 8) throw new Exception("Packet ID Bit Length must be between 1 and 8");

            this.packetIDBitLength = packetIDBitLength;
            AddPackets(types);
        }

        private void AddPackets(Type[] types)
        {
            Type basePacketType = typeof(BasePacket);

            packets = new Dictionary<byte, Type>();


            for(int i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                if (!type.IsSubclassOf(basePacketType)) throw new Exception("Invalid packet type");

                object instanceObj = Activator.CreateInstance(type);

                var instance = (BasePacket)instanceObj;

                packets.Add(instance.ID, type);
            }

        }

        internal BasePacket getPacketInstanceByID(byte packetID)
        {
            if (!packets.ContainsKey(packetID)) return null;

            Type type = packets[packetID];

            object instanceObj = Activator.CreateInstance(type);

            var instance = (BasePacket)instanceObj;

            return instance;
        }
    }

}
