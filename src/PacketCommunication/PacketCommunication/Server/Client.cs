using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication.Server
{
    public class Client
    {
        internal TcpClient TCPClient;
        public PacketServer Server { get; private set; }

        internal Client(TcpClient client, PacketServer server)
        {
            this.TCPClient = client;
            this.Server = server;
        }
        
        public async Task SendPacketAsync(BasePacket packet)
        {
            await Server.SendPacketAsync(TCPClient, packet);
        }


    }
}
