using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StartEvent : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private UnityEvent _event;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_delay);
        _event?.Invoke();
    }
}