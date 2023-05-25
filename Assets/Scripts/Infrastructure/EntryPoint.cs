using System.Collections.Generic;
using CamFollow;
using Character.Enemy;
using Character.Enemy.Loot;
using Character.Player;
using Infrastructure.Level;
using Infrastructure.Input;
using Infrastructure.Inventory;
using Infrastructure.SaveLoad;
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
        [Header("Bullet Settings")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _countBullets;
        [SerializeField] private int _bulletPower;
        [FormerlySerializedAs("_canvasPrefab")]
        [Header("UI")]
        [SerializeField] private UIControl _uiPrefab;
        [Header("Enemy Settings")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _enemyCount;
        [SerializeField] private float _enemySpeed;
        [SerializeField] private int _enemyHealth;
        [Header("Loots")]
        [SerializeField] private List<Loot> _lootsPrefabsList; 
        [Header("TileMap")]
        [SerializeField] private Tilemap _tileMapBase;

        private LevelService levelService;
        private IInputService inputService;
        private ISaveLoadService storageService;
        private UIControl ui;
        private PlayerContainer character;
        private CamFollower camera;
        private IEnemyFabric enemyFabric;
        private ILootFabric lootFabric;
        private EnemySpawner enemySpawner;
        private InventoryContainer inventoryContainer;

        private void Awake()
        {
            levelService = new LevelService(_timeLose, _timeWin);
            storageService = new JsonToFileStorageService();
            inventoryContainer = new InventoryContainer(storageService);
            _levelLoader.Init(storageService);
            CreateAndInitUI();
            inputService = InputService();
            CreateAndSpawnEnemies();
            CreateAndInitPlayer();
            CreateAndInitCamera();
           
        }

        private void CreateAndSpawnEnemies()
        {
            enemyFabric = new EnemyFabric(_enemyPrefab, _enemyCount, _enemySpeed, _enemyHealth);
            lootFabric = new LootFabric(_lootsPrefabsList, _enemyCount);
            Vector3Int size = _tileMapBase.size;
            enemySpawner = new EnemySpawner(levelService, enemyFabric,lootFabric, _enemyCount, size);
        }

        private void CreateAndInitUI()
        {
            ui = Instantiate(_uiPrefab);
            ui.Init(levelService, levelService, _levelLoader, inventoryContainer);
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
            character.Init(levelService,levelService, levelService, inputService, storageService, inventoryContainer, enemySpawner,
                           _playerSpeed, _maxHealth, _bulletPrefab, _countBullets, _bulletPower);
        }

        private void CreateAndInitCamera()
        {
            camera = Camera.main.GetComponent<CamFollower>();
            camera.Init(levelService, character.transform);
        }
    }
}