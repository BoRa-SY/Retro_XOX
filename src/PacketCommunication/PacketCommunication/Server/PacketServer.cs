using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacketCommunication.Server
{
    public class PacketServer
    {
        public event EventHandler<Client> ClientConnected;
        public event EventHandler<Client> ClientDisconnected;

        public event EventHandler<Exception> ServerErrored;
        public event EventHandler<IncomingData> PacketReceived;
        public PacketServer(IPEndPoint EP, PacketCollection packets)
        {
            this.Listener = new TcpListener(EP);
            this.packets = packets;
        }

        private PacketCollection packets;

        private TcpListener Listener;

        public bool Listening { get; private set; }
        public void StartListening()
        {
            Listener.Start();
            Listening = true;

            _ = Task.Run(() =>
            {
                try
                {
                    while (Listening)
                    {
                        TcpClient client = Listener.AcceptTcpClient();
                        _ = HandleClientAsync(client);
                    }
                }
                catch (Exception ex)
                {
                    if (Listening && ServerErrored != null) ServerErrored(this, ex);
                }
                finally
                {
                    if(Listening)
                    StopListening();
                }
            });
        }

        public void StopListening()
        {
            Listener.Stop();
            Listening = false;
        }

        internal async Task SendPacketAsync(TcpClient client, BasePacket packet)
        {
            await NetworkStreamUtils.SendPacketAsync(client.GetStream(), packet);
        }

        private async Task HandleClientAsync(TcpClient tcpClient)
        {
            Client cli = new Client(tcpClient, this);
            if (ClientConnected != null) ClientConnected(this, cli);


            try
            {
                NetworkStream stream = tcpClient.GetStream();

                while (tcpClient.Connected && Listening)
                {
                    BasePacket incomingPacket = await NetworkStreamUtils.ReadPacketAsync(stream, packets);

                    if (incomingPacket == null) continue;

                    Console.WriteLine("Received Packet");


                    try
                    {
                        if (PacketReceived != null) PacketReceived(this, new IncomingData(cli, incomingPacket));
                    }
                    catch (Exception) { }

                }

            }
            catch (IOException ex)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (ClientDisconnected != null) ClientDisconnected.Invoke(this, cli);
                tcpClient.Close();
                tcpClient.Dispose();
            }

        }
    }
}
