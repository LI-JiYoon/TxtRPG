using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    internal class breaktime
    {


        private Player player; // 플레이어 객체를 저장할 필드
        float price = 500;

        public breaktime(Player player)
        {
            this.player = player;
        }

        public void Displaying()
        {
            string text =

                "       _\r\n     _|=|__________\r\n    /              \\\r\n   /                \\\r\n  /__________________\\\r\n   ||  || /--\\ ||  ||\r\n   ||[]|| | .| ||[]||\r\n ()||__||_|__|_||__||()\r\n( )|-|-|-|====|-|-|-|( ) \r\n^^^^^^^^^^====^^^^^^^^^^^"

                + "\r\n\r\n" + 
                "휴식하기\r\n" +
                "500 G 를 내면 체력을 회복할 수 있습니다. " +
                $"(보유 골드 : {player.gold} G)\r\n\r\n" +
                "1. 휴식하기\r\n" +
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
                    if(player.gold >=500)
                    {

                        player.HP = 100;
                        player.gold -= price;
                        Console.WriteLine("휴식을 완료했습니다.");
                    }
                    else 
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                    break;

                }
                else { Console.WriteLine("잘못된 입력입니다."); }

            }

            MyLog.절취선();


        }
     
    }
}
