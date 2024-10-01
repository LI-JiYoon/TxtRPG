using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static rtanRPG.dungeon;

namespace rtanRPG
{

    internal class SHM
    {
        private Player player;

        public SHM(Player player)
        {
            this.player = player;

        }

        public void UsePotionInBattleUI()
        {
            Console.Clear();

            Dictionary<int, Item> indexedInventory = new Dictionary<int, Item>();
            int index = 1;
            foreach (KeyValuePair<Item, int> i in player.inventory.inventory)
            {
                if (i.Value > 0)
                {
                    indexedInventory.Add(index, i.Key);
                    index++;
                }
            }
            string text =
            "[사용가능한 포션]\r\n" +

           $"{PrintPotionWithNum}\r\n" +
            //
            //
            "0 - 나가기\r\n\r\n" +
            //
            "사용할 아이템 번호를 입력하세요\r\n\r\n" +
            //
            ">>";
            Console.WriteLine(text);

            while (true)
            {
                string inputText = Console.ReadLine();
                if (!int.TryParse(inputText, out int inputIDX) || inputIDX > indexedInventory.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                inputIDX--;


                if (inputText == "0")
                {
                    //이 루프가 break되면서 이 UI함수가 종료되면고 DisplayBattleUI의 루프가 계속된다.
                    break;
                }
                if (indexedInventory.TryGetValue(inputIDX + 1, out Item itemKey))
                {
                    if (itemKey is HealthPotion healthpotion)
                    {
                        player.inventory.UsePotion(healthpotion);
                        break;
                    }
                    else if (itemKey is ManaPotion manapotion)
                    {
                        player.inventory.UsePotion(manapotion);
                        break;
                    }
                }

            }
        }
        public string PrintPotionWithNum()
        {
            string PrintPotionWithNum = "";
            int number = 1;
            if (player.inventory.inventory.Values.All(x => x == 0))
            {
                PrintPotionWithNum = "보유하신 아이템이 없습니다.";
            }
            else
            {
                foreach (KeyValuePair<Item, int> i in player.inventory.inventory)
                {
                    if (i.Value > 0 && i.Key is Potion)
                    {
                        PrintPotionWithNum += "-" + $"{number}" + $"{i.Key.name}" + $"{i.Value}개" + "\r\n";
                        number++;
                    }
                }
            }
            return PrintPotionWithNum;
        }
    }


}
