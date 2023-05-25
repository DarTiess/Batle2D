using System;

namespace Infrastructure.Level.Interfaces.Events
{
    public interface ILevelEvents
    {
        event Action OnLevelStart;
        event Action OnLevelWin;
        event Action OnLateWin;
        event Action OnLevelLost;
        event Action OnLateLost;
        event Action OnPlayGame;
        event Action StopGame;
    }
}