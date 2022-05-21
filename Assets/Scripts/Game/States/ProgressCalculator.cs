using UnityEngine;
using Game.Bots.Emit;

namespace Game.States
{
    public class ProgressCalculator : MonoBehaviour
    {
        [SerializeField] private Wave[] _waves;
        private int _part;

        private void Start()
        {
            _part = (int)(100.0f / _waves.Length);
        }

        public int CalculateProgress()
        {
            int progress = 0;

            for (int i = 0; i < _waves.Length; i++)
            {
                progress += CalculatePart(i);
            }

            return progress;
        }

        private int CalculatePart(int index)
        {
            int part = 0;

            part += (int)((float)_waves[index].KiledBotsAmount / _waves[index].Bots.Count * _part);

            return part;
        }
    }
}