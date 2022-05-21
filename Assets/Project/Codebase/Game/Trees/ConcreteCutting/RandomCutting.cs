using UnityEngine;

namespace Trees.Concrete
{
    public class RandomCutting : MonoBehaviour, ICuttableBehaviour
    {
        [SerializeField] private int _minValue;
        [SerializeField] private int _maxValue;

        public int CutWood()
        {
            return Random.Range(_minValue, _maxValue);
        }
    }
}