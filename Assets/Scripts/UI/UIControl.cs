using Infrastructure.Level;
using UI.UIPanels;
using UnityEngine;

namespace UI
{
    public class UIControl : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private StartMenu _panelMenu;
        [SerializeField] private GamePanel _panelInGame;
        [SerializeField] private WinPanel _panelWin;  
        [SerializeField] private LostPanel _panelLost;

        private ILevelManager levelManager;
        private ILevelEvents levelEvents;
        private ILevelLoader levelLoader;
        public void Init(ILevelManager levManager, ILevelEvents levelEvents, ILevelLoader levelLoader)
        {
            levelManager = levManager;
            this.levelEvents = levelEvents;
            this.levelLoader = levelLoader;
     
            this.levelEvents.OnLevelStart += OnLevelStart;
            this.levelEvents.OnLateWin += OnLevelWin; 
            this.levelEvents.OnLateLost += OnLevelLost;

            _panelMenu.ClickedPanel += OnPlayGame;
            _panelInGame.AttackButtonClicked += OnAttack;
            _panelLost.ClickedPanel += RestartGame;
            _panelInGame.ClickedPanel += OnPauseGame;
            _panelWin.ClickedPanel += LoadNextLevel;
            OnLevelStart();
        }

        private void OnAttack()
        {
            levelManager.OnAttack();
           
        }

        private void OnDisable()
        {
            levelEvents.OnLevelStart -= OnLevelStart;
            levelEvents.OnLateWin -= OnLevelWin; 
            levelEvents.OnLateLost -= OnLevelLost;

            _panelMenu.ClickedPanel -= OnPlayGame;
            _panelLost.ClickedPanel -= RestartGame;
            _panelInGame.ClickedPanel -= OnPauseGame;
            _panelWin.ClickedPanel -= LoadNextLevel;
        }

        private void OnLevelStart()      
        {   
            HideAllPanels();
            _panelMenu.Show();
        }
                             
        private void OnLevelWin()      
        {    
            Debug.Log("Level Win"); 
            HideAllPanels();
            _panelWin.Show();  
        }

        private void OnLevelLost()           
        {                                                     
            Debug.Log("Level Lost");  
            HideAllPanels();
            _panelLost.Show();
        }
        private void OnPauseGame()
        {
            levelManager.PauseGame();
        }

        private void OnPlayGame()
        { 
            levelManager.PlayGame();
            HideAllPanels(); 
            _panelInGame.Show();         
        }
        private void LoadNextLevel()
        {
            levelLoader.LoadNextLevel();
        }

        private void RestartGame()
        {
            levelLoader.RestartScene();
        }

        private void HideAllPanels()
        {
            _panelMenu.Hide();
            _panelLost.Hide();
            _panelWin.Hide();
            _panelInGame.Hide();
        }
    
    }
}
