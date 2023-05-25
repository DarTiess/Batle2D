using System;
using System.Collections.Generic;
using Character.Enemy.Loot;
using Infrastructure.Level.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character.Enemy
{
    public class EnemySpawner : IEnemyDestroy
    {
        public event Action DestroyEnemy;
        private IEnemyFabric enemyFabric;
        private ILootFabric lootFabric;
        private List<Enemy> enemiesList;
        private Queue<Loot.Loot> lootsQueu;

        private Vector3Int size;
        private int countEnemy;
        private ILevelWin levelWin;

        public EnemySpawner(ILevelWin levelWinService,IEnemyFabric enemyFabric, ILootFabric lootFabric, 
                            int count, Vector3Int tileMapSize)
        {
            this.enemyFabric = enemyFabric;
            this.lootFabric = lootFabric;
            enemiesList = new List<Enemy>(count);
            countEnemy = count;
            levelWin = levelWinService;
            size = tileMapSize;
            
            enemiesList = this.enemyFabric.CreateEnemyPool();
            lootsQueu = this.lootFabric.CreateLootPool();
            
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (Enemy enemy in enemiesList)
            {
                int halfX = size.x / 2;
                int halfY = size.y / 2;
                
                int rndX = Random.Range(-halfX, halfX);
                int rndY = Random.Range(-halfY, halfY);

                enemy.PushEnemy(rndX, rndY);
                enemy.OnDead += EnemyDead;
            }
        }

        private void EnemyDead(Transform enemy)
        {
            countEnemy -= 1;
            SpawnLoot(enemy);
            DestroyEnemy?.Invoke();

            if (countEnemy <= 0)
            {
                levelWin.LevelWin();
            }
        }

        private void SpawnLoot(Transform enemyPosition)
        {
            lootsQueu.Dequeue().SetAndShow(enemyPosition);
        }
    }
}