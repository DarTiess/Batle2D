using System;
using System.Collections.Generic;
using Character.Enemy.Loot;
using Infrastructure.Inventory;
using Infrastructure.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class GamePanel: PanelBase
    {
        public override event Action ClickedPanel;
        public event Action AttackButtonClicked; 
      
        [SerializeField] private Button _attackButton;
        [SerializeField] private List<Item> itemsList;
        [SerializeField] private float _buttonMoveDuration;
        [SerializeField] private float _buttonMovePosition;
        
        private InventoryContainer intventory;
        private Dictionary<LootType, int> lootCollection = new Dictionary<LootType, int>();
        private ISaveLoadService storageService;

        protected override void Start()
        {
            base.Start();
            _attackButton.onClick.AddListener(OnclickAttackButton);
        }

        public void Init(InventoryContainer inventory)
        {
            intventory = inventory;
            intventory.TakeLoot += AddLoot;


            lootCollection= intventory.GetLootCollection();
            foreach (var item in itemsList)
            {
                if (lootCollection.ContainsKey(item.LootType))
                {
                    item.Init(lootCollection[item.LootType], _buttonMoveDuration, _buttonMovePosition);
                    item.Show();
                    item.OnMinusItem += MinusItemFromCollection;
                    item.OnDeleteItem += DeleteItem;
                }
                else
                {
                    item.Init(0,_buttonMoveDuration,_buttonMovePosition);
                    item.Hide();
                }
            }
        }

        private void DeleteItem(LootType type)
        {
            lootCollection.Remove(type);
            intventory.DeleteLoot(type);
        }

        private void MinusItemFromCollection(LootType type)
        {
            lootCollection[type] -= 1;
            intventory.MinusLootFromCollection(type);
        }

        private void AddLoot(LootType type)
        {
            foreach (var item in itemsList)
            {
                if (item.LootType == type)
                {
                    item.AddCount();
                }
            }
        }

        private void OnclickAttackButton()
        {
            AttackButtonClicked?.Invoke();
        }

        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }
    }
}