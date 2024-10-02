using rtanRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static rtanRPG.dungeon;
namespace rtanRPG
{
    public delegate void MonsterDeadHandler(Monster monster);
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

        static public int dungeongClearCount = 0;

        // State
        bool Isclear = false;
        bool isFleeing;
        bool ClearDiff = false;

        // Field        
        private Player player;
        /// <summary>
        /// 전투 준비중인 몬스터
        /// </summary>
        List<Monster> MonstersQueue = new List<Monster>();

        //스킬 리스트
        List<Skill> SkillList = new List<Skill>();


        //보상아이템
        Item[] items;
        private Dictionary<Item, int> inventory;

        //생성자
        public dungeon(Player player)
        {
            this.player = player;
            this.inventory = player.inventory.inventory;
            this.items = player.inventory.InitalInventoryArray(player.inventory.inventory);
            

        }



        //메서드
        public void Displaying()
        {
            Console.Clear();
            string text =

                "                                  |>>>\r\n                                  |\r\n                    |>>>      _  _|_  _         |>>>\r\n                    |        |;| |;| |;|        |\r\n                _  _|_  _    \\\\.    .  /    _  _|_  _\r\n               |;|_|;|_|;|    \\\\:. ,  /    |;|_|;|_|;|\r\n               \\\\..      /    ||;   . |    \\\\.    .  /\r\n                \\\\.  ,  /     ||:  .  |     \\\\:  .  /\r\n                 ||:   |_   _ ||_ . _ | _   _||:   |\r\n                 ||:  .|||_|;|_|;|_|;|_|;|_|;||:.  |\r\n                 ||:   ||.    .     .      . ||:  .|\r\n                 ||: . || .     . .   .  ,   ||:   |       \\,/\r\n                 ||:   ||:  ,  _______   .   ||: , |            /`\\\r\n                 ||:   || .   /+++++++\\    . ||:   |\r\n                 ||:   ||.    |+++++++| .    ||: . |\r\n              __ ||: . ||: ,  |+++++++|.  . _||_   |\r\n     ____--`~    '--~~__|.    |+++++__|----~    ~`---,              ___\r\n-~--~                   ~---__|,--~'                  ~~----_____-~'   `~----~~"

                + "\r\n\r\n" +

                $"1. 던전 입장    | (현재 진행 : {dungeongClearCount + 1}층)\r\n" +
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

                    EnterDungeon(DungeonDifficulty(dungeongClearCount + 1));
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
        public void EnterDungeon(string difficulty)
        {
            //도망치기 변수 초기화
            isFleeing = false;

            // MonsterQueue 초기화
            MonstersQueue = new List<Monster>();


            //스킬 생성
            SkillList = addSkills(player.role, ref SkillList);


            // 몬스터 생성
            MonstersQueue = makeMonster(ref MonstersQueue, difficulty);

            //int numberOfDraws = rand.Next(1, 5); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)
            //for (int i = 0; i < numberOfDraws; i++)
            //{
            //    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
            //    Monster tempMonster = MonsterPreset.baseMonster[monsterIndex].Clone();
            //    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
            //}

            //전투구현
            if (player.isDead) Console.WriteLine("체력을 회복하고 오세요");
            else PlayBattle();



            //int baseReward = GetBaseReward(difficulty);

            //// 보상 계산
            //int reward = CalculateReward(player.ATK, baseReward);
            //player.gold += reward;
            //Console.WriteLine($"Gold {player.gold - reward} G -> {player.gold} G");


        }

        public void PlayBattle()
        {
            // 플레이어 혹은 몬스터집단 중 한 쪽이 전멸할때까지 반복
            while (!MonstersQueue.All(x => x.isDead) && !player.isDead)
            {
                // 전투 UI 띄우기
                DisplayBattleUI(MonstersQueue, player);

                //도망치기 선택 경우 던전입장 UI로 return
                if (isFleeing)
                {
                    Console.Clear();
                    Console.WriteLine("당신은 쫄아서 튀었다..!");
                    Console.ReadKey();
                    return;
                }

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

            Console.WriteLine($"\n1. 공략\n2. 매력발산\n3. 포션 사용\n0. 도망치기\n\n원하시는 행동을 입력해주세요.");

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
                if (input == "2")
                {
                    //스킬사용
                    UsingSkill(MonsterQueue, player);
                    break;
                }
                if (input == "3")
                {
                    //포션사용
                    UsePotionDuringBattle(player);
                    continue;
                }
                if (input == "0")
                {
                    //도망치기 선택시
                    isFleeing = true;
                    break;
                    continue;

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
                    string monsterHp = MonsterQueue[i].isDead ? "폴인럽" : MonsterQueue[i].hP.ToString();

                    if (MonsterQueue[i].isDead) // 회색으로 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    Console.WriteLine($"{i + 1} Lv. {MonsterQueue[i].level} {MonsterQueue[i].name}  HP {monsterHp}");
                    Console.ResetColor();
                }
                else break;                                                                                         //null이 아니면 for문탈출
            }


            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP {player.HP}/ {player.maxHP}");
            Console.WriteLine($"\n0. 취소\n\n공략 대상을 선택해주세요!");
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
                    Console.WriteLine("이미 꼬신 대상입니다!");
                    Thread.Sleep(1000);

                }
                else break;
            }
;
            //공격  
            Console.Clear();
            MonstersQueue[inputIDX - 1].TakeDamage((int)player.Attack(MonstersQueue[inputIDX - 1].name, MonstersQueue[inputIDX - 1].level));
            //경험치
            if (MonstersQueue[inputIDX - 1].isDead) player.EXP += MonstersQueue[inputIDX - 1].level * 1;

