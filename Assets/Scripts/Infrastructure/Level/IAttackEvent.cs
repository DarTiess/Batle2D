using System;

namespace Infrastructure.Level
{
    public interface IAttackEvent
    {
        event Action AttackEnemy;
    }
}