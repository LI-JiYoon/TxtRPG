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



//    //Location에 퀘스트장소 추가 + 퀘스트는 퀘스트 클래스를 상속받는 것으로 (추상화? 인터페이스? 클래스?, 제일 익숙치 않은 추상화?)
//    public abstract class Quest
//    {
//        string QuestName;
//        string QuestProgress;
//        bool isAccepted;
//        bool isCleared;

//        void QuestContents() { }
//        void QuestRewards() { }
//    }

//    public void QuestUI()
//    {
//        //퀘스트 이름들 보여주는데 이미 완료된(isCleared)는 회색처리 및 선택불가, 뒤에 (완료)
//        //진행중인 퀘스트(isAccepted, !isCleared)는 뒤에 (진행중)
//        string text =
//            "QuestName" +
//            "원하시는 퀘스트를 선택해주세요.";
//    }

//    //처음 선택했을 때, isAccepted == false
//    public void DisplaySelectedQuest()
//    {
//        //퀘스트 이름, 설명, 내용, 보상등

//        //수락 또는 거절 이후 메인메뉴 혹은 QuestUI로
//    }

//    //선택 이후, isAccepted == true
//    public void DisplayRunningQuest()
//    {
//        //퀘스트 이름, 설명, 내용, 보상등

//        //퀘스트 중이라면 다음
//        //퀘스트가 완료되었다면 보상받기 또는 돌아가기 이후 메인메뉴 혹은 QuestUI로
//    }

//    public void Accept(Quest quest)
//    {
//        //해당 퀘스트의 isAccepted = true로 변경
//    }
//    public void GetRewards(Quest quest)
//    {
//        //해당 퀘스트의 isCleared = true로 변경
//        //해당 퀘스트의 Rewards를 인벤토리에 추가
//    }

//    //Dungeon에 이 함수
//    public void QuestManage()
//    {
//        //퀘스트들의 isAccepted와 isCleared를 보고
//        //isAccepted && !isCleared인 퀘스트의 내용에 해당하는 Count변수 생성
//        //몬스터 잡기라면 EnterDungeon에 MonsterQueue가 초기화 되기 전에
//        //몬스터 카운트를 반환하게 하는 함수로, 몬스터 종류와 마릿수를 같이 반환해야 하니까.. Dictionary?
//        //반환한 값은 isAccepted && !isCleared인 퀘스트만을 수정해야함
//    }


//}
