namespace Game.Damage
{
    public interface IDamageable
    {
        bool IsDead { get; }
        void ApplyDamage(int amount);
    }
}