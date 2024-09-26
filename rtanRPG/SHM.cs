using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static rtanRPG.dungeon;

namespace rtanRPG
{
    internal class SHM
    {

        int numberOfDraws;
        int minValue = 1; // 몬스터 뽑기 최소값
        int maxValue = 4; // 몬스터 뽑기 최대값

        Monster[] Monsters = new Monster[]
       {

           new Monster1(),
           new Monster2(),
           new Monster3()
       };

        Monster[] MonstersQueue = new Monster[4];
        int queueIndex = 0;

        public void EnterDungeon(Difficulty difficulty)//던전입장
        {
            numberOfDraws = rand.Next(minValue, maxValue); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)


            for (int i = 0; i < numberOfDraws; i++)
            {
                // MonstersQueue 배열의 다음 빈 공간에 Monsters 배열의 요소를 추가
                if (queueIndex < MonstersQueue.Length)
                {
                    MonstersQueue[queueIndex] = Monsters[i];
                    queueIndex++; // MonstersQueue의 인덱스 증가
                }
            }

            ShowBattleUI(MonstersQueue, player);


            for (int i = 0; i < queueIndex; i++)
            {
                MonstersQueue[i].Attack();

            }

        }




        // Dungeon.cs의 112번째 줄에 적용하면 될 것 같습니다.
        //플레이어의 MaxHP와 현재HP를 구분하면 좋을것 같습니다.
        //레벨업이나 아이템을 통해 MaxHP를 늘릴 수 있게
        public void ShowBattleUI(Monster[] monsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monsterQueue.Length; i++)
            {
                Console.WriteLine($"Lv.{monsterQueue[i].level} {monsterQueue[i].Name}  HP {monsterQueue[i].HP}");
            }
            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP 100/{player.HP}");
            Console.WriteLine($"\n1. 공격\n\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (input == "1")
                {
                    //Attack함수
                    break;
                }
                else { Console.WriteLine("잘못된 입력입니다."); }

            }
        }

        //일단 체력 100 -> 현재체력으로 했지만 2번째부턴 전투 시작전에 전투전HP에 해당하는 변수를 지정해야..?
        public void BattleEnd(Monster[] monsterQueue, Player player)
        {
            Console.Clear();

            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine(player.isDead ? "You Lose": "Victory");
            Console.WriteLine();
            if(player.isDead == false)
            {
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsterQueue.Length);
                Console.WriteLine();
            }
            Console.WriteLine($"Lv.{player.level} {player.name}\nHP 100 -> {player.HP}");           //데미지 계산에서 HP가 음수가되면 0이되게하던가 여기서 분기를 나눠도 OK
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">> ");

            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (input == "0")
                {
                    Displaying();
                    break;
                }
                else { Console.WriteLine("잘못된 입력입니다."); }

            }


        }

        //전투대기열의 모든 몬스터의 isDead가 true가 될 때, 이 함수를 호출합니다.
        //if((monsterQueue.All(x => monsterQueue[x].isDead == true) || player.isDead == true)
        //{
        //      BattleEnd(monsterQueue, player)
        //}
    }

}
