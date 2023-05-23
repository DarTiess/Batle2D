using System;
using UnityEngine;

//using GameAnalyticsSDK;

namespace Infrastructure.Level
{
    public class LevelManager : ILevelManager, ILevelEvents, IAttackEvent, IFinishLevel
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
        private bool _onPaused;

        public LevelManager(float timeWaitLose, float timeWaitWin)
        {
            this.timeWaitLose = timeWaitLose;
            this.timeWaitWin = timeWaitWin;
            LevelStart();
        }
    
        public void LevelStart()
        {
            Taptic.Success();
            OnLevelStart?.Invoke();
                                       
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, LevelLoader.NumLevel);
        }

        public void PauseGame()
        {
            if (!_onPaused)
            {
                StopGame?.Invoke();
                _onPaused = true;
            }
            else
            {
                PlayGame();
                _onPaused = false;
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

            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail,LevelLoader.NumLevel);

            LateLost();
        }

        public void LevelWin()
        {
            Taptic.Success();
            OnLevelWin?.Invoke();

            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,LevelLoader.NumLevel); 

            LateWin();
        }

        private void LateLost()
        {
            while (timeWaitLose>0)
            {
                timeWaitLose -= Time.deltaTime;
            }
            OnLateLost?.Invoke();
        }

        private void LateWin()
        {
            while (timeWaitWin>0)
            {
                timeWaitWin -= Time.deltaTime;
            }
            OnLateWin?.Invoke();
        }

    }
}
