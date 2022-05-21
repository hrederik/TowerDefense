using UnityEngine;
using UnityEngine.Events;
using Game.Towers;

namespace Game.Bots
{
    public class Vision : MonoBehaviour
    {
        public event UnityAction<Player> PlayerFound;
        public event UnityAction<Player> PlayerStay;
        public event UnityAction<Player> PlayerLost;
        public event UnityAction<Tower> TowerFound;
        public event UnityAction<Tower> TowerLost;

        private void OnTriggerEnter(Collider other)
        {
            CheckFor<Player>(other, PlayerFound);
            CheckFor<Tower>(other, TowerFound);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckFor<Player>(other, PlayerLost);
            CheckFor<Tower>(other, TowerLost);
        }

        private void OnTriggerStay(Collider other)
        {
            CheckFor<Player>(other, PlayerStay);
        }

        private void CheckFor<T>(Collider collider, UnityAction<T> callback)
        {
            var entity = collider.GetComponent<T>();

            if (entity != null)
            {
                callback?.Invoke(entity);
            }
        }
    }
}