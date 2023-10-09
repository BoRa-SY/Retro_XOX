using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOXServer.Communication.Packets;

namespace XOXServer
{
    internal class Program
    {
        public static Random rnd = new Random();

        static async Task Main(string[] args)
        {
            string ipPortFilePath = "ipport.txt";
            if(!File.Exists(ipPortFilePath)) { Console.WriteLine("ipport.txt not found"); Console.ReadLine(); return; }

            string[] ipPortPairs = File.ReadAllLines(ipPortFilePath);

            Communication.Listener.Start(ipPortPairs[0], int.Parse(ipPortPairs[1]));

            await Task.Delay(-1);
        }
    }
}
