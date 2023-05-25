using System.Collections.Generic;

namespace Character.Enemy.Loot
{
    public interface ILootFabric
    {
        Queue<Loot> CreateLootPool();
    }
}