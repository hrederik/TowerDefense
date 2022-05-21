using UnityEngine;
using UnityEngine.Events;

namespace Game.Towers.States
{
    public class BuildingState : State
    {
        [SerializeField] private Tower _tower;
        [SerializeField] private State _nextState;
        [SerializeField] private GameObject[] _elements;
        [SerializeField] private FlyingLog[] _logFollowings;
        [SerializeField] private AudioClip _building;
        [SerializeField] private AudioClip _success;
        [SerializeField] private AudioSource _source;
        [SerializeField] private UnityEvent _built;
        private int _index;
        private int _costPerStep;
        private int _cost;

        public event UnityAction Built
        {
            add => _built.AddListener(value);
            remove => _built.RemoveListener(value);
        }

        public override bool CanDo
        {
            get
            {
                int currentPayment = CalculateCurrentPayment();
                bool hasEnoughWood = Inventory.WoodsAmount >= currentPayment;

                return hasEnoughWood && CanContinue;
            }
        }

        private bool CanContinue => _index < _elements.Length;

        private void Start()
        {
            _costPerStep = _tower.Cost / _elements.Length;
            _cost = _tower.Cost;
        }

        public override void Do()
        {
            TryToBuild();
        }

        private void TryToBuild()
        {
            if (CanDo)
            {
                _elements[_index].SetActive(true);

                if (_index % 2 == 0 && _index < _logFollowings.Length)
                {
                    _logFollowings[_index].gameObject.SetActive(true);
                    _logFollowings[_index].Activate(Inventory.transform.position, _tower.Position);

                    _source.pitch = Random.Range(0.90f, 1.10f);
                    _source.PlayOneShot(_building);
                }                

                _index++;

                TakeWood();
                CheckForFinish();
            }
        }

        private void CheckForFinish()
        {
            if (CanContinue == false)
            {
                _built?.Invoke();
                _source.pitch = 1;
                _source.PlayOneShot(_success);

                _tower.State = _nextState;
                _nextState.Initialize(Inventory);
            }
        }

        private void TakeWood()
        {
            int currentPayment = CalculateCurrentPayment();

            Inventory.WoodsAmount -= currentPayment;
            _cost -= currentPayment;
        }

        private int CalculateCurrentPayment()
        {
            bool isLastPayment = CanContinue;
            return isLastPayment == true ? _costPerStep : _cost;
        }
    }
}