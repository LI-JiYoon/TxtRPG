using rtanRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    internal class dungeon
    {
        bool Isclear = false;

        Random rand = new Random();

        private Player player;

        int numberOfDraws;
        int minValue = 1; // 몬스터 뽑기 최소값
        int maxValue = 4; // 몬스터 뽑기 최대값


        Monster[] Monsters = new Monster[]
       {
           new Monster1(),
           new Monster2(),
           new Monster3()
       };

        List <Monster> MonstersQueue = new List <Monster>();
        int queueIndex = 0;

        public dungeon(Player player)
        {
            this.player = player;

        }
        public void Displaying()
        {
            string text =

                "                                  |>>>\r\n                                  |\r\n                    |>>>      _  _|_  _         |>>>\r\n                    |        |;| |;| |;|        |\r\n                _  _|_  _    \\\\.    .  /    _  _|_  _\r\n               |;|_|;|_|;|    \\\\:. ,  /    |;|_|;|_|;|\r\n               \\\\..      /    ||;   . |    \\\\.    .  /\r\n                \\\\.  ,  /     ||:  .  |     \\\\:  .  /\r\n                 ||:   |_   _ ||_ . _ | _   _||:   |\r\n                 ||:  .|||_|;|_|;|_|;|_|;|_|;||:.  |\r\n                 ||:   ||.    .     .      . ||:  .|\r\n                 ||: . || .     . .   .  ,   ||:   |       \\,/\r\n                 ||:   ||:  ,  _______   .   ||: , |            /`\\\r\n                 ||:   || .   /+++++++\\    . ||:   |\r\n                 ||:   ||.    |+++++++| .    ||: . |\r\n              __ ||: . ||: ,  |+++++++|.  . _||_   |\r\n     ____--`~    '--~~__|.    |+++++__|----~    ~`---,              ___\r\n-~--~                   ~---__|,--~'                  ~~----_____-~'   `~----~~"

                + "\r\n\r\n" +
                "1. 쉬운 던전     | 방어력 5 이상 권장\r\n" +
                "2. 일반 던전     | 방어력 11 이상 권장\r\n" +
                "3. 어려운 던전    | 방어력 17 이상 권장\r\n" +
                "0. 나가기\r\n\r\n" +
                "" +
                "원하시는 행동을 입력해주세요." +
                ">>";

            Console.WriteLine(text);

            // 0이 아니면 무한히 질문!
            while (true)
            {
                string inputText = Console.ReadLine();
                if (!int.TryParse(inputText, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                if (inputText == "0")
                {
                    Location.SetLocation(STATE.마을);
                    break;
                }
                else if (inputText == "1")
                {
                    EnterDungeon(dungeon.Difficulty.Easy);
                    break;
                }
                else if (inputText == "2")
                {

                    EnterDungeon(dungeon.Difficulty.Normal);
                    break;
                }
                else if (inputText == "3")
                {

                    EnterDungeon(dungeon.Difficulty.Hard);
                    break;
                }
                else
                {
                    { Console.WriteLine("잘못된 입력입니다."); }
                }
            }
            MyLog.절취선();
        }
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }


        public void EnterDungeon(Difficulty difficulty)//던전입장
        {
            if(MonstersQueue != null)
            {
                MonstersQueue.Clear();                                  //MonsterQueue 초기화
            }
            numberOfDraws = rand.Next(minValue, maxValue + 1); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)
            MonstersQueue.Capacity = numberOfDraws;

            for (int i = 0; i < numberOfDraws; i++)
            {
                int monsterIndex = rand.Next(Monsters.Length);
                // MonstersQueue 배열의 다음 빈 공간에 Monsters 배열의 요소를 추가           monsterQueue에 랜덤한 몬스터 추가
                Monster newMonster;

                // 랜덤한 몬스터 생성
                switch (monsterIndex)
                {
                    case 0:
                        newMonster = new Monster1();
                        break;
                    case 1:
                        newMonster = new Monster2();
                        break;
                    case 2:
                        newMonster = new Monster3();
                        break;
                    default:
                        newMonster = new Monster1();
                        break;
                }

                MonstersQueue.Add(newMonster); // MonstersQueue에 새로운 몬스터 추가
            }

            //전투구현

            while(!MonstersQueue.All(x => x.isDead) && !player.isDead)
            {
                DisplayBattleUI(MonstersQueue, player);

                if(MonstersQueue.All(x => x.isDead))
                {
                    DisplayBattleResult(MonstersQueue, player);
                }

                DisplayEnemyAttack();

                if (player.isDead)
                {
                    DisplayBattleResult(MonstersQueue, player);
                }

            }

            /*
            int baseReward = GetBaseReward(difficulty);

            // 보상 계산
            int reward = CalculateReward(player.ATK, baseReward);
            player.gold += reward;
            Console.WriteLine($"Gold {player.gold - reward} G -> {player.gold} G");
            player.dungeonClearCount++;
            player.checkLevelUp();
            */
        }

        private int GetRecommendedDEF(Difficulty difficulty)//난이도 별 권장 방어력
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 5;
                case Difficulty.Normal:
                    return 10;
                case Difficulty.Hard:
                    return 15;
                default:
                    return 0;
            }
        }

        private int GetBaseReward(Difficulty difficulty)//난이도별 기본 보상
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 1000;
                case Difficulty.Normal:
                    return 1700;
                case Difficulty.Hard:
                    return 2500;
                default:
                    return 0;
            }
        }


        // 보상 계산
        private int CalculateReward(float playerATK, int baseReward)//
        {
            float bonusMultiplier = (float)(rand.NextDouble() + 1); // 1.0 ~ 2.0 범위
            int bonusReward = (int)(baseReward * (playerATK / 100) * bonusMultiplier);
            return baseReward + bonusReward;
        }


        public void DisplayBattleUI(List<Monster> MonsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < MonsterQueue.Count; i++)

            {
                if (MonsterQueue[i] != null)
                {
                    if (MonsterQueue[i].isDead)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;                        //죽은 몬스터들 회색으로
                    }
                    Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {MonsterQueue[i].HP}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]]\r\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP 100/{player.HP}");

            Console.WriteLine($"\n1. 공격\n\n원하시는 행동을 입력해주세요.");

            Console.Write(">>");

            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }


                if (input == "1")
                {
                    DisplaySelectEnemy(MonsterQueue, player);
                    break;
                }
                else { Console.WriteLine("잘못된 입력입니다."); }

            }
        }


        public void DisplaySelectEnemy(List<Monster> MonsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < MonsterQueue.Count; i++)
            {
                if (MonsterQueue[i] != null)
                {
                    string monsterHp = MonsterQueue[i].isDead ? "Dead" : MonsterQueue[i].HP.ToString();

                    if (MonsterQueue[i].isDead) // 회색으로 
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine($"{i + 1} Lv. {MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {monsterHp}");
                    Console.ResetColor();
                }
                else break;                                                                                         //null이 아니면 for문탈출
            }
            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP 100/{player.HP}");
            Console.WriteLine($"\n0. 취소\n\n대상을 선택해주세요.");
            Console.Write(">>");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int inputIDX))
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            else if (inputIDX > MonstersQueue.Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            else if (input == "0")
            {
                DisplayBattleUI(MonstersQueue, player);
            }
            else if (MonstersQueue[inputIDX - 1].isDead)
            {
                Console.WriteLine("이미 처치한 몬스터입니다.");
                Thread.Sleep(1000);
                Console.Clear();
            }

            MonstersQueue[inputIDX - 1].TakeDamage((int)player.Attack(MonstersQueue[inputIDX - 1].Name, MonstersQueue[inputIDX - 1].level));//공격  

            Console.WriteLine($"\n0. 다음\n\n");
            Console.Write(">>");
        }


        public void DisplayBattleResult(List<Monster> monsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            if (player.isDead == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vicotry!");
                Console.WriteLine();
                Console.ResetColor();
            }
            else if (player.isDead == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose!!");
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.WriteLine();
            if (player.isDead == false)
            {
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsterQueue.Count);           //이게 몇마리가 아니라 무조건 4가 나오지
                Console.WriteLine();
            }
            Console.WriteLine($"Lv.{player.level} {player.name}\nHP 100 -> {player.HP}");           //데미지 계산에서 HP가 음수가되면 0이되게하던가 여기서 분기를 나눠도 OK
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">> ");

            //전투 보상(경험치, 골드, 아이템)

            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (input == "0")
                {
                    Displaying();       //던전선택창
                    break;
                }
                else { Console.WriteLine("잘못된 입력입니다."); }
            }

        }


        public void DisplayEnemyAttack()                //죽은사람은 좀비가 되지 말아주세요
        {
            for (int i = 0; i < MonstersQueue.Count; i++)
            {
                if (MonstersQueue[i] != null)
                {
                    Console.Clear();
                    player.TakeDamage(MonstersQueue[i].Attack(player.name));
                    Console.WriteLine($"\n0. 다음\n\n");
                    Console.Write(">>");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int inputIDX))
                    { Console.WriteLine("잘못된 입력입니다."); continue; }

                    if (input == "0")
                    {
                        continue;
                    }
                    else { Console.WriteLine("잘못된 입력입니다."); }
                }
                else break;

            }
        }
    }
}


