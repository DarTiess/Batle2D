using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemySpawner
    {
        private IEnemyFabric enemyFabric;
        private List<Enemy> enemiesList;

        private Vector3Int size;
        private float moveSpeed;

        public EnemySpawner(IEnemyFabric fabric, int count, Vector3Int tileMapSize, float speed)
        {
            enemyFabric = fabric;
            enemiesList = new List<Enemy>(count);
            enemiesList = enemyFabric.CreateEnemyPool();
            size = tileMapSize;
            moveSpeed = speed;
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

                enemy.PushEnemy(rndX, rndY, moveSpeed);
            }
        }
    }
}