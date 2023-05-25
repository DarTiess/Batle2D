using System.Collections.Generic;
using Character.Enemy.Loot;

namespace Infrastructure.Data
{
    public class InventoryData
    {
        public Dictionary<LootType, int> Loot { get; set; }
    }
}