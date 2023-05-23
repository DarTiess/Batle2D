using CamFollow;
using Character;
using Infrastructure.Level;
using Infrastructure.Input;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private PlayerContainer _playerPrefab;
        [FormerlySerializedAs("_canvasPrefab")]
        [Header("UI")]
        [SerializeField] private UIControl _uiPrefab;

        private LevelManager levelManager;
        private IInputService inputService;
        private UIControl _ui;
        private PlayerContainer character;
        private CamFollower camera; 
        private void Awake()
        {
            levelManager = new LevelManager(_timeLose, _timeWin);
            CreateAndInitUI();
            inputService = InputService();
            CreateAndInitPlayer();
            CreateAndInitCamera();
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
            character.Init(levelManager, levelManager, inputService, _playerSpeed, _rotationSpeed);
        }

        private void CreateAndInitCamera()
        {
            camera = Camera.main.GetComponent<CamFollower>();
            camera.Init(levelManager, character.transform);
        }
    }
}