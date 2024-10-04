using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public static class MyLog
    {
        public static void 절취선()
        {
            Console.WriteLine("===================================");
        }

        public static int GetKoreanLength(int padding, string input)
        {
            int length = 0;

            foreach (char c in input)
            {
                if (c >= 0xAC00 && c <= 0xD7A3) length += 1; // 아숙희코드에서 한글 범위
            }

            return padding - length <= 0 ? padding : padding - length;
        }

    }
}
