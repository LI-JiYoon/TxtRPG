using rtanRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace rtanRPG
{
    internal class dungeon
    {
        // init
        Random rand = new Random();
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        // State
        bool Isclear = false;


        // Field        
        private Player player;
        /// <summary>
        /// 전투 준비중인 몬스터
        /// </summary>
        List<Monster> MonstersQueue = new List<Monster>();
       


        //생성자
        public dungeon(Player player)
        {
            this.player = player;

        }



        //메서드
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
        

        /// <summary>
        /// 던전입장
        /// </summary>
        /// <param name="difficulty"></param>
        public void EnterDungeon(Difficulty difficulty)
        {
            // MonsterQueue 초기화
            MonstersQueue = new List<Monster>();

            // 몬스터 생성
            int numberOfDraws = rand.Next(1, 5); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)
            for (int i = 0; i < numberOfDraws; i++)
            {
                int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
                Monster tempMonster = MonsterPreset.baseMonster[monsterIndex].Clone();
                MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
            }

            //전투구현
            if(player.isDead) Console.WriteLine("체력을 회복하고 오세요");
            else PlayBattle();
            

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

        public void PlayBattle()
        {
            // 플레이어 혹은 몬스터집단 중 한 쪽이 전멸할때까지 반복
            while (!MonstersQueue.All(x => x.isDead) && !player.isDead)
            {
                // 전투 UI 띄우기
                DisplayBattleUI(MonstersQueue, player);

                // 몬스터 전멸일 경우
                if (MonstersQueue.All(x => x.isDead))
                {
                    DisplayBattleResult(MonstersQueue, player);
                }

                // 연속된 몬스터 공격 Ui 띄우기
                DisplayEnemyAttack();

                // 플레이어 사망일 경우
                if (player.isDead)
                {
                    DisplayBattleResult(MonstersQueue, player);
                }
            }
        }


        
        public void DisplayBattleUI(List<Monster> MonsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            // 몬스터 정보 출력
            for (int i = 0; i < MonsterQueue.Count; i++)
            {
                if (MonsterQueue[i] != null)
                {
                    if (MonsterQueue[i].isDead)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;                        //죽은 몬스터들 회색으로
                    }
                    Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].name}  HP {MonsterQueue[i].hP}");
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
                    string monsterHp = MonsterQueue[i].isDead ? "Dead" : MonsterQueue[i].hP.ToString();

                    if (MonsterQueue[i].isDead) // 회색으로 
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine($"{i + 1} Lv. {MonsterQueue[i].level} {MonsterQueue[i].name}  HP {monsterHp}");
                    Console.ResetColor();
                }
                else break;                                                                                         //null이 아니면 for문탈출
            }


            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP 100/{player.HP}");
            Console.WriteLine($"\n0. 취소\n\n대상을 선택해주세요.");
            Console.Write(">>");

            int inputIDX;
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out inputIDX))
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
                   
                }
                else break;
            }
;
            //공격  
            Console.Clear();
            MonstersQueue[inputIDX - 1].TakeDamage((int)player.Attack(MonstersQueue[inputIDX - 1].name, MonstersQueue[inputIDX - 1].level));
            Console.ReadKey();
        }
        public void DisplayBattleResult(List<Monster> monsterQueue, Player player)
        {
        
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
                    if (MonstersQueue[i].isDead)
                    {
                        // 몬스터가 죽었기 때문에 for문을 넘긴다.
                        continue;
                    }
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
    }
}


