using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication.Client
{
    public class PacketClient
    {
        private TcpClient client;
        private IPEndPoint EP;

        public event EventHandler ClientDisconnected;
        public event EventHandler<BasePacket> PacketReceived;
        public bool Connected { get { return client.Connected; } }

        public PacketClient(IPEndPoint EP, PacketCollection packets)
        {
            this.EP = EP;
            this.client = new TcpClient();
        }

        private PacketCollection packets;

        public void Connect()
        {
            client.Connect(EP);
            
        }


        public async Task SendPacketAsync(BasePacket packet)
        {
            await NetworkStreamUtils.SendPacketAsync(client.GetStream(), packet);
        }

        private async Task HandleIncomingAsync()
        {

            try
            {

                NetworkStream stream = client.GetStream();

                while (client.Connected)
                {
                    BasePacket packet = await NetworkStreamUtils.ReadPacketAsync(stream, packets);

                    if (packet == null) continue;

                    if (PacketReceived != null) PacketReceived(this, packet);
                }

            }
            catch (IOException)
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                client.Close();

                if (Connected && ClientDisconnected != null) ClientDisconnected.Invoke(this, new EventArgs());

                client.Dispose();
            }
        }
    }
}
