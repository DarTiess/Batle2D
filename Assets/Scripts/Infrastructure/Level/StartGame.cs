using UnityEngine;


namespace Infrastructure.Level
{
    public class StartGame : MonoBehaviour
    {
        public LevelLoader LevelLoader;
       
        private void Awake()
        {
            LevelLoader.StartGame();    
        }
        
    }
}
