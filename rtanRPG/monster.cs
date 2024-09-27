using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{



    public class Monster
    {
        // 몬스터의 기본 속성
        public string Name { get; set; }
        public int HP { get; set; }
        public int AtkPower { get; set; }
        public int level { get; set; }

        public bool isdead = false;

        // 몬스터의 기본 생성자
        public Monster(string name, int hp, int attackPower, int LV)
        {
            Name = name;
            HP = hp;
            AtkPower = attackPower;
            level = LV;
        }

        // 몬스터 공격 메서드
        public virtual int Attack(string Playername)
        {

            Console.WriteLine($"Lv.{level} {Name}의 공격!\r\n" +

                $"{Playername} 을 맞췄습니다  [데미지 : {AtkPower}]\r\n\r\n");

            return AtkPower;
        }

        // 몬스터 피해 메서드
        public void TakeDamage(int damage)
        {
            HP -= damage;

            if (HP < 0) { HP = 0; }
            if (HP <= 0)
            {
                Console.WriteLine($"Lv.{level} {Name}\r\n" + $"HP {HP + damage} -> Dead\r\n");

            }
            else
            {
                Console.WriteLine($"Lv.{level} {Name}\r\n" + $"HP {HP + damage} -> {HP}\r\n");
            }
        }

        // 몬스터 죽음 판별 메서드

        public void IsDead(int hp)
        {
            if (hp <= 0)
            {
                isdead = true;
            }

        }

        // 몬스터 정보 메서드

        public string MonsterInfo(int idx)
        {

            return ($"{idx + 1} Lv.{level} {Name}  HP {HP}\\r\\n");
        }

    }

    // Monster1 클래스
    public class Monster1 : Monster
    {
        public Monster1() : base("김록기", 100, 10, 2)
        {

        }

        public override int Attack(string Playername)
        {

            Console.WriteLine($"Lv.{level} {Name}의 공격!\r\n" +

                $"{Playername} 을 맞췄습니다  [데미지 : {AtkPower}]");
            return AtkPower;
        }
    }

    // Monster2 클래스
    public class Monster2 : Monster
    {
        public Monster2() : base("안혜린", 150, 15, 10)
        {
        }

        public override int Attack(string Playername)
        {

            Console.WriteLine($"Lv.{level} {Name}의 공격!\r\n" +

                $"{Playername} 을 맞췄습니다  [데미지 : {AtkPower}]");
            return AtkPower;
        }
    }

    // Monster3 클래스
    public class Monster3 : Monster
    {
        public Monster3() : base("강채린", 200, 20, 5)
        {
        }

        public override int Attack(string Playername)
        {

            Console.WriteLine($"Lv.{level} {Name}의 공격!\r\n" +

                $"{Playername} 을 맞췄습니다  [데미지 : {AtkPower}]");
            return AtkPower;

        }

    }


}
