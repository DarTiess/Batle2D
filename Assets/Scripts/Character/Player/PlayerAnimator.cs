using UnityEngine;

namespace Character.Player
{
    public class PlayerAnimator: MonoBehaviour, IMoveAnimator
    {
        private Animator animator;
        private static readonly int IS_MOVE = Animator.StringToHash("IsMove");
        private static readonly int WIN = Animator.StringToHash("Win");
        private static readonly int DEATH = Animator.StringToHash("Death");
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        private static readonly int DAMAGE = Animator.StringToHash("Damage");

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void MoveAnimation(float speed)
        {
            animator.SetFloat(IS_MOVE,speed);  
            animator.SetBool(ATTACK,false);
        }
      

        public void WinAnimation()
        {
            animator.SetTrigger(WIN);
        }

        public void LoseAnimation()
        {
            animator.SetBool(DEATH, true);  
        }

        public void AttackAnimation()
        {
            animator.SetBool(ATTACK, true);
        }

        public void FinishDamageAnimation()
        {
            animator.SetBool(DAMAGE, false);
        }

        public void DamageAnimation()
        {
            animator.SetBool(DAMAGE,true);
        }
    }
}