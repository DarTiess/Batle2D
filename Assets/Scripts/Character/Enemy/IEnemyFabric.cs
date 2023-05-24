using System.Collections.Generic;

namespace Character.Enemy
{
    public interface IEnemyFabric
    {
        List<Enemy> CreateEnemyPool();
    }
}