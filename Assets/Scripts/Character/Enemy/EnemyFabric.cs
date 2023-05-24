using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyFabric : IEnemyFabric
    {
        private Enemy enemyPrefab;
        private int countEnemy;
        private List<Enemy> enemiesList;

        public EnemyFabric(Enemy enemyPref, int count)
        {
            enemyPrefab = enemyPref;
            countEnemy = count;
            enemiesList = new List<Enemy>(countEnemy);
        }

        public List<Enemy> CreateEnemyPool()
        {
            for (int i = 0; i < countEnemy; i++)
            {
                Enemy enemy = Object.Instantiate(enemyPrefab);
                enemy.Hide();
                enemiesList.Add(enemy);

            }

            return enemiesList;
        }
    }
}