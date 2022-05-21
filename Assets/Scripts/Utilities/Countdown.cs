using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Countdown : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private float _duration;
    private float _elapsedTime;
    private Coroutine _counting;

    public event UnityAction Started;
    public event UnityAction Ticked;
    public event UnityAction Ended;

    public bool IsLaunched => _counting != null;
    public float NormalizedValue => _elapsedTime / _duration;

    public void Launch()
    {
        _counting = StartCoroutine(Count());
        Started?.Invoke();
    }

    public void Stop()
    {
        StopCoroutine(_counting);
        Ended?.Invoke();

        _counting = null;
        _elapsedTime = 0;
    }

    private IEnumerator Count()
    {
        var waiting = new WaitForSecondsRealtime(_step);

        while (_elapsedTime < _duration)
        {
            yield return waiting;

            _elapsedTime += _step;
            Ticked?.Invoke();
        }

        yield return waiting;

        _elapsedTime = 0;
        Ended?.Invoke();
    }
}