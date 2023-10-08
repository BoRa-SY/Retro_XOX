using PacketCommunication;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XOXServer.GameManager;

namespace XOXServer.Communication
{
    internal static class Listener
    {
        public static PacketServer Server;

        public static void Start(string ip, int port)
        {
            PacketCollection packets = new PacketCollection(3,
                typeof(Packets.Packet_AbortCreateGame),
                typeof(Packets.Packet_CreateGame),
                typeof(Packets.Packet_CreateGameResponse),
                typeof(Packets.Packet_GameStartBroadcast),
                typeof(Packets.Packet_JoinGame),
                typeof(Packets.Packet_JoinGameResponse),
                typeof(Packets.Packet_MakeMove),
                typeof(Packets.Packet_NewMoveBroadcast)
                );



            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(ip), port);
            Server = new PacketServer(EP, packets);

            Server.ClientConnected += Server_ClientConnected;
            Server.ClientDisconnected += Server_ClientDisconnected;
            Server.PacketReceived += Server_PacketReceived;
            Server.ServerErrored += Server_ServerErrored;

            Server.StartListening();


            Console.WriteLine($"Started Listening\nIP: {ip}\nPORT: {port}");
        }

        private static async void Server_PacketReceived(object sender, IncomingData e)
        {
            await Games.HandlePacket(e.Packet, e.Client);
        }

        private static void Server_ClientDisconnected(object sender, Client e)
        {
            Console.WriteLine("Client Disconnected");
        }

        private static void Server_ClientConnected(object sender, Client e)
        {
            Console.WriteLine("Client Connected");
        }

        private static void Server_ServerErrored(object sender, Exception e)
        {
            Console.WriteLine($"[CRITICAL] An error occured while listening\nError: {e.Message}");
        }


    }
}
