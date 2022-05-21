using UnityEngine;
using UnityEngine.Events;
using Players.Systems;

namespace Game.Towers
{
    public class Placer : MonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Tower _bomberPrefab;
        [SerializeField] private Tower _archerPrefab;
        [SerializeField] private Tower _wizardPrefab;

        public event UnityAction Placed;

        public int BomberCost => _bomberPrefab.Cost;
        public int ArcherCost => _archerPrefab.Cost;
        public int WizardCost => _wizardPrefab.Cost;

        public void PlaceBomber(Platform platform)
        {
            Place(_bomberPrefab, platform);
        }

        public void PlaceArcher(Platform platform)
        {
            Place(_archerPrefab, platform);
        }

        public void PlaceWizard(Platform platform)
        {
            Place(_wizardPrefab, platform);
        }

        private void Place(Tower towerPrefab, Platform platform)
        {
            var tower = Instantiate(towerPrefab, platform.Position, Quaternion.identity);
            tower.Initialize(_playerInventory);

            platform.Sleep();
            Placed?.Invoke();
        }
    }
}