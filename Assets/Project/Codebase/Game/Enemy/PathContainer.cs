using UnityEngine;

namespace Game.Bots
{
    public class PathContainer : MonoBehaviour
    {
        [SerializeField] private Transform[] _pathPoints;

        public Transform[] PathPoints => _pathPoints;
    }
}