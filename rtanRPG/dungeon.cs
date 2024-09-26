using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    internal class dungeon
    {   bool Isclear = false;
     
        Random rand = new Random();

        private Player player;
      



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
            int recommendedDEF = GetRecommendedDEF(difficulty);
            int baseReward = GetBaseReward(difficulty);

            if (player.DEF < recommendedDEF)
            {
         
                // 40% 확률로 던전 실패
                if (rand.Next(100) < 40)
                {
                    Console.WriteLine(
                        "__  __                __                       \r\n\\ \\/ /___  __  __    / /   ____  ________      \r\n \\  / __ \\/ / / /   / /   / __ \\/ ___/ _ \\     \r\n / / /_/ / /_/ /   / /___/ /_/ (__  )  __/ _ _ \r\n/_/\\____/\\__,_/   /_____/\\____/____/\\___(_|_|_)"

                        + "\r\n" + 
                        "던전 도전에 실패하셨습니다.");
                    player.HP /= 2; // 체력 절반 감소
                    Console.WriteLine($"현재 체력: {player.HP}");
                    return;
                }
            }

            // 던전 클리어
            Console.WriteLine(" _____                                _____ \r\n( ___ )------------------------------( ___ )\r\n |   |                                |   | \r\n |   |                                |   | \r\n |   |                                |   | \r\n |   |  ,--.   ,--.                   |   | \r\n |   |   \\  `.'  /,---. ,--.,--.      |   | \r\n |   |    '.    /| .-. ||  ||  |      |   | \r\n |   |      |  | ' '-' ''  ''  '      |   | \r\n |   |      `--'  `---'  `----',---.  |   | \r\n |   |  ,--.   ,--.,--.        |   |  |   | \r\n |   |  |  |   |  |`--',--,--, |  .'  |   | \r\n |   |  |  |.'.|  |,--.|      \\|  |   |   | \r\n |   |  |   ,'.   ||  ||  ||  |`--'   |   | \r\n |   |  '--'   '--'`--'`--''--'.--.   |   | \r\n |   |                         '--'   |   | \r\n |   |                                |   | \r\n |   |                                |   | \r\n |___|                                |___| \r\n(_____)------------------------------(_____)"


                + "\r\n\r\n" 

                + "축하합니다!!\r\n"

                + "던전을 클리어 하였습니다.\r\n\r\n"

                + "[탐험 결과]"
                );
            int hpReduction = CalculateHPReduction(player.DEF, recommendedDEF);
            player.HP -= hpReduction;
            

            if (player.HP <= 0)
            {
                player.HP = 0;
                Console.WriteLine("던전 클리어는 했지만 체력이 0이 되었습니다.");
                return;
            }

            Console.WriteLine($"체력 {player.HP + hpReduction} -> {player.HP}");

            // 보상 계산
            int reward = CalculateReward(player.ATK, baseReward);
            player.gold += reward;
            Console.WriteLine($"Gold {player.gold-reward} G -> {player.gold} G");
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
        // 체력 감소량 계산
        private int CalculateHPReduction(float playerDEF, int recommendedDEF)
        {
            int baseMin = 20;
            int baseMax = 35;
            int difference = (int)(recommendedDEF - playerDEF);

            // 체력 감소량 계산
            int minReduction = baseMin + difference;
            int maxReduction = baseMax + difference;

            // 최소 체력 감소량은 1로 고정
            minReduction = Math.Max(minReduction, 1);
            maxReduction = Math.Max(maxReduction, minReduction);

            return rand.Next(minReduction, maxReduction + 1);
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
