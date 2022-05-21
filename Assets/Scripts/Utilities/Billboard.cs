using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _transform;
    private Transform _camera;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        _transform.LookAt(_transform.position + _camera.forward);
    }
}
