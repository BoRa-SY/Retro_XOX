using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication.Server
{
    public class IncomingData
    {
        public Client Client { get; private set; }
        public BasePacket Packet { get; private set; }
        internal IncomingData(Client cli, BasePacket packet)
        {
            this.Client = cli;
            this.Packet = packet;
        }

    }

}
