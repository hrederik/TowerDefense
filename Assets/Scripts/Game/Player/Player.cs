using UnityEngine;
using Game.Stats;
using Game.Damage;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    private Transform _transform;

    public bool IsDead { get; private set; }
    public Vector3 Position => _transform.position;
    public Transform Transform => _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void ApplyDamage(int amount)
    {
        _health.Decrease(amount);
    }

    public void Heal(int amount)
    {
        _health.Increase(amount);
    }
}