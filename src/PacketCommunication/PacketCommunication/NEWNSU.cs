using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication
{
    internal class NewNSU
    {
        public async Task<BasePacket> ReadPacketAsync(NetworkStream stream, PacketCollection packets)
        {
            try
            {
                byte[] firstByte = new byte[1];

                await stream.ReadAsync(firstByte, 0, 1);

                byte packetID = (byte)(firstByte[0] >> (8 - packets.packetIDBitLength));

                BasePacket packet = packets.getPacketInstanceByID(packetID);

                int remainingLength = packet.Length - 1;


                byte[] remainingBytes = new byte[remainingLength];

                int bytesLeft = remainingLength;

                while (bytesLeft > 0)
                {
                    int offset = remainingLength - bytesLeft;

                    int i = stream.Read(remainingBytes, offset, bytesLeft);

                    bytesLeft -= i;
                }

                byte[] bytesCombined = new byte[packet.Length];
                bytesCombined[0] = firstByte[0];
                remainingBytes.CopyTo(bytesCombined, 1);

                return packet.FromByteArray(bytesCombined);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task SendPacketAsync(NetworkStream stream, BasePacket packet)
        {
            byte[] bytes = packet.ToByteArray();
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
