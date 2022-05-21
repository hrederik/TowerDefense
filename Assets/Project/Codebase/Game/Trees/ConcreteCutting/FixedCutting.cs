using UnityEngine;

namespace Trees.Concrete
{
    public class FixedCutting : MonoBehaviour, ICuttableBehaviour
    {
        [SerializeField] private int _value;

        public int CutWood()
        {
            return _value;
        }
    }
}