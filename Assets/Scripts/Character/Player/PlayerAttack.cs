using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    public class PlayerAttack
    {
        private Bullet ballPref;
        private int countBalls;
        private Transform pushBallPoint;
        private Transform parentTransform;
        private List<Bullet> ballList;
        private int indexBall = 0;
        private int attackPower;

        public PlayerAttack(Bullet ballPref, 
                            int countBalls, 
                            Transform pushBallPoint, 
                            Transform originParent,
                            int attackPower)
        {
            this.ballPref = ballPref;
            this.countBalls = countBalls;
            this.pushBallPoint = pushBallPoint;
            parentTransform = originParent;
            this.attackPower = attackPower;
            ballList = new List<Bullet>(this.countBalls);
            CreateBulletsList();
        }

        public void PushBullet(Transform target)
        {
            ballList[indexBall].Push(target, pushBallPoint);
            indexBall++;
            if (indexBall >= ballList.Count)
            {
                indexBall = 0;
            }
                              
        }

        private void CreateBulletsList()
        {
            for (int i = 0; i < countBalls; i++)
            {
                Bullet bullet = Object.Instantiate(ballPref, pushBallPoint.transform.position, pushBallPoint.transform.rotation);
                bullet.Init(parentTransform, attackPower);
                
                ballList.Add(bullet);
            }
        }
    }
}