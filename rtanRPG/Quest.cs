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
        private Player player;
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

        public Quest(Player player) 
        { 
            this.player = player;
        }

        public virtual void Reward(Player player) { }

        public virtual void CheckComplete(Player player) { }
        public virtual void HandleDead(Monster monster) { }
        public virtual void ClearDungeon(Player player) { }


    }

    public class DungeonClearQuest : Quest
    {
        public int RequiredClears { get; set; }
        public int CurrentClear {  get; set; }
        public DungeonClearQuest(int id, string questName, string description, string questReward, int requiredClears) : base(id, questName, description, questReward)
        {
            RequiredClears = requiredClears;
            CurrentClear = 0;
        }

        public override void ClearDungeon(Player player)
        {
            CurrentClear = dungeon.dungeongClearCount;
            CheckComplete(player);
        }

        public override void CheckComplete(Player player)
        {
            CurrentClear = dungeon.dungeongClearCount;

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
        public string targetItem { get; set; }

        public InventoryQuest(int id, string questName, string description, string questReward, string targetItem) : base(id, questName, description, questReward)
        {

            this.targetItem = "[E]" + targetItem;
        }

        public override void CheckComplete(Player player)
        {
            foreach(KeyValuePair<Item, int> i in player.inventory.inventory)
            {
                if(i.Key.name == targetItem && i.Value > 0 && player.inventory.equipList.Contains(i.Key))
                {
                    readyToClear = true;
                }
                
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
        public DifficultDungeon(int id, string questName, string description, string questReward) : base(id, questName, description, questReward)
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

        public HardMonsterCatch() : base(00, "basic", "basicDesc", "basicReward")
        {

        }
        public HardMonsterCatch(int id, string questName, string description, string questReward) : base(id, questName, description, questReward)
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

        public override void HandleDead(Monster monster)
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
