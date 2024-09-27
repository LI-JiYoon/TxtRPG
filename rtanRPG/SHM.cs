//using system;
//using system.collections.generic;
//using system.diagnostics;
//using system.linq;
//using system.text;
//using system.threading.tasks;
//using static rtanrpg.dungeon;

//namespace rtanrpg
//{
//    internal class shm
//    {

//        int numberofdraws;
//        int minvalue = 1; // 몬스터 뽑기 최소값
//        int maxvalue = 4; // 몬스터 뽑기 최대값

//        monster[] monsters = new monster[]
//       {

//           new monster1(),
//           new monster2(),
//           new monster3()
//       };

//        monster[] monstersqueue = new monster[4];
//        int queueindex = 0;

//        public interface item
//        {
//            public string name { get; set; }
//            public string description { get; set; }
//            public float ability { get; }
//            public float price { get; set; }
//            public bool alreadyhave { get; set; }
//            public string type { get; }


//            public string issoldout { get; set; }

//            public string label();

//        }

//        //포션 클래스
//        //클래스를 상속받는 체력포션.
//        //item.cs에 있어야하는






//        public class potion : item
//        {
//            list<string> potioneffect = new list<string>()
//            {
//                "체력"
//            };

//            public string name { get; set; }
//            public string description { get; set; }
//            public float effect;
//            public float ability { get { return effect; } }
//            public float price { get; set; }
//            public bool alreadyhave { get; set; }
//            public int potiontype;       //포션타입0은 체력포션, 1 ~는 다른타입의 포션
//            public string type { get { return potioneffect[potiontype]; } }
//            public string issoldout { get; set; }


//            public potion(string name, string description, float effect, float price, int potiontype)
//            {
//                this.name = name;
//                this.effect = effect;
//                this.description = description;
//                this.price = price;
//                potiontype = potiontype;
//            }


//            public string label()
//            {
//                return $" {name,-15}| {potioneffect[potiontype]} +{(int)ability,-15}| {description,-15}|";     //왜 -15? 정렬을 위해서
//            }

//            public virtual void usepotion(player player) { }

//        }
//        public class healthpotion : potion 
//        {
//            public string name { get; set; }
//            public string description { get; set; }
//            public float ability { get; }
//            public float price { get; set; }

//            //하급체력포션, 중급체력포션등으로 나눌수도?
//            public healthpotion(string name, string description, float effect, float price) : base(name, description, effect, price, 0)
//            {

//            }
//            //public override void usepotion (player player)
//            //{
//            //    player.hp += ability;
//            //    removeitem(items[6])        // 인벤토리의 items배열에 추가하면 체력포션은 6번이니까
//            //    console.writeline($"{name}사용!\n플레이어의 체력을 {(int)ability} 회복했습니다");
//            //}
//        }

//        // dungeon.cs의 112번째 줄에 적용하면 될 것 같습니다.
//        //플레이어의 maxhp와 현재hp를 구분하면 좋을것 같습니다.
//        //레벨업이나 아이템을 통해 maxhp를 늘릴 수 있게
//        public void showbattleui(monster[] monsterqueue, player player)
//        {
//            console.clear();
//            console.writeline("battle!!");
//            console.writeline();
//            for (int i = 0; i < monsterqueue.length; i++)
//            {
//                console.writeline($"lv.{monsterqueue[i].level} {monsterqueue[i].name}  hp {monsterqueue[i].hp}");
//            }
//            console.writeline("\n");
//            console.writeline($"[내정보]\nlv.{player.level}  {player.name} ({player.role})");
//            console.writeline($"hp 100/{player.hp}");
//            console.writeline();
//            console.writeline($"1. 공격\n\n원하시는 행동을 입력해주세요.");
//            console.write(">>");

//            while (true)
//            {
//                string input = console.readline();
//                if (!int.tryparse(input, out int inputidx))
//                { console.writeline("잘못된 입력입니다."); continue; }
//                inputidx -= 1;

//                if (input == "1")
//                {
//                    //attack함수
//                    break;
//                }
//                else { console.writeline("잘못된 입력입니다."); }
//            }
//        }

//        //일단 체력 100 -> 현재체력으로 했지만 2번째부턴 전투 시작전에 전투전hp에 해당하는 변수를 지정해야..?
//        public void battleend(monster[] monsterqueue, player player)
//        {
//            console.clear();
//            console.writeline("battle!! - result");
//            console.writeline();
//            if (player.isdead == false)
//            {
//                console.foregroundcolor = consolecolor.green;
//                console.writeline("vicotry!");
//                console.writeline();
//                console.resetcolor();
//            }
//            else if (player.isdead == true)
//            {
//                console.foregroundcolor = consolecolor.darkred;
//                console.writeline("you lose!!");
//                console.writeline();
//                console.resetcolor();
//            }
//            console.writeline();
//            if(player.isdead == false)
//            {
//                console.writeline("던전에서 몬스터 {0}마리를 잡았습니다.", monsterqueue.length);
//                console.writeline();
//            }
//            console.writeline($"lv.{player.level} {player.name}\nhp 100 -> {player.hp}");           //데미지 계산에서 hp가 음수가되면 0이되게하던가 여기서 분기를 나눠도 ok
//            console.writeline();
//            console.writeline("0. 다음");
//            console.writeline();
//            console.write(">> ");

//            //전투 보상(경험치, 골드, 아이템)

//            while (true)
//            {
//                string input = console.readline();
//                if (!int.tryparse(input, out int inputidx))
//                { console.writeline("잘못된 입력입니다."); continue; }
//                inputidx -= 1;

//                if (input == "0")
//                {
//                    displaying();       //던전선택창
//                    break;
//                }
//                else { console.writeline("잘못된 입력입니다."); }
//            }
//            //전투대기열의 모든 몬스터의 isdead가 true가 될 때, 이 함수를 호출합니다.
//            //if((monsterqueue.all(x => monsterqueue[x].isdead == true) || player.isdead == true)
//            //{
//            //      battleend(monsterqueue, player)
//            //}
//        }
//    }




//    //회피 = 숫자 랜덤으로 뽑아서 def수치보다 낮은숫자면 공격실패

//}
