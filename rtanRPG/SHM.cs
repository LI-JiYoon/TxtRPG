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





        //플레이어의 MaxHP와 현재HP를 구분하면 좋을것 같습니다.
        //레벨업이나 아이템을 통해 MaxHP를 늘릴 수 있게
        public void ShowBattleUI(Monster[] MonsterQueue, Player player)
        {
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for(int i =0; i < MonsterQueue.Length; i++)
            {
                Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {MonsterQueue[i].HP}" );
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
    }
    /*Battle!!

Lv.2 미니언  HP 15
Lv.5 대포미니언 HP 25
LV.3 공허충 HP 10


[내정보]
Lv.1  Chad (전사) 
HP 100/100 

1. 공격

원하시는 행동을 입력해주세요.
>>
     * 1입력시 공격할 몬스터의 번호가 표시
     * 숫자 입력시 선택한 몬스터를 대상으로 공격메서드
     * 
     * 
     */



}
