using UnityEngine;

namespace Game.Players
{
    public class CameraPositions : MonoBehaviour
    {
        [SerializeField] private TargetFollowing _camera;
        [SerializeField] private Vector3 _topPointOffset;
        [SerializeField] private Vector3 _topPointRotation;
        [SerializeField] private Vector3 _bottomPointOffset;
        [SerializeField] private Vector3 _bottomPointRotation;

        public void SetBottomTransform()
        {
            SetNewTransform(_bottomPointOffset, _bottomPointRotation);
        }

        public void SetTopTransform()
        {
            SetNewTransform(_topPointOffset, _topPointRotation);
        }

        private void SetNewTransform(Vector3 offset, Vector3 rotation)
        {
            _camera.ChangeOffset(offset);
            _camera.transform.rotation = Quaternion.Euler(rotation);
            
        }
    }
}