using UnityEngine;

[RequireComponent(typeof(Movement))]
public class CharacterController : MonoBehaviour
{
    private Movement _movement;
    private Quaternion _rotation;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _rotation = Quaternion.Euler(0, FindObjectOfType<Camera>().transform.eulerAngles.y, 0);
    }

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        var direction = _rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _movement.Move(direction);
    }
}
