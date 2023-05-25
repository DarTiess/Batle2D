using System;
using System.Threading.Tasks;
using Infrastructure.Level.Interfaces;
using Infrastructure.Level.Interfaces.Events;
using UnityEngine;

namespace Infrastructure.Level
{
    public class LevelService : ILevelService, ILevelEvents, IAttackEvent, ILevelLost, ILevelWin
    {
        public event Action OnLevelStart;
        public event Action OnLevelWin;
        public event Action OnLateWin;
        public event Action OnLevelLost;
        public event Action OnLateLost;
        public event Action OnPlayGame;
        public event Action StopGame;
        public event Action AttackEnemy;

        private float timeWaitLose;
        private float timeWaitWin;
        private bool onPaused;

        public LevelService(float timeWaitLose, float timeWaitWin)
        {
            this.timeWaitLose = timeWaitLose;
            this.timeWaitWin = timeWaitWin;
            LevelStart();
        }
        public void LevelStart()
        {
            Taptic.Success();
            OnLevelStart?.Invoke();
        }
        public void PauseGame()
        {
            if (!onPaused)
            {
                StopGame?.Invoke();
                onPaused = true;
            }
            else
            {
                PlayGame();
                onPaused = false;
            }
        }
        public void PlayGame()
        {
            OnPlayGame?.Invoke();
        }
        public void OnAttack()
        {
            AttackEnemy?.Invoke();
        }
        public void LevelLost()
        {
            Taptic.Failure();
            OnLevelLost?.Invoke();
            LateLost();
        }
        public void LevelWin()
        {
            Taptic.Success();
            OnLevelWin?.Invoke();
            LateWin();
        }
        private async void LateLost()
        {
            while (timeWaitLose>0)
            {
                timeWaitLose -= Time.deltaTime;
                await Task.Yield();
            }
            OnLateLost?.Invoke();
        }
        private async void LateWin()
        {
            while (timeWaitWin>0)
            {
                timeWaitWin -= Time.deltaTime;
                await Task.Yield();
            }
            OnLateWin?.Invoke();
        }

    }
}
