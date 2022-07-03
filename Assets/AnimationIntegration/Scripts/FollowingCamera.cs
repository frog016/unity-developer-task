using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowingCamera : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _deltaPosition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        var target = FindObjectOfType<Movement>();
        target.OnObjectMoved.AddListener(FollowAtTarget);
        _deltaPosition = _camera.transform.position - target.transform.position;
    }

    private void FollowAtTarget(Vector3 position)
    {
        _camera.transform.position = position + _deltaPosition;
    }
}