            Console.ReadKey();
        }
        public event MonsterDeadHandler onDead;
        public void DisplayBattleResult(List<Monster> monsterQueue, Player player)
        {
            foreach (Monster monster in monsterQueue)
            {
                if (monster.isDead)
                {
                    onDead?.Invoke(monster);
                }
            }
            int baseReward = 0;
            int extraReward = 0;
            List<Item> rewardItems = new List<Item>();//부상 아이템을 받아줄 리스트
            int numberOfItems = rand.Next(1, 3); //보상 아이템개수 (1개 또는 2개)

            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            if (player.isDead == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vicotry!");
                Console.WriteLine();
                Console.ResetColor();

                baseReward = GetBaseReward(DungeonDifficulty(dungeongClearCount));
                extraReward = CalculateReward(player.ATK, baseReward);
                // 아이템 보상 난이도에 따른 분배

                GetItemReward(DungeonDifficulty(dungeongClearCount), ref rewardItems, numberOfItems);
                Console.WriteLine("던전에서 매니저님 {0}명을 매료시켰다!", monsterQueue.Count);           //이게 몇마리가 아니라 무조건 4가 나오지
                Console.WriteLine();
                Console.WriteLine("던전에서 다음과 같은 보상을 얻었다.");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"골드 획득! {player.gold}g -> {player.gold += extraReward}g");
                if (rewardItems != null)
                {
                    foreach (Item item in rewardItems) { player.inventory.AddItem(item); Console.WriteLine($"아이템 획득: {item.name}"); }

                }
                Console.WriteLine();
                player.checkLevelUp();
                string Dif = DungeonDifficulty(dungeongClearCount);

                dungeongClearCount += 1;
                player.quest.ClearDungeon(player);
                if (Dif == "Hard")
                {
                    player.quest.DifClear();
                }

            }
            else if (player.isDead == true)
            {

                player.HP += 10;
                player.EXP -= 10;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose!!");
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.WriteLine();
            //if (player.isDead == false)
            //{

            //    Console.WriteLine("던전에서 몬스터 {0}마리를 잡았다!", monsterQueue.Count);           //이게 몇마리가 아니라 무조건 4가 나오지
            //    Console.WriteLine();
            //    Console.WriteLine("던전에서 다음과 같은 보상을 얻었다.");
            //    Console.WriteLine();
            //    Console.WriteLine();
            //    Console.WriteLine($"골드 획득! {player.gold}g -> {player.gold += extraReward}g");
            //    if (rewardItems != null)
            //    {
            //        foreach (Item item in rewardItems) { player.inventory.AddItem(item); Console.WriteLine($"아이템 획득: {item.name}"); }

            //    }
            //    Console.WriteLine();


            //}
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

        public string DungeonDifficulty(int dungeonLevel)//던전 층수 별 난이도 설정
        {
            if (dungeonLevel >= 1 && dungeonLevel <= 5) return "Easy";
            else if (dungeonLevel >= 6 && dungeonLevel <= 15) return "Normal";
            else return "Hard";

        }

        public List<Monster> makeMonster(ref List<Monster> MonstersQueue, string Difficulty)
        {
            int numberOfDraws = rand.Next(1, 5); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)
            if (Difficulty == "Easy")
            {
                for (int i = 0; i < numberOfDraws; i++)
                {
                    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
                    Monster tempMonster = MonsterPreset.baseMonster[monsterIndex].Clone();
                    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
                }
                return MonstersQueue;
            }
            else if (Difficulty == "Normal")
            {

                for (int i = 0; i < numberOfDraws; i++)
                {
                    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
                    Monster tempMonster = MonsterPreset.NormalMonster[monsterIndex].Clone();
                    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
                }
                return MonstersQueue;
            }
            else if (Difficulty == "Hard")
            {

                for (int i = 0; i < numberOfDraws; i++)
                {
                    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
                    Monster tempMonster = MonsterPreset.HardMonster[monsterIndex].Clone();
                    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
                }
                return MonstersQueue;
            }
            else
            {

                for (int i = 0; i < numberOfDraws; i++)
                {
                    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
                    Monster tempMonster = MonsterPreset.EpicMonster[monsterIndex].Clone();
                    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
                }
                return MonstersQueue;
            }
        }


        public List<Skill> addSkills(string role, ref List<Skill> Skills)
        {
            Skills.Clear();

            switch (role)
            {
                case "수강생":
                    foreach (Skill skill in SkillSet.StudentSkills) Skills.Add(skill);
                    return Skills;
                case "튜터":
                    foreach (Skill skill in SkillSet.TutorSkills) Skills.Add(skill);
                    return Skills;
                case "매니저":
                    foreach (Skill skill in SkillSet.ManagerSkills) Skills.Add(skill);
                    return Skills;
                default:
                    foreach (Skill skill in SkillSet.StudentSkills) Skills.Add(skill);
                    return Skills;

            }
        }

        public void UsingSkill(List<Monster> MonsterQueue, Player player)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();




            Console.WriteLine("\n");
            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
            Console.WriteLine($"HP {player.HP}/ {player.maxHP}");
            Console.WriteLine($"MP {player.MP}/{player.maxMP}\r\n");
            for(int i =0; i<SkillList.Count; i++)
            {
                Console.WriteLine(
                   $"{i+1}. {SkillList[i].Name} - MP {SkillList[i].ConsumeMP}\r\n" +
                   $"  {SkillList[i].Description}\r\n\r\n");

            }
           
            Console.WriteLine($"\n0. 취소\n\n행동을 선택해주세요!");
            Console.Write(">>");

            int inputIDX;
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out inputIDX))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
                else if (inputIDX > SkillList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
                else if (input == "0")
                {
                    break;
                }
                
                var selectedSkill = SkillList[inputIDX - 1];

                // 마나 체크
                if (player.MP < selectedSkill.ConsumeMP)
                {
                    Console.WriteLine("마나가 부족합니다.");
                    return;
                }

                // 광역 스킬 처리
                if (selectedSkill.IsAoE)
                {
                    selectedSkill.UseSkill(player, MonsterQueue);  // 모든 몬스터에게 데미지
                    Console.ReadKey();
                }
                else
                {
                    // 단일 타겟 스킬일 경우, 몬스터를 선택해야 함
                    Console.WriteLine("공격할 몬스터를 선택해주세요.");
                    for (int i = 0; i < MonsterQueue.Count; i++)
                    {
                        var monster = MonsterQueue[i];
                        if (!monster.isDead)
                        {
                            Console.WriteLine($"{i + 1}. Lv.{monster.level} {monster.name} (HP: {monster.hP})");
                        }
                    }

                    Console.Write(">> ");
                    int targetIDX;
                    while (true)
                    {
                        string targetInput = Console.ReadLine();
                        if (!int.TryParse(targetInput, out targetIDX) || targetIDX < 1 || targetIDX > MonsterQueue.Count)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            continue;
                        }

                        var targetMonster = MonsterQueue[targetIDX - 1];
                        if (targetMonster.isDead)
                        {
                            Console.WriteLine("이미 처치된 몬스터입니다. 다른 대상을 선택해주세요.");

                        }
                        else
                        {
                            selectedSkill.UseSkill(player, MonsterQueue, targetMonster);  // 특정 몬스터에게 데미지
                            Console.ReadKey();
                            break;
                        }
                    }
                }

                break;


            }

        }
        public void UsePotionDuringBattle(Player player)
        {
            // 사용 가능한 포션 목록을 출력
            var potions = player.inventory.inventory
                             .Where(i => (i.Key is HealthPotion || i.Key is ManaPotion) && i.Value > 0)
                             .ToList();

            if (potions.Count == 0)
            {
                Console.WriteLine("사용할 수 있는 포션이 없습니다.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n사용 가능한 포션 목록:");
            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potions[i].Key.name} ({potions[i].Value}개)");
            }

            Console.Write("포션 선택: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selectedPotion) && selectedPotion > 0 && selectedPotion <= potions.Count)
            {
                var potion = potions[selectedPotion - 1].Key;

                if (potion is HealthPotion healthPotion)
                {
                    player.inventory.UsePotion(healthPotion);
                }
                else if (potion is ManaPotion manaPotion)
                {
                    player.inventory.UsePotion(manaPotion);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        //private int GetRecommendedDEF(string difficulty)//난이도 별 권장 방어력
        //{
        //    switch (difficulty)
        //    {
        //        case "Easy":
        //            return 5;
        //        case "Normal":
        //            return 15;
        //        case "Hard":
        //            return 30;
        //        default:
        //            return 0;
        //    }
        //}
        private int GetBaseReward(string difficulty)//난이도별 기본 보상
        {
            switch (difficulty)
            {
                case "Easy":
                    return 10;
                case "Normal":
                    return 100;
                case "Hard":
                    return 1000;
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

        private void GetItemReward(string difficulty, ref List<Item> rewardItems, int numberOfItems)
        {
            switch (difficulty)
            {
                case "Easy":
                    rewardItems = items.Where(item => item.price <= 1000)
                                   .OrderBy(x => rand.Next())
                                   .Take(numberOfItems)
                                   .ToList();
                    break;
                case "Normal":
                    rewardItems = items.Where(item => item.price <= 1000)
                                   .OrderBy(x => rand.Next())
                                   .Take(numberOfItems)
                                   .ToList();
                    break;
                case "Hard":
                    rewardItems = items.Where(item => item.price <= 1000)
                                   .OrderBy(x => rand.Next())
                                   .Take(numberOfItems)
                                   .ToList();
                    break;
            }
        }



    }
}


