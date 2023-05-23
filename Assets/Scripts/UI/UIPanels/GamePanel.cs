using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class GamePanel: PanelBase
    {
        public override event Action ClickedPanel;
        public event Action AttackButtonClicked; 
        [SerializeField] private Button _attackButton;

        protected override void Start()
        {
            base.Start();
            _attackButton.onClick.AddListener(OnclickAttackButton);
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