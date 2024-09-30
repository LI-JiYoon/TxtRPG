using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public class Player
    {
        Random rand = new Random();
        public int level = 1;
        public string name = "";
        public string role = "전사";
        public float ATK = 80;
        public float DEF = 5;
        public float HP = 100;
        public float gold = 1500;
        public float dungeonClearCount = 0;

        public float EXP = 0;
        public bool weapon = false;
        public bool armor = false;
        public bool isDead => HP <= 0;              //0이하일 때 True, 초과면 자동으로 false
        public Inventory inventory;


        public Player()
        {
            inventory = new Inventory(this);
        }


        /// <summary>
        /// 상태보기
        /// </summary>
        public void DisplayStatus()
        {
            Console.Clear();
            string text =
                $"Lv. {level}      \r\n" +
                $"{name} ( {role} )\r\n" +
                $"공격력 : {ATK}\r\n" +
                $"방어력 : {DEF}\r\n" +
                $"체 력 : {HP}\r\n" +
                $"Gold : {gold} G\r\n\r\n" +

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
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

        }
        public void checkLevelUp()
        {

            // 던전 클리어 횟수가 현재 레벨에서 필요한 횟수를 초과할 경우 레벨업
            if (EXP >= NeededEXP(level))
            {

                levelUp();
            }
        }
        public void levelUp()
        {

            level += 1;
            ATK += 0.5f;
            DEF += 1;
            EXP = 0; // 클리어 횟수 초기화



        }
        public int NeededEXP(int level)   // 레벨업에 필요한 경험치
        {

            if (level == 1) { return 10; }
            else if (level == 2) { return 35; }
            else if (level == 3) { return 65; }
            else if (level == 4) { return 100; }
            else return 0;
        }

        public float Attack(string MonsterName, int Monsterlevel)
        {


            Console.WriteLine($"{name}의 공격!");
            // 회피 기능 (10% 확률로 회피)
            if (rand.Next(0, 101) <= 10)
            {
                Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return 0;
            }
            else
            {
                // 치명타 기능 (15% 확률로 160% 데미지)
                float damage = ATK;

                if (rand.Next(0, 101) <= 15)
                {
                    Console.Clear();
                    damage = (int)(damage * 1.6);
                    Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                    return damage;

                }
                else
                {
                    // 공격력의 10% 계산
                    damage = (int)ATK * 0.1f;
                    // 소수점 올림 처리
                    float damgeError = (float)Math.Ceiling(damage);
                    // 최소 및 최대 공격력 계산
                    float minDamage = ATK - damgeError;
                    float maxDamage = ATK + damgeError;

                    // 랜덤 공격력 생성

                    int finalDamage = rand.Next((int)minDamage, (int)(maxDamage + 1));
                    Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 을(를) 맞췄습니다. [데미지 : {finalDamage}]");
                    return finalDamage;
                }

                // 몬스터에게 데미지 입히기


            }
        }

        // 플레이어ㅏ 피해 메서드
        public void TakeDamage(int damage)
        {
            int formerHp = (int)HP;
            HP -= damage;

            if (HP <= 0) { HP = 0; }
            else
            {
                Console.WriteLine($"Lv.{level} {name}\r\n" + $"HP {formerHp} -> {HP}\r\n");
            }
        }

    }
}
