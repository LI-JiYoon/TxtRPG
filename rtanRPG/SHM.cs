//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Numerics;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using static rtanRPG.dungeon;

//namespace rtanRPG
//{

//    internal class SHM
//    {
//        private Player player;

//        public SHM(Player player)
//        {
//            this.player = player;

//        }

//        public void UsePotionInBattleUI()
//        {
//            Console.Clear();

//            Dictionary<int, Item> indexedInventory = new Dictionary<int, Item>();
//            int index = 1;
//            foreach (KeyValuePair<Item, int> i in player.inventory.inventory)
//            {
//                if (i.Value > 0)
//                {
//                    indexedInventory.Add(index, i.Key);
//                    index++;
//                }
//            }
//            string text =
//            "[사용가능한 포션]\r\n" +

//           $"{PrintPotionWithNum}\r\n" +
//            //
//            //
//            "0 - 나가기\r\n\r\n" +
//            //
//            "사용할 아이템 번호를 입력하세요\r\n\r\n" +
//            //
//            ">>";
//            Console.WriteLine(text);

//            while (true)
//            {
//                string inputText = Console.ReadLine();
//                if (!int.TryParse(inputText, out int inputIDX) || inputIDX > indexedInventory.Count)
//                {
//                    Console.WriteLine("잘못된 입력입니다.");
//                    continue;
//                }

//                inputIDX--;


//                if (inputText == "0")
//                {
//                    //이 루프가 break되면서 이 UI함수가 종료되면고 DisplayBattleUI의 루프가 계속된다.
//                    break;
//                }
//                if (indexedInventory.TryGetValue(inputIDX + 1, out Item itemKey))
//                {
//                    if (itemKey is HealthPotion healthpotion)
//                    {
//                        player.inventory.UsePotion(healthpotion);
//                        break;
//                    }
//                    else if (itemKey is ManaPotion manapotion)
//                    {
//                        player.inventory.UsePotion(manapotion);
//                        break;
//                    }
//                }

//            }
//        }
//        public string PrintPotionWithNum()
//        {
//            string PrintPotionWithNum = "";
//            int number = 1;
//            if (player.inventory.inventory.Values.All(x => x == 0))
//            {
//                PrintPotionWithNum = "보유하신 아이템이 없습니다.";
//            }
//            else
//            {
//                foreach (KeyValuePair<Item, int> i in player.inventory.inventory)
//                {
//                    if (i.Value > 0 && i.Key is Potion)
//                    {
//                        PrintPotionWithNum += "-" + $"{number}" + $"{i.Key.name}" + $"{i.Value}개" + "\r\n";
//                        number++;
//                    }
//                }
//            }
//            return PrintPotionWithNum;
//        }







//    }
//    //델리게이트 만들고
//    public delegate void EnemyDeadHandler(Monster monster);

//    public void EnterDungeon(string difficulty)
//    {
//        //도망치기 변수 초기화
//        isFleeing = false;

//        // MonsterQueue 초기화
//        MonstersQueue = new List<Monster>();


//        // 몬스터 생성
//        MonstersQueue = makeMonster(ref MonstersQueue, difficulty);

//        //int numberOfDraws = rand.Next(1, 5); // 뽑고 싶은 몬스터의 수를 설정 (1~4사이)
//        //for (int i = 0; i < numberOfDraws; i++)
//        //{
//        //    int monsterIndex = rand.Next(MonsterPreset.baseMonster.Count);
//        //    Monster tempMonster = MonsterPreset.baseMonster[monsterIndex].Clone();
//        //    MonstersQueue.Add(tempMonster); // MonstersQueue에 새로운 몬스터 추가
//        //}

//        //전투구현
//        if (player.isDead) Console.WriteLine("체력을 회복하고 오세요");
//        else PlayBattle();



//        //추가할 내용
//        public event EnemyDeadHandler onDead;

//        foreach(Monster monster in MonstersQueue)
//        {
//            if (monster.isDead)
//            {
//                onDead?.Invoke(monster);
//}
//        }

        




//        //int baseReward = GetBaseReward(difficulty);

//        //// 보상 계산
//        //int reward = CalculateReward(player.ATK, baseReward);
//        //player.gold += reward;
//        //Console.WriteLine($"Gold {player.gold - reward} G -> {player.gold} G");
//        player.dungeonClearCount++;
//player.checkLevelUp();

//    }







//}
