using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public class Quest
    {
        public int Id { get; }
        public string QuestName { get; }
        public string Description { get; }
        public string QuestReward { get; }
        public bool isAccepted { get; set; }
        public bool isCleared { get; set; }
        public bool readyToClear { get; set; }
        public Quest(int id, string questName, string description, string questReward)
        {
            Id = id;
            QuestName = questName;
            Description = description;
            QuestReward = questReward;
            isAccepted = false;
            isCleared = false;
            readyToClear = false;
        }

        public virtual void Reward(Player player) { }

        public virtual void CheckComplete(Player player) { }
    }

    public class DungeonClearQuest : Quest
    {
        public int RequiredClears { get; set; }
        public int CurrentClear { get; set; }

        public DungeonClearQuest(int id, string questName, string description, string questReward, int requiredClears) : base(id, questName, description, questReward)
        {
            RequiredClears = requiredClears;
            CurrentClear = 0;
        }

        public void ClearDungeon(Player player)
        {
            CurrentClear++;
            CheckComplete(player);
        }

        public override void CheckComplete(Player player)
        {
            if (CurrentClear >= RequiredClears)
            {
                CurrentClear = RequiredClears;
                readyToClear = true;

            }
        }

        public override void Reward(Player player)
        {
            player.gold += 1000;
        }

    }

    public class InventoryQuest : Quest
    {
        public Item targetItem { get; set; }

        public InventoryQuest(int id, string questName, string description, string questReward, Item targetItem) : base(id, questName, questReward, questName)
        {

            this.targetItem = targetItem;
        }

        public override void CheckComplete(Player player)
        {
            if (player.inventory.equipList.Contains(targetItem))
            {
                readyToClear = true;
            }
        }
        public override void Reward(Player player)
        {
            player.gold += 1000;
            var item = player.inventory.inventory.First(x => x.Key.name == "스파르타의 창").Key;
            player.inventory.inventory[item] += 1;
        }

    }

    public class DifficultDungeon : Quest
    {
        bool isClearDifficult;
        public DifficultDungeon(int id, string questName, string description, string questReward) : base(id, questName, questReward, questName)
        {
            this.isClearDifficult = false;
        }

        public override void CheckComplete(Player player)
        {
            if (isClearDifficult)
            {
                readyToClear = true;
            }
        }
        public override void Reward(Player player)
        {
            player.gold += 5000;
        }

    }

    public class HardMonsterCatch : Quest
    {
        public bool isKimCatch = false;
        public bool isGangCatch = false;
        public bool isAnCatch = false;

        public HardMonsterCatch(int id, string questName, string description, string questReward) : base(id, questName, questReward, questName)
        {

        }

        public override void CheckComplete(Player player)
        {
            if (isKimCatch == true && isAnCatch == true && !isGangCatch == true)
            {
                readyToClear = true;
            }
        }

        public override void Reward(Player player)
        {
            player.gold += 10000;
        }

        public void HandleDead(Monster monster)
        {
            if (MonsterPreset.HardMonster.Contains(monster))
            {
                switch (monster.name)
                {
                    case "김록기(대장)":
                        isKimCatch = true;
                        break;
                    case "안혜린(대장)":
                        isAnCatch = true; break;
                    case "강채린(대장)":
                        isGangCatch = true; break;
                }
            }
        }
    }
}
