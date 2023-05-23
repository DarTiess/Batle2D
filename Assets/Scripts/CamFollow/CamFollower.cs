using System;
using Infrastructure.Level;
using UnityEngine;

namespace CamFollow {
    public class CamFollower : MonoBehaviour
    {
        [SerializeField] private TypeCam _typeCam = TypeCam.NotWork;
        [Space][Header("Follower")] 
        [SerializeField] private float _distance = 2;
        [SerializeField] private float _height = 2;
        [SerializeField] private float _heightDamping = 2;
        [SerializeField] private float _rotationDamping = 0.6f;

        [Space][Header("Vector")] 
        [SerializeField] private float _speedVector;
        [SerializeField] private float _vectorY = 10;
        [SerializeField] private float _vectorZ = 10;
        [SerializeField] private float _vectorX;
        [SerializeField] private bool _vectorXFrom0;
        [SerializeField] private bool _lookAtTarget;

        [Space][Header("Point")]
        [SerializeField] private float _speedMovePoint;
        [SerializeField] private float _speedRotatePoint;

        [Space][Space]  
        [SerializeField] private bool _dropTarget;
        [SerializeField] private ParticleSystem _particleWin;

        private Transform target;
        private Vector3 _temp;

        private LevelManager levelManager;

        public void Init(LevelManager levManager, Transform player)
        {
            levelManager = levManager;
            target = player;
            levelManager.OnLevelWin += OnLevelWin;
            levelManager.OnLevelLost += OnLevelLost;
        } 
    
        private void OnLevelWin()               
        {
            if (_particleWin){_particleWin.Play();}
        }
        private void OnLevelLost()
        {
            SetStop();
        }
        private void FixedUpdate()   
        {                 
            if (!target) return;
       
            switch (_typeCam)
            {
                case TypeCam.NotWork: return;
                case TypeCam.Follower: MoveFollow(); break;
                case TypeCam.Vector: MoveVector();   break;
                case TypeCam.MovePoint: MovePoint(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

       private void MoveVector()
        {
            _temp = target.position;
            _temp.y += _vectorY;
            _temp.z -= _vectorZ;
            _temp.x = _vectorXFrom0 ? _vectorX : _temp.x + _vectorX ;   
                                  
            transform.position = Vector3.Lerp(transform.position,_temp,_speedVector * Time.deltaTime);
            if (_lookAtTarget) transform.LookAt(target.position); 
        }
        
        private void MoveFollow()    
        {

            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + _height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
       
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);
       
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
       
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * _distance;  
       
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);    

            transform.LookAt(target);     
        }

        private void MovePoint()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speedMovePoint * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, _speedRotatePoint * Time.deltaTime);
        }

        public void SetStop()
        {
            if(!_dropTarget) return;  
            target = null;  
        }

    }
}