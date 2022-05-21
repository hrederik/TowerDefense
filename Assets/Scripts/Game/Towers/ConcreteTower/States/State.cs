using UnityEngine;
using Players.Systems;

namespace Game.Towers.States
{
    public abstract class State : MonoBehaviour
    {
        private Inventory _inventory;

        public abstract bool CanDo { get; }
        protected Inventory Inventory => _inventory;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
        }

        public abstract void Do();
    }
}