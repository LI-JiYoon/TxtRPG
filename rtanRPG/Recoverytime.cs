using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    internal class Recoverytime
    {
        private Player player;
        float price = 500;

        public Recoverytime(Player player)
        {
            this.player = player;
        }

        public void Displaying()
        {
            Console.Clear();

            string text =

                "       _\r\n     _|=|__________\r\n    /              \\\r\n   /                \\\r\n  /__________________\\\r\n   ||  || /--\\ ||  ||\r\n   ||[]|| | .| ||[]||\r\n ()||__||_|__|_||__||()\r\n( )|-|-|-|====|-|-|-|( ) \r\n^^^^^^^^^^====^^^^^^^^^^^"

                + "\r\n\r\n" +
                "회복하기\r\n" +
                "체력을 회복할 수 있습니다. " +

                "1. 회복하기\r\n" +
                "0. 나가기\r\n\r\n" +
                "원하시는 행동을 입력해주세요.\r\n" +
                ">>";

            Console.WriteLine(text);


            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (input == "0")
                {
                    Location.SetLocation(STATE.마을);
                    break;
                }
                else if (input == "1")
                {
                    // mp, hp 랜덤 회복
                    Random random = new Random();

                    player.HP += random.Next(0, 51);
                    player.MP += random.Next(0, 51);

                    Console.WriteLine("회복을 완료했습니다.");
                    Console.ReadKey();
                    
                    break;

                }

                else { Console.WriteLine("잘못된 입력입니다."); }
                Location.SetLocation(STATE.마을);
            }

            Location.SetLocation(STATE.마을);

            MyLog.절취선();


        }
    }
}
