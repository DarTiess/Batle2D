using System;
using UnityEngine;

namespace UI.UIPanels
{
    public class StartMenu : PanelBase
    {
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
            Debug.Log("Clicked Start win");
           ClickedPanel?.Invoke();
        }
    }
}