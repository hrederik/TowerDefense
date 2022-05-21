using UnityEngine;
using UnityEngine.UI;
using Players.Systems;

namespace UI.Ingame
{
    public class IngamePanel : MonoBehaviour
    {
        [SerializeField] private Text _woodsAmountLabel;
        [SerializeField] private Inventory _inventory;

        private void OnEnable()
        {
            _inventory.WoodsAmountChanged += RefreshWoodsAmount;
        }

        private void OnDisable()
        {
            _inventory.WoodsAmountChanged -= RefreshWoodsAmount;
        }

        private void RefreshWoodsAmount(int amount)
        {
            _woodsAmountLabel.text = amount.ToString();
        }
    }
}
