using UnityEngine;
using UnityEngine.Events;
using Game.Bots;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<EnemyAi> _entered;
    [SerializeField] private UnityEvent<EnemyAi> _exited;

    private void OnTriggerEnter(Collider other)
    {
        InvokeIfPlayer(other, _entered);
    }

    private void OnTriggerExit(Collider other)
    {
        InvokeIfPlayer(other, _exited);
    }

    private void InvokeIfPlayer(Collider other, UnityEvent<EnemyAi> callback)
    {
        var enemy = other.GetComponent<EnemyAi>();

        if (enemy != null)
        {
            callback?.Invoke(enemy);
        }
    }
}
