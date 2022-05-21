using UnityEngine;

namespace Game.Bonuses
{
    public class Healer : MonoBehaviour
    {
        [SerializeField] private int _amount;

        public void Heal(Player player)
        {
            player.Heal(_amount);
        }
    }
}