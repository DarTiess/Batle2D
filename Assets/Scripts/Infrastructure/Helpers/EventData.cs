using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Helpers
{
    public class EventData : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        public Vector2 DataDelta, DataNormal;      
        public PointerEventData Data = new PointerEventData(null);
        public float Speed = 0.2f;
    
        private Vector2 _last = Vector2.zero;
        public static EventData Instance; 
    
        private void Awake()
        {
            Instance = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Data = eventData;
            DataNormal = eventData.delta.normalized;
            DataDelta.x = (eventData.delta.x) * Speed;
            DataDelta.y = (eventData.delta.y) * Speed;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DataNormal = eventData.delta.normalized;
            Data = eventData;
            DataDelta = eventData.delta.normalized * Speed; 
        }

        private void Update()
        {
            if (_last == DataDelta)
            {
                DataDelta = Vector2.zero;
                DataNormal = Vector2.zero;
            }                             
            _last = DataDelta;    
        }
    }
}
