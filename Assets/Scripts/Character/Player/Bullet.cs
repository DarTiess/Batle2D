using DG.Tweening;
using UnityEngine;

namespace Character
{
    public class Bullet: MonoBehaviour
    {
        private int _damage;
        public int Damage => _damage;

        public void Init(Transform parentTransform, int attackPower)
        {
            SetParent(parentTransform);
            _damage = attackPower;
            Hide();
        }
        public void Push(Transform target, Transform pushBallPoint)
        {
            SetToPushPosition(pushBallPoint.position);
            Show();
            transform.DOMove(target.position,1).OnComplete(() =>
            {
                SetToPushPosition(target.transform.position);
                Hide();
            });
        }
        private void SetParent(Transform parentTransform)
        {
            transform.parent = parentTransform;
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void SetToPushPosition(Vector3 pushPosition)
        {
            transform.position = pushPosition;
        }

        public void TryDestroy()
        {
            DOTween.Kill(this);
            Hide();
        }
    }
}