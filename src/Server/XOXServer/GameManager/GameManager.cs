using PacketCommunication;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XOXServer.Communication.Packets;
using XOXServer.GameManager.Models;

namespace XOXServer.GameManager
{
    internal static class Games
    {
        private static List<Game> games = new List<Game>();

        public static async Task HandlePacket(BasePacket packet, Client client)
        {
            Type packetType = packet.GetType();

            if (packet is Packet_CreateGame) await CreateGame(client);

            else if (packet is Packet_AbortCreateGame) AbortCreateGame(client);

            else if (packet is Packet_JoinGame) await JoinGame(client, packet as Packet_JoinGame);

            else if (IsClientInAGame(client, out Game game))
            {
                await game.HandlePacket(packet, client);
            }
        }


        private static async Task JoinGame(Client client, Packet_JoinGame packet)
        {
            if (IsClientInAGame(client, out _)) return;
            
            Game game = games.Where(x => x.RoomCode == packet.RoomCode.ToLowerEN()).FirstOrDefault();


            if (game == null)
            {
                Packet_JoinGameResponse.RoomState roomState;

                roomState = Packet_JoinGameResponse.RoomState.IncorrectRoomCode;

                Packet_JoinGameResponse resp = new Packet_JoinGameResponse() { roomState = roomState };
                await client.SendPacketAsync(resp);
            }
            else if (game.player2 != null)
            {
                Packet_JoinGameResponse.RoomState roomState;

                roomState = Packet_JoinGameResponse.RoomState.RoomFull;
                Packet_JoinGameResponse resp = new Packet_JoinGameResponse() { roomState = roomState };
                await client.SendPacketAsync(resp);
            }
            else
            {
                Packet_JoinGameResponse.RoomState roomState;

                roomState = Packet_JoinGameResponse.RoomState.Success;

                Packet_JoinGameResponse resp = new Packet_JoinGameResponse() { roomState = roomState };

                await client.SendPacketAsync(resp);
                
                await game.SetPlayer2(client);
            }
        }

        private static async Task CreateGame(Client client)
        {
            if (IsClientInAGame(client, out _)) return;

            Game game = new Game(client, generateRoomCode());
            

            string roomCode = game.RoomCode;

            Packet_CreateGameResponse response = new Packet_CreateGameResponse() { RoomCode = roomCode };

            games.Add(game);

            await client.SendPacketAsync(response);

            Console.WriteLine($"Sent create game response (CREATED ROOM: {game.RoomCode.ToUpperEN()})");
            
        }

        private static void AbortCreateGame(Client client)
        {
            if (!IsClientInAGame(client, out Game game)) return;
            if (game.GameStarted) return;

            games.Remove(game);

            Console.WriteLine($"Aborted create game (ROOM CODE: {game.RoomCode.ToUpperEN()})");
        }


        private static bool IsClientInAGame(Client client, out Game game)
        {
            return (game = games.Where(x => x.player1 == client || x.player2 == client).FirstOrDefault()) != null;
        }
        private static string generateRoomCode()
        {
            const string allValidChars = "abcdefghijklmnopqrstuvwxyz123456789";

            string roomCode = "";
            do
            {
                lock (Program.rnd)
                {
                    for(int i = 0; i < 6; i++)
                    {
                        roomCode += allValidChars[Program.rnd.Next(allValidChars.Length - 1)];
                    }
                }

            } while (games.Any(x => x.RoomCode == roomCode));

            return roomCode;
        }
    }
}
