using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowingCamera : MonoBehaviour
{
    private Camera _camera;
    private Movement _target;
    private Vector3 _deltaPosition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _target = FindObjectOfType<Movement>();
        _target.OnObjectMoved.AddListener(FollowAtTarget);
        _deltaPosition = _camera.transform.position - _target.transform.position;
    }

    private void FollowAtTarget(Vector3 direction)
    {
        _camera.transform.position = _target.transform.position + _deltaPosition;
    }
}
