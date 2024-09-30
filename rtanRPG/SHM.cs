//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static rtanRPG.dungeon;

//namespace rtanRPG
//{
//    internal class SHM
//    {

//        int numberOfDraws;
//        int minValue = 1; // 몬스터 뽑기 최소값
//        int maxValue = 4; // 몬스터 뽑기 최대값

//        Monster[] Monsters = new Monster[]
//       {

//           new Monster1(),
//           new Monster2(),
//           new Monster3()
//       };

//        Monster[] MonstersQueue = new Monster[4];
//        int queueIndex = 0;

//        public interface Item
//        {
//            public string name { get; set; }
//            public string description { get; set; }
//            public float ability { get; }
//            public float price { get; set; }
//            public bool alreadyHave { get; set; }
//            public string type { get; }


//            public string isSoldout { get; set; }

//            public string Label();

//        }

//        //포션 클래스
//        //클래스를 상속받는 체력포션.
//        //Item.cs에 있어야하는









//            public string Label()
//            {
//                return $" {name,-15}| {potionEffect[potionType]} +{(int)ability,-15}| {description,-15}|";     //왜 -15? 정렬을 위해서
//            }

//            public virtual void UsePotion(Player player) { }

//        }


//        // Dungeon.cs의 112번째 줄에 적용하면 될 것 같습니다.
//        //플레이어의 MaxHP와 현재HP를 구분하면 좋을것 같습니다.
//        //레벨업이나 아이템을 통해 MaxHP를 늘릴 수 있게
//        public void ShowBattleUI(Monster[] monsterQueue, Player player)
//        {
//            Console.Clear();
//            Console.WriteLine("Battle!!");
//            Console.WriteLine();
//            for (int i = 0; i < monsterQueue.Length; i++)
//            {
//                Console.WriteLine($"Lv.{monsterQueue[i].level} {monsterQueue[i].Name}  HP {monsterQueue[i].HP}");
//            }
//            Console.WriteLine("\n");
//            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
//            Console.WriteLine($"HP 100/{player.HP}");
//            Console.WriteLine();
//            Console.WriteLine($"1. 공격\n\n원하시는 행동을 입력해주세요.");
//            Console.Write(">>");

//            while (true)
//            {
//                string input = Console.ReadLine();
//                if (!int.TryParse(input, out int inputIDX))
//                { Console.WriteLine("잘못된 입력입니다."); continue; }
//                inputIDX -= 1;

//                if (input == "1")
//                {
//                    //Attack함수
//                    break;
//                }
//                else { Console.WriteLine("잘못된 입력입니다."); }
//            }
//        }

//        //일단 체력 100 -> 현재체력으로 했지만 2번째부턴 전투 시작전에 전투전HP에 해당하는 변수를 지정해야..?
//        public void BattleEnd(Monster[] monsterQueue, Player player)
//        {
//            Console.Clear();
//            Console.WriteLine("Battle!! - Result");
//            Console.WriteLine();
//            if (player.isDead == false)
//            {
//                Console.ForegroundColor = ConsoleColor.Green;
//                Console.WriteLine("Vicotry!");
//                Console.WriteLine();
//                Console.ResetColor();
//            }
//            else if (player.isDead == true)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine("You Lose!!");
//                Console.WriteLine();
//                Console.ResetColor();
//            }
//            Console.WriteLine();
//            if (player.isDead == false)
//            {
//                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsterQueue.Length);
//                Console.WriteLine();
//            }
//            Console.WriteLine($"Lv.{player.level} {player.name}\nHP 100 -> {player.HP}");           //데미지 계산에서 HP가 음수가되면 0이되게하던가 여기서 분기를 나눠도 OK
//            Console.WriteLine();
//            Console.WriteLine("0. 다음");
//            Console.WriteLine();
//            Console.Write(">> ");

//            //전투 보상(경험치, 골드, 아이템)

//            while (true)
//            {
//                string input = Console.ReadLine();
//                if (!int.TryParse(input, out int inputIDX))
//                { Console.WriteLine("잘못된 입력입니다."); continue; }
//                inputIDX -= 1;

//                if (input == "0")
//                {
//                    Displaying();       //던전선택창
//                    break;
//                }
//                else { Console.WriteLine("잘못된 입력입니다."); }
//            }

//            전투대기열의 모든 몬스터의 isDead가 true가 될 때, 이 함수를 호출합니다.
//            if ((monsterQueue.All(x => monsterQueue[x].isDead == true) || player.isDead == true)
//            {
//                BattleEnd(monsterQueue, player)
//            }
//        }
//    }




//    //회피 = 숫자 랜덤으로 뽑아서 DEF수치보다 낮은숫자면 공격실패

//}
