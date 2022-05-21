using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomUserInput.ConcreteInputs
{
    public class HoldAndDrag : MonoBehaviour, IInputable, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float _horizontalDumpingCoefficient;
        private Vector2 _startPoint;

        public event UnityAction<Vector3> Inputting;
        public event UnityAction Started;
        public event UnityAction Stopped;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Started?.Invoke();
            _startPoint = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = eventData.position - _startPoint;
            var newVector = new Vector3(delta.x * _horizontalDumpingCoefficient, 0, delta.y);

            Inputting?.Invoke(newVector);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Inputting?.Invoke(Vector3.zero);
            Stopped?.Invoke();
        }
    }
}