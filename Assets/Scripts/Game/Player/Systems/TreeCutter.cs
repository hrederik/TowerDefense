using Tree = Trees.Tree;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Players.Systems
{
    public class TreeCutter : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _cutDelay;
        private Coroutine _cutting;

        public event UnityAction Cut;

        public void StartCutting(Tree tree)
        {
            StopCutting();
            _cutting = StartCoroutine(Cutting(tree));
        }

        public void StopCutting()
        {
            if (_cutting != null)
            {
                StopCoroutine(_cutting);
                _cutting = null;
            }
        }

        private IEnumerator Cutting(Tree tree)
        {
            while(tree.CanCut)
            {
                var amount = tree.TakeWood();
                _inventory.WoodsAmount += amount;
                Cut?.Invoke();

                yield return new WaitForSeconds(_cutDelay);
            }
        }
    }
}