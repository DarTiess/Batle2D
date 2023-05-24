using System;
using UnityEngine;


namespace Character.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Enemy: MonoBehaviour
    {
        private float moveSpeed;
        private Transform player;
        private bool canMove;
        private EnemyAnimator animator;

        private void Update()
        {
            if(!canMove)
                return;
            
            Move();
        }
        
        public void PushEnemy(int rndX, int rndY, float speed)
        {
           
            gameObject.transform.position = new Vector2(rndX, rndY);
            moveSpeed = speed;
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
            animator.IdleAnimation();
        }

        public void Hide()
        { 
            animator = GetComponent<EnemyAnimator>();
            gameObject.SetActive(false);
        }

        private void MoveToPlayer(Transform target)
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<PlayerContainer>(out PlayerContainer player))
            {
                MoveToPlayer(player.transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<PlayerContainer>(out PlayerContainer player))
            {
                player.TakeDamage(1);
            }
        }
    }
}