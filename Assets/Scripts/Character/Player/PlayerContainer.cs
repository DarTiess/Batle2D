using Character.Enemy;
using Character.Enemy.Loot;
using Infrastructure.Data;
using Infrastructure.Input;
using Infrastructure.Inventory;
using Infrastructure.Level.Interfaces;
using Infrastructure.Level.Interfaces.Events;
using Infrastructure.SaveLoad;
using UnityEngine;

namespace Character.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(HealthBar))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private ParticleSystem _bloodEffect;
        
        private const string PLAYERHEALTH = "PlayerHealth";
        private PlayerAnimator playerAnimator;
        private PlayerMovement move;
        private PlayerHealthData playerData;
        private HealthBar healthBar;
        private CircleCollider2D collider;
        private ILevelLost levelLost;
        private ILevelEvents levelEvent;
        private ISaveLoadService storageService;

        private IAttackEvent attackEvent;
        private IInventoryContainer inventoryContainer;
        private bool isDead;
        private int maxHealth;
        private int currentHealth=0;
        private bool canMove;
        private PlayerAttack playerAttack;
        private Transform enemyTarget;
        private IEnemyDestroy enemyDestroy;

        private void FixedUpdate()
        {
            if (isDead) return;
            if (canMove)
            {
                move.Move(); 
            }
            
        }

        public void Init(ILevelLost levManager,
                         ILevelEvents levelEvents,
                         IAttackEvent attackEvents, 
                         IInputService input, 
                         ISaveLoadService storage,
                         IInventoryContainer invContainer,
                         IEnemyDestroy enemyDestr,
                         float speedMove,
                         int health, Bullet bulletPrefab, int countBullet, int bulletPower)
        {
            levelLost = levManager;
            levelEvent = levelEvents;
            attackEvent = attackEvents;
            storageService = storage;
            inventoryContainer = invContainer;
            enemyDestroy = enemyDestr;
            
            levelEvent.OnLateWin += Win;
            attackEvent.AttackEnemy += AttackEnemy;
            enemyDestroy.DestroyEnemy += FreeTarget;
            
            playerAnimator = GetComponent<PlayerAnimator>();
            collider = GetComponent<CircleCollider2D>();
            playerData = new PlayerHealthData();
            playerAttack = new PlayerAttack(bulletPrefab, countBullet, _bulletPosition, transform, bulletPower);
            InitHealthBarParameters(health);

            move = GetComponent<PlayerMovement>();
            move.Init(input,playerAnimator, speedMove);

            canMove = true;
        }

        private void OnDisable()
        {
            levelEvent.OnLevelWin -= Win;
            attackEvent.AttackEnemy -= AttackEnemy;
            enemyDestroy.DestroyEnemy -= FreeTarget;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            SaveHealthData();
            healthBar.SetBadValues(damage);
            _bloodEffect.Play();
            playerAnimator.DamageAnimation();
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Enemy.Enemy enemy))
            {
                enemyTarget = enemy.transform;
                enemy.MoveToPlayer(transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out Loot loot))
            {
                 loot.Hide();
                inventoryContainer.SetNewLootToCollection(loot.LootType);
            }
        }

        private void FreeTarget()
        {
            enemyTarget = null;
            collider.enabled = false;
            collider.enabled = true;
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
            if (enemyTarget != null)
            {
                playerAttack.PushBullet(enemyTarget);
                playerAnimator.AttackAnimation();
            }
        }

        private void Win()
        {
            move.FinishGame();
            //playerAnimator.WinAnimation();
        }

        private void Death()
        {
            move.FinishGame();
            playerAnimator.LoseAnimation();
            levelLost.LevelLost();
        }
    }
}