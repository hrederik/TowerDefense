using UnityEngine;
using VFX;

namespace Trees
{
    public class FollowingLogs : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private LogMagnit[] _logMagnit;
        [SerializeField] private Transform _target;
        private int _index;

        private void OnEnable()
        {
            foreach (var logMagnit in _logMagnit)
            {
                logMagnit.Reached += PlaySound;
            }
        }

        private void OnDisable()
        {
            foreach (var logMagnit in _logMagnit)
            {
                logMagnit.Reached -= PlaySound;
            }
        }

        public void Activate()
        {
            if (_index < _logMagnit.Length)
            {
                for (int i = 0; i < 3; i++)
                {
                    _logMagnit[_index].gameObject.SetActive(true);
                    _logMagnit[_index].Activate(_target);
                    _index++;
                }
            }
        }

        public void Clear()
        {
            _index = 0;
        }

        private void PlaySound()
        {
            _source.Play();
        }
    }
}