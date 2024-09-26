using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public class Player
    {
        public int level = 1;
        public string name = "";
        public string role = "전사";
        public float ATK = 10;
        public float DEF = 5;
        public float HP = 100;
        public float gold = 1500;
        public float dungeonClearCount = 0;

        public float EXP = 0;  // ? 나중에 던전도 만들면 레벨업 시스템 구현하려구 웅!

        public bool weapon = false;
        public bool armor = false;

        public Inventory inventory;


        public Player()
        {
            inventory = new Inventory(this);
        }


        /// <summary>
        /// 상태보기
        /// </summary>
        public void DisplayStatus()
        {
            string text =
                $"Lv. {level}      \r\n" +
                $"{name} ( {role} )\r\n" +
                $"공격력 : {ATK}\r\n" +
                $"방어력 : {DEF}\r\n" +
                $"체 력 : {HP}\r\n" +
                $"Gold : {gold} G\r\n\r\n" +

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
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            }
        public void checkLevelUp()
        {
            // 레벨업에 필요한 던전 클리어 횟수
            int requiredClearCount = level;

            // 던전 클리어 횟수가 현재 레벨에서 필요한 횟수를 초과할 경우 레벨업
            if (dungeonClearCount >= requiredClearCount)
            {
                levelUp();
            }
        }
        public void levelUp()
        {
            
                level += 1;
                ATK += 0.5f;
                DEF += 1;
                dungeonClearCount = 0; // 클리어 횟수 초기화



        }

    }
}
