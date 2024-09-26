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

        public List<Item> items = new List<Item>();

        public void display()
        {
            string text =
                "인벤토리\r\n" +
                "보유 중인 아이템을 관리할 수 있습니다.\r\n\r\n" +

                "[아이템 목록]\r\n\r\n" +

                PrintInventory() + "\r\n" +

                "1. 장착 관리\r\n" +
                "0. 나가기\r\n\r\n" +


                "원하시는 행동을 입력해주세요.\r\n" +

                ">>";

            Console.WriteLine(text);
     


            while (true)
            {
                string inputText = Console.ReadLine();
                
                if (!int.TryParse(inputText, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

               
                if (inputIDX > items.Count)
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
            items.Add(item);

        }
        public void RemoveItem(Item item)//아이템 추가 메서드
        {
            items.Remove(item); 

        }


        /// <summary>
        /// 인벤토리에 아이템이 있는지 확인하는 메서드
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasItem(Item item)
        {
            foreach (var i in items)
            {
                if (i.name == item.name) // 이름이 같으면 같은 아이템으로 간주
                {
                    return true;
                }
            }
            return false;
        }

     
        public void equip()
        { 
            string text =
                "장착관리\r\n" +
                "보유 중인 아이템을 관리할 수 있습니다.\r\n\r\n" +

                "[아이템 목록]\r\n\r\n" +

                PrintInventoryWithIDX()+ "\r\n" +

                "0. 나가기\r\n\r\n" +


                "원하시는 행동을 입력해주세요.\r\n" +

                ">>";

            Console.WriteLine(text);
            

            while (true)
            {
                string inputText = Console.ReadLine();
                int inputIDX = int.Parse(inputText) - 1;

                if (inputIDX > items.Count) 
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                

                if (inputText == "0")
                {
                    Location.SetLocation(STATE.인벤토리);
                    break;
                }
                else
                {
                    // 이름 앞에 [E]가 붙어있으면 [E]를 제거
                    if (items[inputIDX].name.Substring(0, 3) == "[E]")
                        
                        items[inputIDX].name = items[inputIDX].name.Substring(3);
                    else
                    {
                        foreach (var item in items)
                        {
                            if (item.name.Substring(0, 3) == "[E]" && 
                                item.type == items[inputIDX].type)
                            {
                                item.name = item.name.Substring(3);
                                break;
                            }    
                        }
                        items[inputIDX].name = "[E]" + items[inputIDX].name;
                    }
                    break;

                }
            }
        }

        public string PrintInventory()
        {
            string PrintInventory = "";
            if (items.Count > 0)
            {

                for (int i = 0; i < items.Count; i++)
                {

                    PrintInventory += "-" + $"{items[i].Label()}" + "\r\n";
                }
            }
            else
            {
                PrintInventory = "보유하신 아이템이 없습니다.";
            }
            return PrintInventory;
        }
        public string PrintInventoryWithIDX()
        {
            string PrintInventoryWithIDX = "";
            if (items.Count > 0)
            {

                for (int i = 0; i < items.Count; i++)
                {

                    PrintInventoryWithIDX += "-"  + $"{i + 1}" + $"{items[i].Label()}" + "\r\n";
                }
            }
            else
            {
                PrintInventoryWithIDX = "보유하신 아이템이 없습니다.";
            }
            return PrintInventoryWithIDX;
        }
        
    }
    }



