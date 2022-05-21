using UnityEngine;
using Game.Stats;

namespace Game.Towers.States
{
    public class HealingState : State
    {
        [SerializeField] private Health _health;
        [SerializeField] private int _amount;
        [SerializeField] private int _cost;

        public override bool CanDo 
        { 
            get
            {
                bool hasEnoughWood = _cost <= Inventory.WoodsAmount;
                bool hasPartialHp = _health.Current < _health.Max;

                return hasEnoughWood && hasPartialHp;
            }
        }

        public override void Do()
        {
            TryToHeal();
        }

        private void TryToHeal()
        {
            if (CanDo)
            {
                _health.Increase(_amount);
                Inventory.WoodsAmount -= _cost;
            }
        }
    }
}