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
        private Player player;
        float price = 500;

        public breaktime(Player player)
        {
            this.player = player;
        }

        public void Displaying()
        {
            Console.Clear();

            string text =

                "       _\r\n     _|=|__________\r\n    /              \\\r\n   /                \\\r\n  /__________________\\\r\n   ||  || /--\\ ||  ||\r\n   ||[]|| | .| ||[]||\r\n ()||__||_|__|_||__||()\r\n( )|-|-|-|====|-|-|-|( ) \r\n^^^^^^^^^^====^^^^^^^^^^^"

                + "\r\n\r\n" +
                "휴식하기\r\n" +
                $"{player.name} 님! 이곳에서 잠시 쉬어가는 건 어때요?\r\n\r\n" +

                "1. 회복하기\r\n" +
                "2. 나들이\r\n" +
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
                    BreaktimeDisplaying();
                    break;

                }
                else if (input == "2")
                {
                    FreeBreaktimeDisplay();
                    break;

                }
                else { Console.WriteLine("잘못된 입력입니다."); }
                Location.SetLocation(STATE.마을);
            }

            Location.SetLocation(STATE.마을);

            MyLog.절취선();
        }
    
    public void BreaktimeDisplaying()
        {
            Console.Clear();

            string text =

                "\r\n\r\n" + 
                "회복하기\r\n" +
                "500 G 를 내고 체력을 모두 회복합니다. " +
                $"(보유 골드 : {player.gold} G)\r\n\r\n" +
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
                    if(player.gold >=500)
                    {
                        player.HP = player.maxHP;
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
                Location.SetLocation(STATE.마을);
            }
            Location.SetLocation(STATE.마을);

            MyLog.절취선();


        }

        public void FreeBreaktimeDisplay()
        {
            Console.Clear();

            string text =

                "         wWWWw               wWWWw\r\n   vVVVv (___) wWWWw         (___)  vVVVv\r\n   (___)  ~Y~  (___)  vVVVv   ~Y~   (___)\r\n    ~Y~   \\|    ~Y~   (___)    |/    ~Y~\r\n    \\|   \\ |/   \\| /  \\~Y~/   \\|    \\ |/\r\n   \\\\|// \\\\|// \\\\|/// \\\\|//  \\\\|// \\\\\\|///\r\njgs^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^" +

                "\r\n\r\n" +
                "플레이어의 HP와 MP를 랜덤으로 회복합니다.\r\n " +

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

                    player.HP += random.Next(0, 41);
                    player.MP += random.Next(0, 41);

                    if (player.HP > player.maxHP)
                    {
                        player.HP = player.maxHP;
                    }

                    if (player.MP > player.maxMP)
                    {
                        player.MP = player.maxMP;
                    }

                    Console.WriteLine("따스한 햇살을 쬐며 조금 회복했습니다.");
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

