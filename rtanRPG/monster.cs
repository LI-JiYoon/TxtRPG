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
        string _name = "";
        int _hp;
        int _atkPower;
        int _level;

        // Properties
        public string name { get; set; } = "";
        public int hP { get; set; }
        public int atkPower { get; set; }
        public int level { get; set; }
        public bool isDead { get { return hP <= 0 ? true : false; } }
        // (조건) ? 참일경우 : 거짓일경우
        // ? : 구분자
        // 참일경우 : 조건이 참일 경우에 리턴시킬 값
        // 거짓일경우 : 조건이 거짓일 경우에 리턴시킬 값

        // 몬스터의 기본 생성자
        public Monster(string name, int hp, int attackPower, int LV)
        {
            this._name = name;
            this._hp = hp;
            this._atkPower = attackPower;
            this._level = LV;

            Init();
        }

        public Monster Clone()
        {
            return new Monster(_name, _hp, _atkPower, _level);
        }

        // 초기화
        public void Init()
        {
            name = _name;
            hP = _hp;
            atkPower = _atkPower;
            level = _level;
        }

        // 몬스터 공격 메서드
        public virtual int Attack(string Playername)
        {

            Console.WriteLine($"Lv.{level} {name}의 공격!\r\n" +

                $"{Playername} 을 맞췄습니다  [데미지 : {atkPower}]\r\n\r\n");

            return atkPower;
        }

        // 몬스터 피해 메서드
        public void TakeDamage(int damage)
        {
            int formerHp = hP;
            hP -= damage;

            if (hP <= 0)
            {
                hP = 0;
                Console.WriteLine($"Lv.{level} {name}\r\n" + $"HP {formerHp} -> Dead\r\n");

            }
            else
            {
                Console.WriteLine($"Lv.{level} {name}\r\n" + $"HP {formerHp} -> {hP}\r\n");
            }
        }


        // 몬스터 정보 메서드

        public string MonsterInfo(int idx)
        {

            return ($"{idx + 1} Lv.{level} {name}  HP {hP}\\r\\n");
        }

    }


    public class MonsterPreset
    {
        public static List<Monster> baseMonster = new List<Monster>()
        {
            new Monster("김록기", 5, 4, 2),
            new Monster("안혜린", 6, 7, 10),
            new Monster("강채린", 7, 10, 5)
        };
        public static List<Monster> NormalMonster = new List<Monster>()
        {
            new Monster("보다 강해진 김록기", 8, 8, 5),
            new Monster("보다 강해진 안혜린", 9, 14, 15),
            new Monster("보다 강해진 강채린", 10, 20, 10)
        };
        public static List<Monster> HardMonster = new List<Monster>()
        {
            new Monster("눈부시게 강한 김록기", 15, 20, 10),
            new Monster("눈부시게 강한 안혜린", 20, 30, 20),
            new Monster("눈부시게 강한 강채린", 25, 50, 15)
        };
        public static List<Monster> EpicMonster = new List<Monster>()
        {
            new Monster("김록기(대장)", 1000, 100, 20),
            new Monster("안혜린(대장)", 1500, 150, 100),
            new Monster("강채린(대장)", 2000, 200, 50)
        };
    }
}
