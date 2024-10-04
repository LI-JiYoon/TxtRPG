using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace rtanRPG
{
    public class Inventory
    {

        private Player player;
        public Inventory(Player player)
        {
            this.player = player;
        }

        public Dictionary<Item, int> inventory = new Dictionary<Item, int>()
        {
           { new DefItem("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000), 0 },
           { new DefItem("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다", 3500), 0 },
           { new AtkItem("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600), 0 },
           { new AtkItem("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500), 0 },
           { new AtkItem("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000), 0 },
           { new AtkItem("뿅망치", 1, "뿅망치에 맞으면 삐용삐용.", 300), 0 },
           { new HealthPotion("체력포션", 50, "물배를 채워 허기감을 약간 채워줍니다.", 500), 0},
           { new HealthPotion("고급 체력포션", 80, "물배를 채워 허기감을 많이 채워줍니다.", 800), 0},
           { new ManaPotion("마나포션", 100, "용기를 얻어 부탁할 수 있습니다.", 800), 0 }
        };
        public List<Item> equipList = new List<Item>();


        public Item[] InitalInventoryArray(Dictionary<Item, int> inventory)
        {
            Item[] items = new Item[inventory.Count];
            int index = 0;
            foreach (KeyValuePair<Item, int> item in inventory)
            {
                items[index] = item.Key;
                index++;
            }
            return items;
        }
        public void display()
        {
            Console.Clear();

            string text =
                "인벤토리\r\n" +
                "보유 중인 아이템을 관리할 수 있습니다.\r\n\r\n" +

                "[아이템 목록]\r\n\r\n" +

                PrintInventory() + "\r\n\r\n\r\n" +

               $"보유 골드 : {player.gold}\r\n\r\n" +

                "1. 장착 관리\r\n" +
                "0. 나가기\r\n\r\n" +


                "원하시는 행동을 입력해주세요.\r\n" +

                ">>";

            Console.WriteLine(text);



            while (true)
            {
               
                string inputText = Console.ReadLine();
                music.soundEffectPlay("select.wav");
                if (!int.TryParse(inputText, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;


                if (inputIDX > inventory.Count)
                { Console.WriteLine("잘못된 입력입니다."); continue; }

                if (inputText == "0")
                {
                    Location.SetLocation(STATE.마을);
                    break;
                }
                else if (inputText == "1")
                {

                    equip();
                    break;
                }



            }
        }


        public void AddItem(Item item)//아이템 추가 메서드
        {
            inventory[item] += 1;
        }
        public void RemoveItem(Item item)//아이템 제거 메서드
        {
            inventory[item] -= 1;
            if (inventory[item] < 0)
            {
                inventory[item] = 0;
            }

        }

        public void UsePotion(HealthPotion potion) // hp
        {
            player.Heal(potion.ability, 0);
            RemoveItem(potion);
            Console.WriteLine($"{potion.name}사용!\n플레이어의 {potion.type}을 {(int)potion.ability} 회복했습니다");
            Thread.Sleep(500);
        }
        public void UsePotion(ManaPotion potion) // mp
        {
            player.Heal(0, potion.ability);
            RemoveItem(potion);
            Console.WriteLine($"{potion.name}사용!\n플레이어의 {potion.type}을 {(int)potion.ability} 회복했습니다");
            Thread.Sleep(500);
        }

        /// <summary>
        /// 인벤토리에 아이템이 있는지 확인하는 메서드
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasItem(Item item)
        {
            if (inventory.ContainsKey(item) && inventory[item] > 0)
            {
                return true;
            }
            else if (inventory.ContainsKey(item) && inventory[item] <= 0)
            {
                return false;
            }
            else
            {
                Console.WriteLine("잘못된 아이템");
                Thread.Sleep(500);
                return false;
            }
        }

        public void equip()
        {
            Console.Clear();

            Dictionary<int, Item> indexedInventory = new Dictionary<int, Item>();
            int index = 1;
            foreach (KeyValuePair<Item, int> i in inventory)
            {
                if (i.Value > 0)
                {
                    indexedInventory.Add(index, i.Key);
                    index++;
                }
            }

            string text =
                "장착관리\r\n" +
                "보유 중인 아이템을 관리할 수 있습니다.\r\n\r\n" +

                "[아이템 목록]\r\n\r\n" +

                PrintInventoryWithIDX() + "\r\n" +

                "0. 나가기\r\n\r\n" +


                "원하시는 행동을 입력해주세요.\r\n" +

                ">>";

            Console.WriteLine(text);


            while (true)
            {
                string inputText = Console.ReadLine();
                music.soundEffectPlay("select.wav");
                if (!int.TryParse(inputText, out int inputIDX) || inputIDX > indexedInventory.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                inputIDX--;


                if (inputText == "0")
                {
                    Location.SetLocation(STATE.인벤토리);
                    break;
                }
                if (indexedInventory.TryGetValue(inputIDX + 1, out Item itemKey))
                {
                    if (!(itemKey is Potion))
                    {
                        EquipItem(itemKey);
                        break;
                    }
                    else if (itemKey is HealthPotion healthpotion)
                    {
                        UsePotion(healthpotion);
                        break;
                    }
                    else if (itemKey is ManaPotion manapotion)
                    {
                        UsePotion(manapotion);
                        break;
                    }
                }

            }
        }

        public void EquipItem(Item item)
        {
            if (item.name.Substring(0, 3) == "[E]")
            {
                item.name = item.name.Substring(3);
                if (item is AtkItem)
                {
                    player.extraAtk -= (int)item.ability;
                }
                else if (item is DefItem)
                {
                    player.extraDef -= (int)item.ability;

                }
                equipList.Remove(item);
            }

            else
            {
                foreach (KeyValuePair<Item, int> i in inventory)
                {
                    if (i.Key.name.Substring(0, 3) == "[E]" &&
                        i.Key.type == i.Key.type)
                    {
                        i.Key.name = i.Key.name.Substring(3);
                        break;
                    }
                }

                item.name = "[E]" + item.name;
                if (item is AtkItem)
                {
                    player.extraAtk += (int)item.ability;
                }
                else if (item is DefItem)
                {
                    player.extraDef += (int)item.ability;
                }
                equipList.Add(item);
            }
        }

        public string PrintInventory()
        {
            string PrintInventory = "";
            if (inventory.Values.All(x => x == 0))
            {
                PrintInventory = "보유하신 아이템이 없습니다.";
            }
            else
            {
                foreach (KeyValuePair<Item, int> i in inventory)
                {
                    if (i.Value > 0)
                        PrintInventory += "-" + $"{i.Key.Label()}" + $"{i.Value}개" + "\r\n";
                }
            }

            return PrintInventory;
        }
        public string PrintInventoryWithIDX()
        {
            string PrintInventoryWithIDX = "";
            int number = 1;
            if (inventory.Values.All(x => x == 0))
            {
                PrintInventoryWithIDX = "보유하신 아이템이 없습니다.";
            }
            else
            {
                foreach (KeyValuePair<Item, int> i in inventory)
                {
                    if (i.Value > 0)
                    {
                        PrintInventoryWithIDX += "-" + $"{number}" + $"{i.Key.Label()}" + $"{i.Value}개" + "\r\n";
                        number++;
                    }
                }
            }
            return PrintInventoryWithIDX;
        }

    }
}



