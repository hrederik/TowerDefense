using UnityEngine;

namespace Game.Towers
{
    public class Blinking : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Launch()
        {
            _animator.SetTrigger("Launch");
        }

        public void Stop()
        {
            _animator.SetTrigger("Stop");
        }
    }
}