using Infrastructure.Input;
using Infrastructure.Level;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(AnimatorController))]
    public class PlayerContainer : MonoBehaviour
    {
        private AnimatorController animator;
        private PlayerMovement move;
        private IFinishLevel levelManager;

        private IAttackEvent attackEvent;
        private bool _isDead;

        public void Init(IFinishLevel levManager, IAttackEvent attackEvent, IInputService input, float speedMove, float speedRotate)
        {
            levelManager = levManager;
            this.attackEvent = attackEvent;
            this.attackEvent.AttackEnemy += AttackEnemy;
            animator = GetComponent<AnimatorController>(); 
            move = GetComponent<PlayerMovement>();
            move.Init(input,animator, speedMove, speedRotate);
        }

        private void AttackEnemy()
        {
            animator.AttackAnimation();
           Debug.Log("Attack");
        }

        private void FixedUpdate()
        {
            if (_isDead) return;
        
            move.Move();
        
           // if (transform.position.y < -2) levelManager.LevelLost();
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Win"))
            {
                move.FinishGame();
                animator.WinAnimation(); 
                levelManager.LevelWin();
            }
            if (other.gameObject.CompareTag("Lose"))
            {
                move.FinishGame();
                animator.LoseAnimation();
                levelManager.LevelLost();
            }
        

        }
    }
}