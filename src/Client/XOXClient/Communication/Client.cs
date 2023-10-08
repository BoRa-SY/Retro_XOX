using PacketCommunication;
using PacketCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XOXClient.Communication
{
    internal static class Client
    {
        private static PacketClient client;

        public static void Connect(string ip, int port)
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

            client = new PacketClient(new IPEndPoint(IPAddress.Parse(ip), port), packets);

            client.Connect();

            client.PacketReceived += Client_PacketReceived;
        }
        public static event EventHandler<BasePacket> packetReceived;

        private static void Client_PacketReceived(object sender, BasePacket e)
        {
            if (packetReceived != null) packetReceived(sender, e);
        }

        public static void SendPacket(BasePacket packet)
        {
            client.SendPacketAsync(packet);
        }
    }
}
