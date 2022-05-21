using UnityEngine;

namespace Game.Damage
{
    public class Bullet : Projectile
    {
        protected override void ApplyDamage()
        {
            Target.ApplyDamage(Damage);
            Instantiate(SoundsPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}