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
        public string Name { get; set; }m
        public int HP { get; set; }
        public int AtkPower { get; set; }
        public int level { get; set; }


        // 몬스터의 기본 생성자
        public Monster(string name, int hp, int attackPower, int level)
        {
            Name = name;
            HP = hp;
            AtkPower = attackPower;
        }

        // 몬스터의 기본 공격 메서드
        public virtual void Attack()
        {
            Console.WriteLine($"{Name}이(가) 공격합니다! 공격력: {AtkPower}");
        }

        // 몬스터가 피해를 입는 메서드
        public void TakeDamage(int damage)
        {
            HP -= damage;
            Console.WriteLine($"{Name}이(가) {damage}의 피해를 입었습니다. 남은 체력: {HP}");

        }


    }

    // Monster1 클래스
    public class Monster1 : Monster
    {
        public Monster1() : base("김록기", 100, 10, 2)
        {

        }

        public override void Attack()
        {
            Console.WriteLine($"{Name}이(가) 화염 공격을 사용합니다! 공격력: {AtkPower}");
        }
    }

    // Monster2 클래스
    public class Monster2 : Monster
    {
        public Monster2() : base("안혜린", 150, 15, 10)
        {
        }

        public override void Attack()
        {
            Console.WriteLine($"{Name}이(가) 얼음 공격을 사용합니다! 공격력: {AtkPower}");
        }
    }

    // Monster3 클래스
    public class Monster3 : Monster
    {
        public Monster3() : base("강채린", 200, 20, 5)
        {
        }

        public override void Attack()
        {
            Console.WriteLine($"{Name}이(가) 번개 공격을 사용합니다! 공격력: {AtkPower}");
        }
    }


}



