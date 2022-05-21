using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DelayedEvent : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private UnityEvent _event;

    public void Invoke()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _event?.Invoke();
    }
}