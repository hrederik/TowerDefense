using System.Collections;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float _passedTime;
    private Coroutine _stopwatch;

    public float PassedTime => _passedTime;

    public void Launch()
    {
        _stopwatch = StartCoroutine(Activate());
    }

    public void Stop()
    {
        Pause();
        _passedTime = 0;
    }

    public void Pause()
    {
        if (_stopwatch != null)
        {
            StopCoroutine(_stopwatch);
        }
    } 

    private IEnumerator Activate()
    {
        while (true)
        {
            _passedTime += Time.deltaTime;

            yield return null;
        }
    }
}