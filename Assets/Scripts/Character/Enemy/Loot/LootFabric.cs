using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy.Loot
{
    public class LootFabric : ILootFabric
    {
        private List<Loot> lootPrefaList;
        private int countEnemy;
        private Queue<Loot> lootsQueu;
       
       
        public LootFabric(List<Loot> prefs, int count)
        {
            lootPrefaList = prefs;
            countEnemy = count;
            lootsQueu = new Queue<Loot>(countEnemy);
        }

        public Queue<Loot> CreateLootPool()
        {
            for (int i = 0; i < countEnemy; i++)
            {
                int rndLoot = Random.Range(0, lootPrefaList.Count);
                Loot loot = Object.Instantiate(lootPrefaList[rndLoot]);
                loot.Hide();
                lootsQueu.Enqueue(loot);
            }
            return lootsQueu;
        }
    }
}