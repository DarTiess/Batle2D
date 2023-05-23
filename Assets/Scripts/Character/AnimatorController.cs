using UnityEngine;

namespace Character
{
    public class AnimatorController: MonoBehaviour, IMoveAnimator
    {
        private Animator animator;
        private static readonly int IS_MOVE = Animator.StringToHash("IsMove");
        private static readonly int WIN = Animator.StringToHash("Win");
        private static readonly int LOSE = Animator.StringToHash("Lose");
        private static readonly int ATTACK = Animator.StringToHash("Attack");

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void MoveAnimation(float speed)
        {
            animator.SetFloat(IS_MOVE,speed);  
        }

        public void WinAnimation()
        {
            animator.SetTrigger(WIN);
        }

        public void LoseAnimation()
        {
            animator.SetTrigger(LOSE);  
        }

        public void AttackAnimation()
        {
            animator.SetBool(ATTACK, true);
        }
    }
}