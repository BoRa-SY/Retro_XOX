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
            Communication.Listener.Start("127.0.0.1", 1234);

            await Task.Delay(-1);
        }
    }
}
