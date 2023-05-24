using Infrastructure.Input;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement: MonoBehaviour
    {
        private IInputService inputService;
        private IMoveAnimator moveAnimator;
        private Vector2 temp;
        private Rigidbody2D rigidbody;
        private float playerSpeed;
        private bool isFacingRight;

        public void Init(IInputService inputService, IMoveAnimator moveAnimator, float speedMove)
        {
            this.inputService=inputService;
            this.moveAnimator = moveAnimator;
            rigidbody = GetComponent<Rigidbody2D>();
            playerSpeed = speedMove;
        }
        public void Move()
        {
            float inputHorizontal = inputService.GetHorizontal;
            float inputVertical = inputService.GetVertical;
       
            temp.x = inputHorizontal;
            temp.y = inputVertical;
           
            moveAnimator.MoveAnimation(temp.magnitude);
            
           // rigidbody.transform.Translate(temp * Time.deltaTime * playerSpeed, Space.World);

            rigidbody.MovePosition(rigidbody.position + temp * playerSpeed * Time.fixedDeltaTime);
           
            if (inputHorizontal > 0 && isFacingRight)
            {
                Flip();
            }else if(inputHorizontal < 0 && !isFacingRight)
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
        

        public void FinishGame()
        {
            playerSpeed  = 0;
        }
    }
}