using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCommunication
{
    public static class Extensions
    {
        public static void CopyTo(this byte[] source, byte[] destination, int destinationOffset, int sourceOffset, int count)
        {
            for(int i = 0; i < count; i++)
            {
                int sourceIndex = sourceOffset + i;
                int destinationIndex = destinationOffset + i;

                destination[destinationIndex] = source[sourceIndex];
            }
        }

        // 01110000
        // Start Index: 2
        // Count: 3
        // Desired Output: 111 (00000111)
        public static byte GetBits(this byte b, int startIndex, int count)
        {
            byte s1 = (byte)(b << startIndex);
            byte res = (byte)(s1 >> (8 - count));
            return res;
        }
    }
}
