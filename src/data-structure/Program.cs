using System;
using System.Text;

namespace Ds
{
    class Program
    {
        static void Main(string[] args)
        {
            ushort short11 = 1024;
            DecimalToBinary(6);

            Console.ReadKey();
        }

        private static void DecimalToBinary(int d)
        {
            var builder = new StringBuilder();
            for (var i = 63; i >= 0; --i)
            {
                if (((d >> i) & 1) == 1)
                    builder.Append('1');
                else
                    builder.Append('0');
                //if (!(((i >> 2) & 1) == 1))     // if 'i' is divisible by 4 append space.
                //    builder.Append(' ');
            }

            var str = builder.ToString();
            Console.WriteLine(str);
            Console.WriteLine(Convert.ToString(d, 2));
        }
    }
}
