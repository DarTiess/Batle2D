using Character.Enemy.Loot;

namespace Infrastructure.Inventory
{
    public interface IInventoryContainer
    {
        void SetNewLootToCollection(LootType type);
    }
}