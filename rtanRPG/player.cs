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
        public string role;
        public float ATK;
        public float DEF;
        public float HP = 100;
        public float MP = 100;
        public float maxHP = 100;
        public float maxMP = 100;
        public float gold = 1500;
        public float dungeonClearCount = 0;

        public int extraAtk;
        public int extraDef;

        public float EXP = 0;
        public bool weapon = false;
        public bool armor = false;
        public bool isDead => HP <= 0;              //0이하일 때 True, 초과면 자동으로 false
        public Inventory inventory;
        public MiniGame minigame;
        public Quest quest;

        public Player()
        {
            inventory = new Inventory(this);
            minigame = new MiniGame(this);
            quest = new Quest(this);
        }

        private static Dictionary<string, (float ATK, float DEF)> roleStats = new Dictionary<string, (float, float)> 
    {
        {"수강생", (80, 5)},   
        {"튜터", (70, 20)},
        {"매니저", (90, 15)}
    };

        public void SetPlayerStats(string selectedRole)
        {
            if (roleStats.ContainsKey(selectedRole))
            {
                (this.ATK, this.DEF) = roleStats[selectedRole];
                this.role = selectedRole;
            }
            else
            {
                Console.WriteLine("잘못된 직업입니다.");
            }
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
                (extraAtk == 0 ? $"공격력 : {ATK}\r\n" : $"공격력 : {ATK + extraAtk} (+{extraAtk})\r\n") +
                (extraDef == 0 ? $"방어력 : {DEF}\r\n" : $"방어력 : {DEF + extraDef} (+{extraDef})\r\n") +
                $"체 력 : {HP}/{maxHP}\r\n" +
                $"마나 : {MP}/{maxMP}\r\n" +
                $"경험치 : {EXP}\r\n" +
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
            while (EXP >= NeededEXP(level))
            {
                int formerLevel = level;
                EXP -= NeededEXP(level);
                levelUp();
                Console.WriteLine($"레벨이 {formerLevel} -> {level}로 올랐습니다!");

            }
        }
        public void levelUp()
        {

            level += 1;
            ATK += 0.5f;
            DEF += 1;
            maxHP += 50;
            maxMP += 50;
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
                Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 매니저님을(를) 공략했지만 아무일도 일어나지 않았다...");
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
                    Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 매니저님께 적극적으로 어필했다! [데미지 : {damage}] - 하트 뿜뿜!!");
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
                    Console.WriteLine($"Lv.{Monsterlevel} {MonsterName} 매니저님께 조르기를 시전했다! [데미지 : {finalDamage}]");
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

        // 플레이어 회복 메서드 (amount로 회복량 조절)
        public void Heal(float hpAmount, float mpAmount)
        {
            if (hpAmount > 0)
            {
                HP += hpAmount;

                if (HP > maxHP)
                {
                    HP = maxHP;
                }
            }

            if (mpAmount > 0)
            {
                MP += mpAmount;

                if (MP > maxMP)
                {
                    MP = maxMP;
                }
            }
        }
    }
}
