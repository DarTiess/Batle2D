using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemySpawner
    {
        private IEnemyFabric enemyFabric;
        private List<Enemy> enemiesList;

        private Vector3Int size;
       
        public EnemySpawner(IEnemyFabric fabric, int count, Vector3Int tileMapSize)
        {
            enemyFabric = fabric;
            enemiesList = new List<Enemy>(count);
            enemiesList = enemyFabric.CreateEnemyPool();
            size = tileMapSize;
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
            }
        }
    }
}