using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyFabric : IEnemyFabric
    {
        private Enemy enemyPrefab;
        private int countEnemy;
        private List<Enemy> enemiesList;
        private float speedMove;
        private int health;

        public EnemyFabric(Enemy enemyPref, int count, float speed, int hp )
        {
            enemyPrefab = enemyPref;
            countEnemy = count;
            speedMove = speed;
            health = hp;
            enemiesList = new List<Enemy>(countEnemy);
        }

        public List<Enemy> CreateEnemyPool()
        {
            for (int i = 0; i < countEnemy; i++)
            {
                Enemy enemy = Object.Instantiate(enemyPrefab);
                enemy.Init(speedMove, health);
                enemiesList.Add(enemy);
            }
            return enemiesList;
        }
    }
}