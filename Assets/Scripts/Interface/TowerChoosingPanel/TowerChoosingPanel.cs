using Game.Towers;
using UnityEngine;

namespace UI.Ingame.TowerChoosingPanel
{
    public class TowerChoosingPanel : MonoBehaviour
    {
        [SerializeField] private Placer _towerPlacer;
        [SerializeField] private GameObject _window;
        [SerializeField] private TowerButton _bomber;
        [SerializeField] private TowerButton _archer;
        [SerializeField] private TowerButton _wizard;

        private void OnEnable()
        {
            _towerPlacer.Placed += Hide;
        }

        private void OnDisable()
        {
            _towerPlacer.Placed -= Hide;
        }

        public void Initialize(Platform platform)
        {
            _bomber.Initialize(_towerPlacer.BomberCost, () => _towerPlacer.PlaceBomber(platform));
            _archer.Initialize(_towerPlacer.ArcherCost, () => _towerPlacer.PlaceArcher(platform));
            _wizard.Initialize(_towerPlacer.WizardCost, () => _towerPlacer.PlaceWizard(platform));

            _window.SetActive(true);
        }

        private void Hide()
        {
            _window.SetActive(false);
        }
    }
}