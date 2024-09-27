//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace rtanRPG
//{
//    internal class Younjin
//    {
//        public void ShowAttackUI(Monster[] MonsterQueue, Player player, int monsterhp)
//        {
//            Console.WriteLine("Battle!!");
//            Console.WriteLine();
//            for (int i = 0; i < MonsterQueue.Length; i++)
//            {
//                bool isDead = MonsterQueue[i].HP <= 0;
//                string monsterHp = isDead ? "Dead" : MonsterQueue[i].HP.ToString();

//                if (isDead) // 회색으로 
//                {
//                    Console.ForegroundColor = ConsoleColor.Gray; 
//                }

//                Console.WriteLine($"Lv.{i + 1} {MonsterQueue[i].level} {MonsterQueue[i].Name}  HP {monsterHp}");
//            }
//            Console.WriteLine("\n");
//            Console.WriteLine($"[내정보]\nLv.{player.level}  {player.name} ({player.role})");
//            Console.WriteLine($"HP 100/{player.HP}");
//            Console.WriteLine($"\n0. 취소\n\n대상을 선택해주세요.");
//            Console.Write(">>");

//            while (true)
//            {
//                string input = Console.ReadLine();

//                if (!int.TryParse(input, out int inputIDX))
//                { Console.WriteLine("잘못된 입력입니다."); continue; }

//                if (input == "0")
//                {
//                    // ShowBattleUI();
//                    break;
//                }

//                // 선택한 몬스터에 공격 함수 실행
//                if (inputIDX >= 1 && inputIDX <= MonsterQueue.Length)
//                {
//                    int selectedMonsterIndex = inputIDX - 1;
//                    PlayerAttack(MonsterQueue, selectedMonsterIndex, player);

//                    break;

//                }

//                else { Console.WriteLine("잘못된 입력입니다."); }
//            }
//        }

//        // 공격 후 리턴값 출력 UI
//        public void ReturnAttackUI(Monster[] MonsterQueue, Player player, int finalDamage)
//        {
//            Console.WriteLine("Battle!!");
//            Console.WriteLine();
//            for (int i = 0; i < MonsterQueue.Length; i++)
//            {
//                Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].Name} 을(를) 맞췄습니다. [데미지: {finalDamage}]");
//                Console.WriteLine("\n");
//                Console.WriteLine($"Lv.{MonsterQueue[i].level} {MonsterQueue[i].Name}");

//                Console.WriteLine($"HP {MonsterQueue[i].HP} -> {MonsterQueue[i].HP - finalDamage}");
//            }
//            Console.WriteLine($"\n0. 다음\n\n");
//            Console.Write(">>");

//            while (true)
//            {
//                string input = Console.ReadLine();

//                if (!int.TryParse(input, out int inputIDX))
//                { Console.WriteLine("잘못된 입력입니다."); continue; }

//                if (input == "0")
//                {
//                    // 몬스터 공격 로직();
//                    break;
//                }

//                else { Console.WriteLine("잘못된 입력입니다."); }

//            }
//        }

//        // 플레이어 > 몬스터 공격 함수
//        public void PlayerAttack(Monster[] MonsterQueue, int selectedMonsterIndex, Player player)
//        {
//            // 공격력의 10% 계산
//            float damage = (int)player.ATK * 0.1f;
//            // 소수점 올림 처리
//            float damgeError = (float)Math.Ceiling(damage);
//            // 최소 및 최대 공격력 계산
//            float minDamage = damage - damgeError;
//            float maxDamage = damage + damgeError;

//            // 랜덤 공격력 생성
//            Random random = new Random();
//            int finalDamage = random.Next((int)minDamage, (int)(maxDamage + 1));

//            MonsterQueue[selectedMonsterIndex].HP -= finalDamage;
//        }
//    }
//}
