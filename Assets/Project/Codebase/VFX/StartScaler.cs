using DG.Tweening;
using UnityEngine;

namespace VFX
{
    public class StartScaler : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;
        private Transform _transform;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _defaultScale = _transform.localScale;

            _transform.localScale = Vector3.zero;
        }

        private void OnEnable()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_transform.DOScale(_defaultScale, _duration).SetEase(_ease));
        }
    }
}