using UnityEngine;
using UnityEngine.Events;

namespace Players.Systems
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject[] _logs;
        private int _logIndex;
        private int _woodsAmount;
        private int _previousValue;

        public event UnityAction<int> WoodsAmountChanged;

        public int WoodsAmount
        {
            get => _woodsAmount;
            set
            {
                _woodsAmount = value;
                WoodsAmountChanged?.Invoke(_woodsAmount);
                RefreshPresenter();
            }
        }

        private void RefreshPresenter()
        {
            if (_previousValue < _woodsAmount)
            {
                // TODO: прямой цикл

                if (_logIndex < _logs.Length)
                {
                    _logs[_logIndex].SetActive(true);
                    _logIndex++;
                }
            }
            else
            {
                if (_woodsAmount < _logs.Length)
                {
                    // TODO: Обратный цикл

                    for (int i = 0; i < _previousValue - _woodsAmount; i++)
                    {
                        if (_logIndex > 0)
                        {
                            _logIndex--;
                            _logs[_logIndex].SetActive(false);
                        }
                        else
                        {
                            break;
                        }
                    }                    
                }
            }

            _previousValue = _woodsAmount;
        }
    }
}