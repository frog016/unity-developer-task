using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnRate;

    public UnityEvent<Vector3> OnObjectMoved { get; private set; }

    private Rigidbody _rigidbody;

    private void Awake()
    {
        OnObjectMoved = new UnityEvent<Vector3>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Rotate(direction);
        _rigidbody.velocity = direction * _moveSpeed;
        OnObjectMoved.Invoke(direction);
    }

    public void Rotate(Vector3 direction)
    {
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(direction), _turnRate * Time.deltaTime);
    }
}
