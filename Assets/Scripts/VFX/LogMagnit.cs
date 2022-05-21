using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace VFX
{
    public class LogMagnit : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;
        [SerializeField] private Transform _transform;
        private Vector3 _defaultPosition;
        private Vector3 _defaultScale;
        private Quaternion _defaultRotation;

        private Transform _target;

        public event UnityAction Reached;

        private void Awake()
        {
            _defaultPosition = _transform.localPosition;
            _defaultRotation = _transform.rotation;
            _defaultScale = _transform.localScale;
        }

        public void Activate(Transform target)
        {
            _target = target;
            Clear();

            var randomRotation = new Vector3();
            randomRotation.y = Random.Range(0, 360);

            _transform.DORotate(randomRotation, _duration);

            var sequence = DOTween.Sequence();

            Vector3 randomDirection = new Vector3();
            randomDirection.x = Random.Range(2, 5);
            randomDirection.z = Random.Range(2, 5);

            sequence.Append(_transform.DOLocalMove(randomDirection, _duration).SetEase(_ease));
            sequence.AppendCallback(() => 
            {                
                StartCoroutine(Following());
                _transform.DOScale(0, _duration);
            });
        }

        private IEnumerator Following()
        {
            while (Vector3.Distance(_transform.position, _target.position) > 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _target.position, Time.deltaTime * 20);

                yield return null;
            }

            Reached?.Invoke();
            gameObject.SetActive(false);
        }

        private void Clear()
        {
            _transform.localPosition = _defaultPosition;
            _transform.localRotation = _defaultRotation;
            _transform.localScale = _defaultScale;
        }
    }
}