using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public static class art
    {
        //public static void title()
        //{
        //    string[] asciiArt = {
        //        "           ███     ███                  ███                █████████████            ██              ",
        //        "       ███████████ ███      ████ █████  ███        ███         █████                ███             ",
        //        "          ██████   ███        ██   ███  ███       ████     █████   ██████         ██████            ",
        //        "        ███    ███ ██████    ███   ██   ███      █████                         ████    ████         ",
        //        "        ███   ████ ███      ███    ██   ███    ███ ███   █████████████████    ██          ██        ",
        //        "          ██████   ███     ███    ██    ███  ███   ███           ██              ███   ██           ",
        //        "          ██       ███     ██    ███    ███  ███████████   ███       ███         ███   ██           ",
        //        "          ██                    ███     ███        ███     █████████████    ██████████████████      ",
        //        "          ████████████                  ███         ██     █████████████                             "
        //    };


        //    // 물결 효과 적용
        //    int waveHeight = 2;  // 물결의 높이
        //    int waveSpeed = 150; // 프레임 딜레이 (밀리초)

        //    while (true)
        //    {
        //        for (int frame = 0; frame < 2 * waveHeight; frame++)
        //        {
        //            Console.Clear();

        //            // 각 줄을 위아래로 물결치게 이동
        //            for (int i = 0; i < asciiArt.Length; i++)
        //            {
        //                int offset = (int)(Math.Sin((i + frame) * 0.5) * waveHeight); // 사인 함수를 이용해 물결 효과 적용
        //                Console.SetCursorPosition(0, i + offset); // y축 위치를 조절해 물결효과
        //                Console.WriteLine(asciiArt[i]);
        //            }

        //            Thread.Sleep(waveSpeed); // 다음 프레임으로 넘어가기 전 잠시 대기
        //        }
        //    }
        //}
        public static void vidtory()
        {

            string[] fireworkFrames = new string[]
            {
                //0단게: 사전준비
                @"
                  
                  
                  
                  *",
                @"
                  
                  
                  |
                  *",
                 @"
                  
                  |
                  |
                  *",
                // 1단계: 폭죽 올라가는 모습
                @"
                  |
                  |
                  |
                  *",

                // 2단계: 폭죽이 터지기 전 작은 불꽃
                @"
                  *
                 / \
                * | *
                 \ /",

                // 3단계: 폭죽이 터지는 모습
                @"
                  *
                 * * *
                *  |  *
                 * * *
                  *",

                // 4단계: 폭죽이 크게 터진 모습
                @"
               *     *
              *  * *  *
            *   * | *   *
              *  * *  *
               *     *",

                // 5단계: 폭죽이 사라지는 모습
                @"
                 .  * .
                *  . |  .
                 . * * .
                .  * .  .
                 .     ."
            };

            // 애니메이션 재생
            foreach (var frame in fireworkFrames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                Thread.Sleep(500); // 0.5초 딜레이
            }


        }

    }
}




