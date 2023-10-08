using PacketCommunication;
using PacketCommunication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XOXClient.Communication.Packets;
using XOXClient.UCs;

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

        private static void Client_PacketReceived(object sender, BasePacket e)
        {
            if(e is Packet_GameStartBroadcast)
            {
                var packet = e as Packet_GameStartBroadcast;

                Program.formMain.UCGame.currentPlayer = (UC_Game.Player)(int)packet.player;
                Program.formMain.CB_SetToGame();
            }
            else if(e is Packet_NewMoveBroadcast)
            {
                var packet = e as Packet_NewMoveBroadcast;
                Program.formMain.UCGame.NewMoveReceived(packet);
            }
        }

        public static async Task SendPacket(BasePacket packet)
        {
            await client.SendPacketAsync(packet);
        }

        public static async Task<DesiredResponseType> SendPacketAndWaitForResponse<DesiredResponseType>(BasePacket packet, int timeoutSeconds) where DesiredResponseType : BasePacket
        {

            client.SendPacketAsync(packet).Wait();

            DesiredResponseType receivedPacket = null;


            EventHandler<BasePacket> packetReceivedHandler = (sender, e) =>
            {
                if (e is DesiredResponseType) receivedPacket = (DesiredResponseType)e;
            };

            client.PacketReceived += packetReceivedHandler;

            DateTime loopStart = DateTime.Now;

            int timeoutMS = timeoutSeconds * 1000;
            int timeLeft = timeoutMS;

            while (receivedPacket == null && timeLeft > 0)
            {
                await Task.Delay(100);
                timeLeft -= 100;
            }

            client.PacketReceived -= packetReceivedHandler;

            return receivedPacket;
        }
    }
}
