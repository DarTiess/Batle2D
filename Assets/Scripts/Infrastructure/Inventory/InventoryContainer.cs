using System;
using System.Collections.Generic;
using Character.Enemy.Loot;
using Infrastructure.Data;
using Infrastructure.SaveLoad;

namespace Infrastructure.Inventory
{
    public class InventoryContainer : IInventoryContainer
    {
        public event Action<LootType> TakeLoot;
        private const string INVENTORY = "Inventory";
        private InventoryData inventoryData;
        private ISaveLoadService storageService;
        private Dictionary<LootType, int> lootsCollection;

        public InventoryContainer(ISaveLoadService storage)
        {
            inventoryData = new InventoryData();
            storageService = storage;
            lootsCollection = new Dictionary<LootType, int>();
            storageService.Load<InventoryData>(INVENTORY, data =>
            {
                lootsCollection = data.Loot;
            });
        }

        public void SetNewLootToCollection(LootType type)
        {
            if (lootsCollection.ContainsKey(type))
            {
                lootsCollection[type] += 1;
            }
            else
            {
                lootsCollection.Add(type,1);
            }

            SaveData();
            TakeLoot?.Invoke(type);
        }

        public Dictionary<LootType,int> GetLootCollection()
        {
            return lootsCollection;
        }

        public void MinusLootFromCollection(LootType type)
        {
            if (lootsCollection.ContainsKey(type))
            {
                lootsCollection[type] -= 1;
                if (lootsCollection[type] <= 0)
                {
                   DeleteLoot(type);
                }
            }
           
        }

        public void DeleteLoot(LootType type)
        {
            lootsCollection.Remove(type);
            CheckIsEmptyCollection();
        }

        private void SaveData()
        {
            inventoryData.Loot = lootsCollection;
            storageService.Save(INVENTORY, inventoryData);
        }

        private void CheckIsEmptyCollection()
        {
            if (lootsCollection.Count > 0)
            {
               SaveData();
            }
            else
            {
                storageService.Delete(INVENTORY);
            }
        }
    }
}