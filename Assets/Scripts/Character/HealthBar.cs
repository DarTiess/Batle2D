using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class HealthBar: MonoBehaviour
    {
        
        [SerializeField] private Slider _slider;
        private float valueProgress = 0;
        private Camera camera;

        private void Start()
        {
            camera = Camera.main;
        }
        private void Update()
        {
            _slider.transform.LookAt(transform.position + camera.transform.forward);
        }
        public void Init(float maxValues, float curValue)
        {
            _slider.maxValue = maxValues;
            valueProgress = curValue;
            _slider.value = curValue;
            
        } 
        public void Init(float value)
        {
            _slider.maxValue = value;
            valueProgress = value;
            _slider.value = value;
            
        }

        public void SetBadValues(float price)
        {
            valueProgress -= price;
            _slider.DOValue(valueProgress, 0.7f);
            if (valueProgress <= 0)
            {
                _slider.gameObject.SetActive(false);
            }
        }
    }
}