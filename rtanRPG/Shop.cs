using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace rtanRPG
{
    public class Shop
    {
        float discount = 0.85f;

        private Player player; // 플레이어 객체를 저장할 필드

        
        public Shop(Player player)
        {
            this.player = player;
        }


        Item[] items = new Item[]
        {
            new DefItem("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
            new DefItem("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다", 3500),
            new AtkItem("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new AtkItem("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
            new AtkItem("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000),
            new AtkItem("뿅망치", 1, "뿅망치에 맞으면 삐용삐용.", 300)

        };


        public void Displaying()
        {
            string text =

                "   |\\                     /)\r\n /\\_\\\\__               (_//\r\n|   `>\\-`     _._       //`)\r\n \\ /` \\\\  _.-`:::`-._  //\r\n  `    \\|`    :::    `|/\r\n        |     :::     |\r\n        |.....:::.....|\r\n        |:::::::::::::|\r\n        |     :::     |\r\n        \\     :::     /\r\n         \\    :::    /\r\n          `-. ::: .-'\r\n           //`:::`\\\\\r\n          //   '   \\\\\r\n         |/         \\\\"
                + "\r\n\r\n" + 
                "상점\r\n" +
                "필요한 아이템을 얻을 수 있는 상점입니다.\r\n\r\n" +
                "[보유 골드]\r\n" +
                $"{player.gold} gold\r\n\r\n[" +
                "아이템 목록]\r\n" +

                itemsInven() + "\r\n" +

                "1. 아이템 구매\r\n" +
                "2. 아이템 판매\r\n" +
                "0. 나가기\r\n\r\n" +
                "원하시는 행동을 입력해주세요.\r\n" +
                ">>";

            Console.WriteLine(text);
          
         
            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;



                if (input == "0")
                {
                    Location.SetLocation(STATE.마을);
                    break;
                }
                else if (input == "1")
                {
                    buying();
                    break;

                }
                else if (input == "2")
                {
                    resell();
                    break;

                }
                else { Console.WriteLine("잘못된 입력입니다."); }
               
            }
            
            MyLog.절취선();
        }


        public void buying()
        {

            string text =
               "상점\r\n" +
               "필요한 아이템을 얻을 수 있는 상점입니다.\r\n\r\n" +
               "[보유 골드]\r\n" +
               $"{player.gold} gold\r\n\r\n[" +
               "아이템 목록]\r\n" +

               itemsInvenWithIdx() + "\r\n" +

               "0. 나가기\r\n\r\n" +
               "원하시는 행동을 입력해주세요.\r\n" +
               ">>";

            Console.WriteLine (text);
            

            while (true)
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;
                
                // 예외 처리
                if (inputIDX > items.Length)
                { 
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                if (input == "0") { break; }

                if (player.inventory.HasItem(items[inputIDX]))
                {

                    Console.WriteLine("이미 구매한 아이템입니다");
                    break;
                }
                else
                {
                    if (player.gold < items[inputIDX].price)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        break;
                    }
                    else
                    {
                        player.inventory.AddItem(items[inputIDX]);
                        items[inputIDX].isSoldout = "true";
                        player.gold -= items[inputIDX].price;
                        Console.WriteLine("구매를 완료했습니다.");
                        break;
                    }
                }
            }

        }
        public void resell()
        {
            string text =
               "상점\r\n" +
               "필요한 아이템을 얻을 수 있는 상점입니다.\r\n\r\n" +
               "[보유 골드]\r\n" +
               $"{player.gold} gold\r\n\r\n" +
                "[아이템 목록]\r\n" +

               resellTxt() + "\r\n" +

               "0. 나가기\r\n\r\n" +
               "원하시는 행동을 입력해주세요.\r\n" +
               ">>";
            Console.Write (text);
   
            while (true)
            {
                // 예외 처리
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int inputIDX))
                { Console.WriteLine("잘못된 입력입니다."); continue; }
                inputIDX -= 1;

                if (input == "0") { break; }

                else
                {

            
                    player.gold += player.inventory.items[inputIDX].price * discount;
                    player.inventory.items[inputIDX].isSoldout = "false";
                    player.inventory.RemoveItem(player.inventory.items[inputIDX]);
                    Console.WriteLine("판매를 완료했습니다.");
                    break;
                }
              
                }

            }

        public string itemsInven()

        {
            string itemsInven = "";
            // Location.SetLocation(STATE.상점);
            for (int i = 0; i < items.Length; i++)
            {
                
                    itemsInven += "-" + items[i].Label() + $"{items[i].isSoldout}" + "\r\n";
                



            }
            return itemsInven;
        }
        public string itemsInvenWithIdx()

        {
            string itemsInvenWithIdx = "";
            // Location.SetLocation(STATE.상점);
            for (int i = 0; i < items.Length; i++)
            {
                
                    itemsInvenWithIdx += $"- {i + 1}" + items[i].Label() + $" {items[i].isSoldout}" + "\r\n";
               


            }
            return itemsInvenWithIdx;
        }
        public string resellTxt()

        {
           
            string reseltxt = "";
            // Location.SetLocation(STATE.상점);
            for (int i = 0; i < player.inventory.items.Count; i++)
            {


                reseltxt += $"{i + 1}" + $"{player.inventory.items[i].Label()}" + $"{player.inventory.items[i].price * discount}" +  "\r\n";

            }
            return reseltxt;
        }
    }

    //    Item TraingArmor = new Item("수련자 갑옷", 5, 0, "수련에 도움을 주는 갑옷입니다.", 1000);
    //    Item IronArmor = new Item("무쇠갑옷", 9, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000);
    //    Item SpartanArmor = new Item("스파르타의 갑옷", 15, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다", 3500);
    //    Item OldSword = new Item("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
    //    Item BronzeAx = new Item("청동 도끼", 0, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
    //    Item SpartanSpear = new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000);


    //    Dictionary<string, Item> dict = new Dictionary<string, Item>() 
    //    {
    //        { "무쇠갑옷", new Item("무쇠갑옷", 9, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000)}
    //    };

}
