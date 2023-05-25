using UnityEngine;

namespace Character.Enemy.Loot
{
    public class Loot: MonoBehaviour
    {
        [SerializeField] private LootType lootType;

        public LootType LootType => lootType;
        public void SetAndShow(Transform enemyPosition)
        {
            gameObject.transform.position = enemyPosition.position;
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}