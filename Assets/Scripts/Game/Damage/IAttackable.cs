using Game.Bots;

namespace Game.Damage
{
    public interface IAttackable
    {
        void Attack(EnemyAi enemy);
    }
}