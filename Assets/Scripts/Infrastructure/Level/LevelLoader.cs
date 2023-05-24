using System.Collections.Generic;
using SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Level
{
    [CreateAssetMenu(fileName = "LevelLoader", menuName = "LevelLoader", order = 51)]
    public class LevelLoader : ScriptableObject, ILevelLoader
    {
        private const string NUMSCENE = "NumScene";
        public List<string> NameScene;

        private ISaveLoadService storageService;
        private SceneStorage sceneData;
        private int sceneNum=0;

        public void StartGame()
        {
            Debug.Log("Load Scene Number " + sceneNum );

            SceneManager.LoadScene(NameScene[sceneNum]); 
        }
       public void Init(ISaveLoadService storage)
        {
            storageService = storage;
            sceneData = new SceneStorage();
            storageService.Load<SceneStorage>(NUMSCENE, data =>
            {
                sceneNum = data.IntScene;
                
            });
          
           SaveSceneData();
        }

        public void LoadNextLevel()
        {
            LoadScene();           
        }

        private void LoadScene()
        {
            if (sceneNum >= NameScene.Count)
            {
                sceneNum = 0;
            }
            else
            {
                SaveSceneData();
            }
            Debug.Log("Load Scene Number " + sceneNum );

            SceneManager.LoadScene(NameScene[sceneNum]); 
            sceneNum += 1;
        }

        private void SaveSceneData()
        {
            sceneData.SceneNumber = NameScene[sceneNum];
            sceneData.IntScene = sceneNum;
            storageService.Save(NUMSCENE, sceneData);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


