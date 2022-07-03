using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Transform _bodyBone;

    private Camera _camera;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void LateUpdate()
    {
        RotateBodyToMouse();
        _animator.SetFloat("velocity", _rigidbody.velocity.magnitude);
    }

    private void RotateBodyToMouse()
    {
        var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            var angle = Vector3.SignedAngle(transform.forward, hit.point - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up);
            _bodyBone.localEulerAngles += Quaternion.Euler(new Vector3(-angle, 0, 0)).eulerAngles;
        }
    }
}
