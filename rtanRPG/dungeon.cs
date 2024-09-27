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

        Monster[] MonstersQueue = new Monster[4];
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






            int baseReward = GetBaseReward(difficulty);

            // 보상 계산
            int reward = CalculateReward(player.ATK, baseReward);
            player.gold += reward;
            Console.WriteLine($"Gold {player.gold - reward} G -> {player.gold} G");
            player.dungeonClearCount++;
            player.checkLevelUp();
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


        public void ShowBattleUI(Monster[] MonsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < MonsterQueue.Length; i++)

            {
                if (MonsterQueue[i] != null)
                {
                    Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {MonsterQueue[i].HP}");

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
                    ShowAttackUI(MonsterQueue, player);
                    //Attack함수
                    break;
                }
                else { Console.WriteLine("잘못된 입력입니다."); }

            }
        }


        public void ShowAttackUI(Monster[] MonsterQueue, Player player)
        {
            bool IsYourTurn = true;


            while (true)
            {
                if (IsYourTurn)
                {
                    Console.Clear();
                    Console.WriteLine("Battle!!");
                    Console.WriteLine();
                    for (int i = 0; i < MonsterQueue.Length; i++)
                    {
                        if (MonsterQueue[i] != null)
                        {

                            bool isDead = MonsterQueue[i].HP <= 0;
                            string monsterHp = isDead ? "Dead" : MonsterQueue[i].HP.ToString();

                            if (isDead) // 회색으로 
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }

                            Console.WriteLine($"{i + 1} Lv. {MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {monsterHp}");
                        }

                    }
                    Console.WriteLine("\n");
                    Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
                    Console.WriteLine($"HP 100/{player.HP}");
                    Console.WriteLine($"\n0. 취소\n\n대상을 선택해주세요.");
                    Console.Write(">>");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int inputIDX)) { Console.WriteLine("잘못된 입력입니다."); continue; }
                    if (inputIDX > MonstersQueue.Length) { Console.WriteLine("잘못된 입력입니다."); continue; }


                    if (input == "0")

                    {
                        // ShowBattleUI();
                        break;
                    }
                    if (MonstersQueue[inputIDX - 1].isdead) { Console.WriteLine("이미 처치한 몬스터입니다."); continue; }
                    Console.Clear();

                    MonstersQueue[inputIDX - 1].TakeDamage((int)player.Attack(MonstersQueue[inputIDX - 1].Name, MonstersQueue[inputIDX - 1].level));//공격

                    Console.WriteLine($"\n0. 다음\n\n");
                    Console.Write(">>");

                    while (true)
                    {

                        input = Console.ReadLine();

                        if (!int.TryParse(input, out inputIDX))
                        { Console.WriteLine("잘못된 입력입니다."); continue; }

                        if (input == "0")
                        {
                            break;
                        }

                        else { Console.WriteLine("잘못된 입력입니다."); }

                    }
                    IsYourTurn = false; // 플레이어 턴 끝
                    if ((MonstersQueue.All(x => x.isdead == true) || player.isDead == true))
                    {
                        BattleEnd(MonstersQueue, player);
                    }

                    continue;

                    //if(win?)

                }
                else
                {
                    for (int i = 0; i <= MonstersQueue.Length; i++)
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
                    IsYourTurn = true; // 몬스터 턴 끝
                    if ((MonstersQueue.All(x => x.isdead == true) || player.isDead == true))
                    {
                        BattleEnd(MonstersQueue, player);
                    }
                    //if(lose?)
                }


                //if (player.isDead)
                //{
                //    Console.WriteLine("플레이어가 사망했습니다. 게임을 종료합니다.");
                //    Environment.Exit(0); // 게임 종료
                //}


            };


        }




        public void BattleEnd(Monster[] monsterQueue, Player player)
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
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsterQueue.Length);
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

    }


}
