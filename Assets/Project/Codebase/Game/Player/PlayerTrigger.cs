using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<Player> _entered;
    [SerializeField] private UnityEvent<Player> _exited;

    private void OnTriggerEnter(Collider other)
    {
        InvokeIfPlayer(other, _entered);
    }

    private void OnTriggerExit(Collider other)
    {
        InvokeIfPlayer(other, _exited);
    }

    private void InvokeIfPlayer(Collider other, UnityEvent<Player> callback)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            callback?.Invoke(player);
        }
    }
}