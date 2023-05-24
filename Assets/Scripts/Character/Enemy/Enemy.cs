using System;
using System.Collections;
using UnityEngine;


namespace Character.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthBar))]
   
    public class Enemy: MonoBehaviour
    {
        private float moveSpeed;
        private Transform player;
        private bool canMove;
        private EnemyAnimator animator;
        private Rigidbody2D rigidbody;
        private bool isFacingRight;
        private HealthBar healthBar;
        private int hp;
        private bool _isDead;

        private void Update()
        {
            if(_isDead) return;
            if(!canMove)
                return;
            
            Move();
        }
        
        public void PushEnemy(int rndX, int rndY)
        {
            gameObject.transform.position = new Vector2(rndX, rndY);
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
            animator.IdleAnimation();
        }

        public void Init(float speed, int health)
        { 
            animator = GetComponent<EnemyAnimator>();
            rigidbody = GetComponent<Rigidbody2D>();
            healthBar = GetComponent<HealthBar>();
            moveSpeed = speed;
            hp = health;
            healthBar.Init(hp);
            Hide();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public void MoveToPlayer(Transform target)
        {
            player = target;
            canMove = true;
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
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<PlayerContainer>(out PlayerContainer player))
            {
                player.TakeDamage(1);
              
            }

            if (col.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
            {
                Debug.Log("Bullet");
               
                canMove = false;
                animator.DamageAnimation(); 
                HealthDamage(bullet);
                bullet.TryDestroy();
            }
        }

        private void HealthDamage(Bullet bullet)
        {
            hp -= bullet.Damage;
            healthBar.SetBadValues(bullet.Damage);
            if (hp <= 0)
            {
                _isDead = true;
                animator.Death();
            }
          
        }
        public void ContinueMove()
        {
            canMove = true;
        }

      
    }
}