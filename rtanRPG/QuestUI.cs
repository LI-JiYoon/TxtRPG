using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    //Location에 퀘스트장소 추가
    public class QuestUI
    {
        private Player player; // 플레이어 객체를 저장할 필드
        private Dictionary<Item, int> inventory;
        public Dictionary<int, Quest> quests;

        
        public QuestUI(Player player)
        {
            this.player = player;
            this.inventory = player.inventory.inventory;
            quests = new Dictionary<int, Quest>();
            AddQuest(quest01);
            AddQuest(quest02);
            //AddQuest(quest03);
            //AddQuest(quest04);
        }

        
        Quest quest01 = new DungeonClearQuest(01, "당신 배가 고프지 않은가?",
            "배가 고픈 당신! 매니저님들과 가까워지고싶지만 소심한 I라 말못하는 당신!\r\n" +
            "어디한번 매니저님께 부탁해보지 않겠나? 밥을 사달라고!\r\n\r\n던전클리어5회", "1000G", 5);
        Quest quest02 = new InventoryQuest(02, "더욱 강하게 어필하기!",
            "부탁드려봤지만 아직 부족하다! 더 강하게 어필해야할 필요가 있다!\r\n\r\n", "스파르타의 창", "청동 도끼");
        //Quest quest03 = new DifficultDungeon(03, "강해진 상태로 어필하기", "이젠... 사주시겠죠?\r\n\r\n16층 이상 클리어",
        //    "5000G");
        //Quest quest04 = new HardMonsterCatch(04, "최종병기 매니저님", "이제 연습은 끝났다. 매니저님들께 부탁을 드리러 가자.", "10000G");


        public Quest GetQuest(int num)
        {
            foreach(KeyValuePair<int, Quest> i in quests)
            {
                if(i.Key == num)
                {
                    return i.Value;
                }
            }
            return null;
        }

        public void DisplayQuestUI()
        {

            bool isSelected = false;

            while (isSelected == false)
            {
                foreach (KeyValuePair<int, Quest> i in quests)      //모든 퀘스트 성공여부 체크
                {
                    i.Value.CheckComplete(player);
                }
                Console.Clear();
                string text =
                   $"{ShowQuests()}\r\n" +
                   "0. 나가기 \r\n\r\n" +
                    "원하시는 퀘스트를 선택해주세요.\r\n";
                Console.WriteLine(text);

                string input = Console.ReadLine();
                music.soundEffectPlay("select.wav");
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }

                switch (inputIDX)
                {
                    case 0:
                        {
                            isSelected = true;

                            Location.SetLocation(STATE.마을);
                            break;
                        }
                    case 1:
                        if (quest01.isCleared)
                            break;
                        else if (quest01.readyToClear)
                        {
                            DisplayReadyToClearQuest(quest01);
                            isSelected = true;
                            break;
                        }
                        else
                        {
                            isSelected = true;

                            DisplaySelectedQuest(quest01); break;
                        }
                    case 2:
                        if (quest02.isCleared)
                            break;
                        else if (quest02.readyToClear == true)
                        {
                            isSelected = true;

                            DisplayReadyToClearQuest(quest02);
                            break;
                        }
                        else
                        {
                            isSelected = true;

                            DisplaySelectedQuest(quest02); break;
                        }
                    //case 3:
                    //    if (quest03.isCleared)
                    //        break;
                    //    else if (quest03.readyToClear)
                    //    {
                    //        isSelected = true;

                    //        DisplayReadyToClearQuest(quest03);
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        isSelected = true;

                    //        DisplaySelectedQuest(quest03); break;
                    //    }
                    //case 4:
                    //    if (quest04.isCleared)
                    //        break;
                    //    else if (quest04.readyToClear)
                    //    {
                    //        isSelected = true;

                    //        DisplayReadyToClearQuest(quest04);
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        isSelected = true;

                    //        DisplaySelectedQuest(quest04); break;
                    //    }

                    default:
                        Console.WriteLine("잘못된 입력입니다."); continue;
                }
            }
        }
        public void DisplaySelectedQuest(Quest quest)
        {
            quest.CheckComplete(player);
            Console.Clear();
            string text =
                $"{quest.QuestName}\r\n\r\n" +
                $"{quest.Description}\r\n\r\n" +
                $"{WhatToDoForQuest(quest)}\r\n" +
                $"보상\r\n{quest.QuestReward}\r\n\r\n" +
                "1. 수락" +
                "0.나가기\r\n\r\n" +
                "행동을 입력하세요";
            Console.WriteLine(text);

            while (true)
            {
                string input = Console.ReadLine();
                music.soundEffectPlay("select.wav");
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }

                if (inputIDX == 0)
                    break;
                else if (inputIDX == 1)
                {
                    Accept(quest); break;
                }
            }
        }


        //선택 이후, isAccepted == true
        public void DisplayReadyToClearQuest(Quest quest)
        {
            Console.Clear();
            string text =
                $"{quest.QuestName}\r\n\r\n" +
                $"{quest.Description}\r\n\r\n" +
                $"{WhatToDoForQuest(quest)}\r\n\r\n" +
                $"보상\r\n{quest.QuestReward}\r\n\r\n" +
                "1. 완료하기" +
                "0.나가기\r\n\r\n" +
                "행동을 입력하세요";
            Console.WriteLine(text);

            while (true)
            {
                string input = Console.ReadLine();
                
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }

                if (inputIDX == 0)
                    break;
                else if (inputIDX == 1)
                {
                    music.soundEffectPlay("clear.wav");
                    GetRewards(quest); break;
                }
            }
        }

        public void Accept(Quest quest)
        {
            if (quest.isAccepted)
            {
                Console.WriteLine("이미 수락한 퀘스트입니다.");
            }
            else
            {
                quest.isAccepted = true;
            }
        }

        public void GetRewards(Quest quest)
        {
            quest.isCleared = true;
            quest.Reward(player);               
        }

        public void AddQuest(Quest quest)
        {
            if (!quests.ContainsKey(quest.Id))
            {
                quests.Add(quest.Id, quest);
            }
        }

        string ShowQuests()
        {
            string ShowAllQuest = "";
            int number =1;
            foreach (KeyValuePair<int, Quest> i in quests)
            {
                string isRunning = "";
                if (!i.Value.isAccepted && !i.Value.readyToClear && !i.Value.isCleared)
                {
                    isRunning += "";
                    //아직 받지 않은 상태
                }
                else if (i.Value.isAccepted && !i.Value.readyToClear && !i.Value.isCleared)
                {
                    isRunning += "(진행중)";
                    //받고 진행중인 상태
                }
                else if (i.Value.isAccepted && i.Value.readyToClear && !i.Value.isCleared)
                {
                    isRunning += "(보상 획득 가능)";
                    //완료버튼 누르면 Ok인 상태
                }
                else if (i.Value.isAccepted && i.Value.readyToClear && i.Value.isCleared)
                { 
                    //이미 완료한 상태
                    Console.ForegroundColor = ConsoleColor.Red;
                    isRunning += "(완료)";
                }
                ShowAllQuest += number +"- " + i.Value.QuestName + isRunning + "\r\n";
                number++;
                Console.ResetColor();
            }
            return ShowAllQuest;
        }

        public string WhatToDoForQuest(Quest quest)
        {
            string ToDo = "";
            if (quest is DungeonClearQuest dungeonClearQuest)
            {
                ToDo += $"{dungeonClearQuest.CurrentClear}/{dungeonClearQuest.RequiredClears}";
            }
            else if (quest is InventoryQuest inventoryQuest)
            {
                ToDo += inventoryQuest.readyToClear ? $"{inventoryQuest.targetItem} 장착완료" : $"{inventoryQuest.targetItem} 장착하기";
            }
            //else if (quest is DifficultDungeon difDun)
            //{
            //    ToDo += difDun.readyToClear ? "1/1" : "0/1";
            //}
            //else if(quest is HardMonsterCatch HMC)
            //{
            //    string WhoCated = "";
            //    WhoCated += HMC.isKimCatch ? "눈부시게 강한 김록기 1/1\r\n" : "눈부시게 강한 김록기 0/1\r\n";
            //    WhoCated += HMC.isAnCatch ? "눈부시게 강한 안혜린 1/1\r\n" : "눈부시게 강한 안혜린 0/1\r\n";
            //    WhoCated += HMC.isGangCatch ? "눈부시게 강한 강채린 1/1" : "눈부시게 강한 강채린 0/1";
            //    ToDo += WhoCated;
            //}
            return ToDo;
        }

    }
}