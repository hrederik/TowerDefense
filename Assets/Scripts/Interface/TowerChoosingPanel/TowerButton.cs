using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI.Ingame.TowerChoosingPanel
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _costLabel;

        public void Initialize(int cost, UnityAction callback)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(callback);
            _costLabel.text = cost.ToString();
        }
    }
}