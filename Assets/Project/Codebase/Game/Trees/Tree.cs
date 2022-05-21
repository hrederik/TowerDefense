using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private int _restoreDelay;
        [SerializeField] private Presenter _presenter;
        [SerializeField] private MonoBehaviour _cuttableBehaviour;

        [SerializeField] private UnityEvent _cut;

        public bool CanCut => _presenter.LeftLogsAmount + 1 > 0;
        private ICuttableBehaviour _cuttable => (ICuttableBehaviour)_cuttableBehaviour;

        private void OnValidate()
        {
            if (_cuttableBehaviour != null)
                InterfaceValidator.Validate<ICuttableBehaviour>(_cuttableBehaviour);
        }

        public int TakeWood()
        {
            var cuttedWood = 0;

            if (CanCut)
            {
                cuttedWood = _cuttable.CutWood();
                _presenter.Cut();

                CheckForEmpty();
            }
            
            return cuttedWood;
        }

        private void CheckForEmpty()
        {
            if (CanCut == false)
            {
                _cut?.Invoke();
                StartCoroutine(Restoring());
            }
        }

        private IEnumerator Restoring()
        {
            yield return new WaitForSeconds(_restoreDelay);

            _presenter.Restore();
        }
    }
}