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
        public string Name { get; set; }
        //public string Description { get; set; }
        //public string Job { get; set; } // Job으로 구분

    }

    public class Student(Player player) : Skill
    {
        private Player player = player;
        public void StudentSkill()
        {
            Console.WriteLine($"{player.name}님이 {Name} 스킬을 사용합니다!");

            switch (Name)
            {
                case "조르기": // 단일 스킬 
                    float increaseATK = (player.ATK) * 0.20f;
                    player.ATK += increaseATK;
                    break;
                case " ": //// 광역 스킬 
                    break;
            }
        }
    }
      
    public class Tutor(Player player) : Skill
    {
        private Player player = player;

        public void TutorSkill()
        {
            Console.WriteLine($"{player.name}님이 {Name} 스킬을 사용합니다!");

            switch (Name)
            {
                case "": // 단일 스킬 
                    float increaseDEF = (player.DEF) * 0.20f;
                    player.DEF += increaseDEF;
                    break;
                case " ": // 광역 스킬 
                    break;
            }
        }
    }

    public class Manager(Player player) : Skill
    {
        private Player player = player;
        public void ManagerSkill()
        {
            Console.WriteLine($"{player.name}님이 {Name} 스킬을 사용합니다!");

            switch (Name)
            {
                case "": // 단일 스킬 
                    float increaseDEF = (player.DEF) * 0.20f;
                    float increaseATK = (player.ATK) * 0.20f;
                    player.DEF += increaseDEF;
                    player.ATK += increaseDEF;
                    break;
                case " ": // 광역 스킬 
                    break;
            }
        }
    }

    //internal class SkillManager
    //{ 
    //    public List<Skill> skills = new List<Skill>();
    //    public void InitializeSkills()
    //    {
    //        skills.Add(new Skill { Name = "조르기", Description = "플레이어 공격력을 20% 증가시킵니다.", Job = "수강생" });
    //        skills.Add(new Skill { Name = "", Description = "모든 몬스터의 방어력이 10% 감소합니다.", Job = "수강생" });
    //        skills.Add(new Skill { Name = "", Description = "방어력을 10% 증가시킵니다.", Job = "튜터" });
    //        skills.Add(new Skill { Name = "윙크", Description = "모든 몬스터의 (5~15)HP 가 감소됩니다.", Job = "튜터" });
    //        skills.Add(new Skill { Name = "", Description = "공격력과 방어력이 모두 20% 증가시킵니다.", Job = "매니저" });
    //        skills.Add(new Skill { Name = "", Description = "한 턴 동안 무적 상태가 됩니다", Job = "매니저" });
    //    }

    //}

}
