using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rtanRPG
{
    public class Skill
    {
        public string Name { get; set; }  // 스킬 이름
        public int Damage { get; set; }   // 스킬의 데미지
        public bool IsAoE { get; set; }   // 광역딜 여부 (true면 광역딜, false면 단일딜)

        public string Description { get; set; }
        public int ConsumeMP {  get; set; } 
        // 생성자
        public Skill(string name, int damage, bool isAoE, int consumeMP, string description)
        {
            Name = name;
            Damage = damage;
            IsAoE = isAoE;
            ConsumeMP = consumeMP;
            Description = description;  
        }

        // 스킬 사용 메서드
        public virtual void UseSkill(Player player, List<Monster> monsters, Monster target = null)
        {
            Console.Clear();
            if (IsAoE)
            {
                Console.WriteLine($"{Name} 시전! 모든 매니저님(들)에게 {Damage} 만큼 어필합니다.");
                foreach (var monster in monsters)
                {
                    if (monster.isDead) continue;
                    monster.TakeDamage(Damage);  // 각 몬스터에게 광역 데미지
                    player.MP -= ConsumeMP;
                    if(player.MP < 0)
                    {
                        player.MP = 0;
                    }
                }
            }
            else
            {
                if (target != null)
                {
                    Console.WriteLine($"{Name} 시전! {target.name} 매니저님에게 {Damage} 만큼 어필합니다.");
                    target.TakeDamage(Damage);  // 특정 몬스터에게 단일 데미지
                    player.MP -= ConsumeMP;
                    if (player.MP < 0)
                    {
                        player.MP = 0;
                    }
                }
            }
        }
    }


    // 수강생 스킬
    public class StudentSkill : Skill
    {
        public StudentSkill(string name, int damage, bool isAoE, int consumeMP, string Description)
            : base(name, damage, isAoE, consumeMP, Description) { }



    }

    // 튜터 스킬
    public class TutorSkill : Skill
    {
        public TutorSkill(string name, int damage, bool isAoE, int consumeMP, string Description)
            : base(name, damage, isAoE, consumeMP, Description) { }
    }

    // 매니저 스킬
    public class ManagerSkill : Skill
    {
        public ManagerSkill(string name, int damage, bool isAoE, int consumeMP, string Description)
            : base(name, damage, isAoE, consumeMP, Description) { }
    }

    public static class SkillSet
    {
        // 수강생 스킬 리스트
        public static List<Skill> StudentSkills = new List<Skill>()
    {
        new StudentSkill("깨물고 싶어! 수강생의 폭풍 애교!♥", 50, false, 10, "폭풍애교로 매니저님 한 명을 함락시킵니다. 공략지수 50"),  // 단일딜
        new StudentSkill("귀여움 폭발 수강생의 손하트♥ ", 30, true, 15, "손하트로 모든 매니저님 마음에 하트 도장을 찍습니다. 공략지수 30")   // 광역딜
    };

        // 튜터 스킬 리스트
        public static List<Skill> TutorSkills = new List<Skill>()
    {
        new TutorSkill("매혹적인 튜터의 윙크♥", 70, false, 10, "윙크로 그의 마음을 흔들어봅시다. 공략지수 70"),  // 단일딜
        new TutorSkill("심쿵유발 튜터의 사랑의 눈빛♥", 40, true, 15, "그윽한 눈빛으로 모든 매니저님의 하-또를 뻇아옵시다 . 공략지수 40") // 광역딜
    };

        // 매니저 스킬 리스트
        public static List<Skill> ManagerSkills = new List<Skill>()
    {
        new ManagerSkill("앙큼폭스! 매니저의 볼콕!", 60, false, 10, "너무 앙큼하다! 저 손가락이 내 입덕계기야! 매니저님 한 분께 공략지수 60"),  // 단일딜
        new ManagerSkill("반전매력! 매니저의 깜찍 댄스♥", 50, true, 15, "아니 이런 매력이! 넘어가지 않을 수가 없다! 모든 매니저님께 공략지수 50")    // 광역딜
    };
    }


    //internal class SkillManager
    //{ 
    //    public List<Skill> skills = new List<Skill>();
    //    public void InitializeSkills()
    //    {
    //        skills.Add(new Skill { Name = "조르기", Description = "플레이어 공격력을 20% 증가시킵니다.", Job = "수강생" });
    //        skills.Add(new Skill { Name = "", Description = "모든 몬스터의 공격력이 10% 감소합니다.", Job = "수강생" });
    //        skills.Add(new Skill { Name = "", Description = "방어력을 10% 증가시킵니다.", Job = "튜터" });
    //        skills.Add(new Skill { Name = "윙크", Description = "모든 몬스터의 (5~15)HP 가 감소됩니다.", Job = "튜터" });
    //        skills.Add(new Skill { Name = "", Description = "공격력과 방어력이 모두 20% 증가시킵니다.", Job = "매니저" });
    //        skills.Add(new Skill { Name = "", Description = "(알아서 부탁합니다) 무적 상태가 됩니다", Job = "매니저" });
    //    }

    //}

}
