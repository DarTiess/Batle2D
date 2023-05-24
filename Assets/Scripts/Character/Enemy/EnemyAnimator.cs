using UnityEngine;

namespace Character.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator: MonoBehaviour
    {
        private Animator animator;
        private static readonly int WALK = Animator.StringToHash("walk");
        private static readonly int IDLE = Animator.StringToHash("Idle");
        private static readonly int DAMAGE = Animator.StringToHash("damage");
        private static readonly int DEATH = Animator.StringToHash("death");

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void MoveAnimation()
        {
            animator.SetBool(WALK, true);
            animator.SetFloat(IDLE,0);
            animator.SetBool(DAMAGE, false);
        }

        public void IdleAnimation()
        {
            int rndAnimation = Random.Range(1,3);
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
            animator.SetBool(WALK, false);
            animator.SetFloat(IDLE,rndAnimation);
            animator.SetBool(DAMAGE, false);
        }

        public void DamageAnimation()
        {
            animator.SetBool(WALK, false);
            animator.SetBool(DAMAGE, true);
        }

        public void Death()
        {
            animator.SetBool(WALK, false);
            animator.SetBool(DAMAGE, false);
            animator.SetTrigger(DEATH);
        }
    }
}