using DG.Tweening;
using UnityEngine;

namespace Trees
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _graphics;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _loweringStep;
        [SerializeField] private float _sleepYPosition;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _movingDuration;
        [SerializeField] private Ease _ease;
        [SerializeField] private FollowingLogs _followingLogs;
        [SerializeField] private GameObject[] _logs;
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _cutSound;
        [SerializeField] private AudioClip _fullCutSound;
        private float _defaultYPosition;
        private int _index;

        public int LeftLogsAmount => _index;

        private void Awake()
        {
            _loweringStep *= _parent.localScale.y;
            _sleepYPosition *= _parent.localScale.y;

            _defaultYPosition = _graphics.position.y;
            ResetValues();
        }

        public void Cut()
        {
            _logs[_index].SetActive(false);
            _index--;

            ChangeYPosition(_graphics.position.y - _loweringStep);
            SleepIfEmpty();

            _followingLogs.Activate();
            _particle.Play();

            _source.pitch = Random.Range(0.90f, 1.10f);
            _source.PlayOneShot(_cutSound);
        }

        public void Restore()
        {
            foreach (var log in _logs)
            {
                log.SetActive(true);
            }

            ChangeYPosition(_defaultYPosition);
            ResetValues();
            _collider.enabled = true;
            _followingLogs.Clear();
        }

        private void SleepIfEmpty()
        {
            if (_index < 0)
            {
                ChangeYPosition(_sleepYPosition);
                _collider.enabled = false;
            }
        }

        private void ResetValues()
        {
            _index = _logs.Length - 1;
        }

        private void ChangeYPosition(float y)
        {
            _graphics.DOMoveY(y, _movingDuration).SetEase(_ease);
        }
    }
}