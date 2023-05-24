using CamFollow;
using Character;
using Character.Enemy;
using Infrastructure.Level;
using Infrastructure.Input;
using SaveLoad;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private float _timeWin;
        [SerializeField] private float _timeLose;
        [Header("Player Settings")]
        [SerializeField] private float _playerSpeed;
        [SerializeField] private int _maxHealth;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private PlayerContainer _playerPrefab;
        [FormerlySerializedAs("_canvasPrefab")]
        [Header("UI")]
        [SerializeField] private UIControl _uiPrefab;
        [Header("Enemy Settings")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _enemyCount;
        [SerializeField] private float _enemySpeed;
        [Header("TileMap")]
        [SerializeField] private Tilemap _tileMapBase;

        private LevelManager levelManager;
        private IInputService inputService;
        private ISaveLoadService storageService;
        private UIControl _ui;
        private PlayerContainer character;
        private CamFollower camera;
        private IEnemyFabric enemyFabric;
        private EnemySpawner enemySpawner;
        private void Awake()
        {
            levelManager = new LevelManager(_timeLose, _timeWin);
            storageService = new JsonToFileStorageService();
            _levelLoader.Init(storageService);
            CreateAndInitUI();
            inputService = InputService();
            CreateAndInitPlayer();
            CreateAndInitCamera();

            enemyFabric = new EnemyFabric(_enemyPrefab, _enemyCount);
            Vector3Int size = _tileMapBase.size;
            enemySpawner = new EnemySpawner(enemyFabric, _enemyCount, size, _enemySpeed);
        }

        private void CreateAndInitUI()
        {
            _ui = Instantiate(_uiPrefab);
            _ui.Init(levelManager, levelManager, _levelLoader);
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }

        private void CreateAndInitPlayer()
        {
            character = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
            character.Init(levelManager, levelManager, inputService, storageService, _playerSpeed, _maxHealth);
        }

        private void CreateAndInitCamera()
        {
            camera = Camera.main.GetComponent<CamFollower>();
            camera.Init(levelManager, character.transform);
        }
    }
}