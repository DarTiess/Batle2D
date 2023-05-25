using System;

namespace Infrastructure.Level.Interfaces.Events
{
    public interface IAttackEvent
    {
        event Action AttackEnemy;
    }
}