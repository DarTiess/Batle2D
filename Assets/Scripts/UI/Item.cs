using System;
using Character.Enemy.Loot;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Item: MonoBehaviour
    {
        public event Action<LootType> OnMinusItem;
        public event Action<LootType> OnDeleteItem;
        [SerializeField] private Button _mainButton;
        [FormerlySerializedAs("_count")]
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private LootType _type;
        [SerializeField] private Button _clearButton;

        private int count = 0;
        private bool isClickedBtn;
        private float buttonMovePosition;
        private float buttonMoveDuration;
        public LootType LootType => _type;

        public void Init(int value, float btnMoveDuration, float btnMovePos)
        {
            count = value;
            buttonMoveDuration = btnMoveDuration;
            buttonMovePosition = btnMovePos;
           
           _mainButton.onClick.AddListener(ClickedItemButton);
          
           _clearButton.gameObject.SetActive(false); 
           _clearButton.onClick.AddListener(DeleteItem);
        }

        public void Show()
        {
            CheckCount();
            _countText.text ="x"+ count;
            gameObject.SetActive(true);
        }

        public void AddCount()
        {
            count += 1;
            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void DeleteItem()
        {
            count -= 1;
            if (count <= 0)
            {
                OnDeleteItem?.Invoke(_type);
                Hide();
            }
            else
            {
                OnMinusItem?.Invoke(_type);
                CheckCount();
            }
        }

        private void ClickedItemButton()
        {
            if (!isClickedBtn)
            {
                _clearButton.transform.DOMoveX(_clearButton.transform.position.x - buttonMovePosition, buttonMoveDuration)
                            .OnStart(()=>
                            {
                                _clearButton.gameObject.SetActive(true);
                            });
                isClickedBtn = true;
            }
            else
            {
                _clearButton.transform.DOMoveX(_clearButton.transform.position.x + buttonMovePosition, buttonMoveDuration)
                            .OnComplete(()=>
                            {
                                _clearButton.gameObject.SetActive(false);
                            });
                isClickedBtn = false;
            }
        }

        private void CheckCount()
        {
            if (count <= 1)
            {
                _countText.gameObject.SetActive(false);
            }
            else
            {
                _countText.gameObject.SetActive(true);
            }
        }
    }
}