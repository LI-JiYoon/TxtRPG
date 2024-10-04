using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{

    public class MiniGame
    {
        private Player player;
        private int playerWins = 0;

        public int GetGold()
        {
            return playerWins * 50;
        }

        public MiniGame(Player player)
        {
            this.player = player;
        }



        public void Displaying()
        {
            Console.Clear();

            string text =

                "       _\r\n     _|=|__________\r\n    /              \\\r\n   /                \\\r\n  /__________________\\\r\n   ||  || /--\\ ||  ||\r\n   ||[]|| | .| ||[]||\r\n ()||__||_|__|_||__||()\r\n( )|-|-|-|====|-|-|-|( ) \r\n^^^^^^^^^^====^^^^^^^^^^^"

                + "\r\n\r\n" +
                "미니게임\r\n" +
                "미니게임으로 골드를 획득하세요! " +
                $"(보유 골드 : {player.gold} G)\r\n\r\n" +
                "1. 가위바위보\r\n" +
                "0. 나가기\r\n\r\n" +
                "원하시는 행동을 입력해주세요.\r\n" +
                ">>";

            Console.WriteLine(text);

            while (true)
            {
                string inputText = Console.ReadLine();

                if (!int.TryParse(inputText, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (inputText == "0")
                {
                    Location.SetLocation(STATE.마을);
                    break;
                }
                else if (inputText == "1")
                {
                    RockPaperSissors();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public void RockPaperSissors()
        {

            Console.Clear();

            string[] choices = { "가위", "바위", "보" };
            int attempts = 3;
            string playerChoice = "";
            string computerChoice;

            for (int i = 0; i < attempts; i++)
            {
                computerChoice = choices[new Random().Next(0, 3)];
                Console.Write("가위, 바위, 보 중 하나를 선택하세요: ");
                playerChoice = Console.ReadLine();

                if (playerChoice != "가위" && playerChoice != "바위" && playerChoice != "보")
                {
                    Console.WriteLine("잘못된 입력입니다!");
                    i--;
                    continue;
                }

                Console.WriteLine("NPC: " + computerChoice);


                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("비겼습니다!");
                }
                else if ((playerChoice == "가위" && computerChoice == "보") ||
                         (playerChoice == "바위" && computerChoice == "가위") ||
                         (playerChoice == "보" && computerChoice == "바위"))
                {
                    Console.WriteLine($"{player.name} 승리!");
                    playerWins++;
                }
                else
                {
                    Console.WriteLine("NPC 승리!");
                }
            }

            Console.Clear();

            string text =

            $"게임 종료! {playerWins}번 이겼습니다.\r\n" +
            $"총 {GetGold()} G를 획득했습니다.\r\n\r\n" +

            $"(보유 골드 : {player.gold} G >> {player.gold += GetGold()} G)\r\n\r\n";

            Console.WriteLine(text);
            Console.ReadKey();

        }
    }
}
