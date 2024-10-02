﻿using System.Data;
using System.Threading.Tasks.Dataflow;

namespace rtanRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 기능 정의

            string startMessage =
                " ____                   _                ____  ____   ____ \r\n/ ___| _ __   __ _ _ __| |_ __ _ _ __   |  _ \\|  _ \\ / ___|\r\n\\___ \\| '_ \\ / _` | '__| __/ _` | '_ \\  | |_) | |_) | |  _ \r\n ___) | |_) | (_| | |  | || (_| | | | | |  _ <|  __/| |_| |\r\n|____/| .__/ \\__,_|_|   \\__\\__,_|_| |_| |_| \\_\\_|    \\____|\r\n      |_|                                                  "


                + "\r\n\r\n" +

                "스파르타 마을에 오신 여러분 환영합니다.\r\n" +
                "원하시는 이름을 결정해주세요.";

            Player player = new Player();
            Shop shop = new Shop(player);
            breaktime breaktime = new breaktime(player);
            dungeon dungeon = new dungeon(player);
            MiniGame minigame = new MiniGame(player);
            QuestUI questUI = new QuestUI(player);
            //dungeon.onDead += quest4.HandleDead;

            // 내 위치 초기화
            Location.preLocation = STATE.시작;
            Location.currentLocation = STATE.시작;

            // Update
            while (true)
            {
                // 현재 내 위치에 따라서 띄울 텍스트 선택
                switch (Location.currentLocation)
                {
                    case STATE.시작:
                        // 이름입력 코드dd
                        string inputText;
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine(startMessage);
                            inputText = Console.ReadLine();
                            if (inputText != null) break;
                        }
                        player.name = inputText;
                        MyLog.절취선();

                        Console.WriteLine($"입력하신 이름은 {inputText} 입니다.\r\n" +
                           "1. 저장\r\n" +

                           "2. 취소");
                        while (true)
                        {
                            inputText = Console.ReadLine();

                            Console.Clear();
                            if (inputText == "1")
                            {
                                Console.WriteLine(

                                    "1. 수강생\r\n" +
                                    "2. 튜터\r\n" +
                                    "3. 매니저\r\n\r\n" +

                                "원하시는 직업을 입력해주세요.\r\n" +
                                ">>");

                                string roleInput = Console.ReadLine();

                                switch (roleInput)
                                {
                                    // selectedRole 인자에 직업 할당
                                    case "1":
                                        player.SetPlayerStats("수강생");
                                        break;
                                    case "2":
                                        player.SetPlayerStats("튜터");
                                        break;
                                    case "3":
                                        player.SetPlayerStats("매니저");
                                        break;
                                    default:
                                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                                        return;  
                                }
                                                                
                                Console.WriteLine($"선택한 직업은 {player.role} 입니다.\r\n");
                                Console.ReadKey();

                                Location.SetLocation(STATE.마을);
                                break; // 위치를 강제로 마을로 변경
                            }
                            else if (inputText == "2")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("정확한 값을 입력하세요");
                            }
                        }
                        MyLog.절취선();
                        break;

                    case STATE.마을:
                        Console.Clear();

                        Console.WriteLine(

                            "       _T      .,,.    ~--~ ^^\r\n ^^   // \\                    ~\r\n      ][O]    ^^      ,-~ ~\r\n   /''-I_I         _II____\r\n__/_  /   \\ ______/ ''   /'\\_,__\r\n  | II--'''' \\,--:--..,_/,.-{ },\r\n; '/__\\,.--';|   |[] .-.| O{ _ }\r\n:' |  | []  -|   ''--:.;[,.'\\,/\r\n'  |[]|,.--'' '',   ''-,.    |\r\n  ..    ..-''    ;       ''. '"

                            + "\r\n\r\n" +
                            
                            "스파르타 마을에 오신 여러분 환영합니다.\r\n" +
                            "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\r\n\r\n" +
                            
                            "1. 상태 보기\r\n" +
                            "2. 인벤토리\r\n" +
                            "3. 상점\r\n" +
                            "4. 던전\r\n" +
                            "5. 휴식하기\r\n" +
                            "6. 미니게임\r\n" +
                            "7. 퀘스트\r\n\r\n" +

                            "원하시는 행동을 입력해주세요.");
                
                        while (true)
                        {
                            inputText = Console.ReadLine();
                            if (inputText == "1")
                            {
                                Location.SetLocation(STATE.상태보기);
                                break;
                            }
                            else if (inputText == "2")
                            {
                                Location.SetLocation(STATE.인벤토리);
                                break;
                            }
                            else if (inputText == "3")
                            {
                                Location.SetLocation(STATE.상점);
                                break;

                            }
                            else if (inputText == "4")
                            {
                                Location.SetLocation(STATE.던전);
                                break;
                            }
                            else if (inputText == "5")
                            {
                                Location.SetLocation(STATE.휴식하기);
                                break;
                            }
                            else if (inputText == "6")
                            {
                                Location.SetLocation(STATE.미니게임);
                                break;
                            }
                            else if (inputText == "7")
                            {
                                Location.SetLocation(STATE.퀘스트);
                                break;
                            }

                            else
                            {
                                Console.WriteLine("정확한 값을 입력하세요");

                            }

                            }
                        MyLog.절취선();
                        break;

                        // 마을 텍스트 발동!
                        // 0.인벤토리
                        // 1.여관
                        // 2.던전\
                        break;
                    case STATE.인벤토리:
                        player.inventory.display();
                        break;
                        // 0.뒤로가기
                        // 뒤로가기란 location.preLocation 의 위치로 이동
                    case STATE.휴식하기:
                        breaktime.Displaying();
                        break;
                    case STATE.던전:
                        dungeon.Displaying();
                        break;
                    case STATE.상점:
                        shop.Displaying();
                        break;
                    case STATE.상태보기:
                        player.DisplayStatus();
                        break;
                    case STATE.미니게임:
                        minigame.Displaying();
                        break;
                    case STATE.퀘스트:
                        questUI.DisplayQuestUI();
                        break;

                }
            }
        }

    }
}



