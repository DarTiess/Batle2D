using System;
using Infrastructure.Input;
using Infrastructure.Level;
using SaveLoad;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(HealthBar))]
    public class PlayerContainer : MonoBehaviour
    {
        private const string PLAYERHEALTH = "PlayerHealth";
        private PlayerAnimator _playerAnimator;
        private PlayerMovement move;
        private PlayerHealthData playerData;
        private HealthBar healthBar;
        private IFinishLevel levelManager;
        private ISaveLoadService storageService;

        private IAttackEvent attackEvent;
        private bool _isDead;
        private int maxHealth;
        private int currentHealth=0;
        private bool canMove;

        public void Init(IFinishLevel levManager, 
                         IAttackEvent attackEvents, 
                         IInputService input, 
                         ISaveLoadService storage,
                         float speedMove,
                         int health)
        {
            levelManager = levManager;
            attackEvent = attackEvents;
            attackEvent.AttackEnemy += AttackEnemy;
            storageService = storage;
            _playerAnimator = GetComponent<PlayerAnimator>();
            playerData = new PlayerHealthData();
           
            InitHealthBarParameters(health);

            move = GetComponent<PlayerMovement>();
            move.Init(input,_playerAnimator, speedMove);
            canMove = true;
        }

        private void InitHealthBarParameters(int health)
        {
            healthBar = GetComponent<HealthBar>();
            maxHealth = health;
            storageService.Load<PlayerHealthData>(PLAYERHEALTH, data =>
            {
                currentHealth = data.Health;
            });
            if (currentHealth<=0 || currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                SaveHealthData();
            }

            healthBar.Init(maxHealth, currentHealth);
        }

        private void SaveHealthData()
        {
            playerData.Health = currentHealth;
            storageService.Save(PLAYERHEALTH, playerData);
        }

        private void AttackEnemy()
        {
            _playerAnimator.AttackAnimation();
           Debug.Log("Attack");
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            SaveHealthData();
            healthBar.SetBadValues(damage);
            if (currentHealth <= 0)
            {
                Death();
            }
            
        }

        private void FixedUpdate()
        {
            if (_isDead) return;
            if (canMove)
            {
                move.Move(); 
            }
            
        }
        
        private void Win()
        {
            move.FinishGame();
            _playerAnimator.WinAnimation();
            levelManager.LevelWin();
        }

        private void Death()
        {
            move.FinishGame();
            _playerAnimator.LoseAnimation();
            levelManager.LevelLost();
        }
    }
}