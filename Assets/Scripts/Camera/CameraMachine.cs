using UnityEngine;

public class CameraMachine : MonoBehaviour
{
    [SerializeField] private Transform _nearPoint;
    [SerializeField] private Transform _farPoint;
    private Transform _camera;

    public void SetFarPoint()
    {
        _camera.position = _farPoint.position;
        _camera.rotation = _farPoint.rotation;
    }

    public void SetNearPoint()
    {
        _camera.position = _nearPoint.position;
        _camera.rotation = _nearPoint.rotation;
    }
}