using UnityEngine;

namespace Game.Bots.Emit
{
    public class BotsContainer : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;

        public void SleepAllBots()
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                foreach (var bot in _waves[i].Bots)
                {
                    bot.ForceHide();
                }
            }
        }
    }
}