using System;
using Character.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(HealthBar))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Enemy: MonoBehaviour
    {
        public event Action<Transform> OnDead;
      
        [FormerlySerializedAs("explosionEffect")]
        [SerializeField] private ParticleSystem _explosionEffect;
      
        private float moveSpeed;
        private Transform player;
        private bool canMove;
        private EnemyAnimator animator;
        private CapsuleCollider2D collider;
        private bool isFacingRight;
        private HealthBar healthBar;
        private int hp;
        private bool isDead;

        private void Update()
        {
            if(isDead)
            {
                return;
            }

            if(!canMove)
            {
                return;
            }

            Move();
        }

        public void Init(float speed, int health)
        { 
            animator = GetComponent<EnemyAnimator>();
            healthBar = GetComponent<HealthBar>();
            collider = GetComponent<CapsuleCollider2D>();
            moveSpeed = speed;
            hp = health;
            healthBar.Init(hp);
            Hide();
        }

        public void PushEnemy(int rndX, int rndY)
        {
            gameObject.transform.position = new Vector2(rndX, rndY);
            Show();
        }

        public void MoveToPlayer(Transform target)
        {
            player = target;
            canMove = true;
        }

        public void ContinueMove()
        {
            canMove = true;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out PlayerContainer player))
            {
                player.TakeDamage(1);
                canMove = false;
                animator.AttackAnimation();
            }

            if (col.gameObject.TryGetComponent(out Bullet bullet))
            {
                canMove = false;
                animator.DamageAnimation();
                _explosionEffect.transform.position = bullet.transform.position;
                _explosionEffect.Play();
                HealthDamage(bullet);
                bullet.TryDestroy();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
            animator.IdleAnimation();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Move()
        {
            animator.MoveAnimation();
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
           
            RotateToTarget();
        }

        private void RotateToTarget()
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            if (direction.x > 0 && isFacingRight)
            {
                Flip();
            }else if(direction.x < 0 && !isFacingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void HealthDamage(Bullet bullet)
        {
            hp -= bullet.Damage;
            healthBar.SetBadValues(bullet.Damage);
            if (hp <= 0 && !isDead)
            {
                isDead = true;
                animator.Death();
                collider.enabled = false;
                OnDead?.Invoke(transform);
            }
        }
    }
}