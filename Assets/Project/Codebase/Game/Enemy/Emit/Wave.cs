using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Game.Bots.Emit
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] private List<EnemyAi> _bots;
        [SerializeField] private UnityEvent _waveStarted;
        [SerializeField] private UnityEvent _allBotsKilled;
        private int _botsAmount;
        private int _killedBotsAmount;

        public event UnityAction BotKilled;
        public event UnityAction AllBotsKilled
        {
            add => _allBotsKilled.AddListener(value);
            remove => _allBotsKilled.RemoveListener(value);
        }

        public int KiledBotsAmount => _killedBotsAmount;
        public List<EnemyAi> Bots => _bots;

        private void Start()
        {
            _botsAmount = _bots.Count;
        }

        public void Launch()
        {
            foreach (var bot in _bots)
            {
                bot.Killed += OnBotKilled;
            }

            _waveStarted?.Invoke();
        }

        private void OnBotKilled()
        {
            _killedBotsAmount++;
            BotKilled?.Invoke();
            CheckForEmpty();
        }

        private void CheckForEmpty()
        {
            if (_killedBotsAmount >= _botsAmount)
            {
                OnWaveEnded();
            }
        }

        private void OnWaveEnded()
        {
            _allBotsKilled?.Invoke();

            foreach (var bot in _bots)
            {
                bot.Killed -= OnBotKilled;
            }
        }
    }
}