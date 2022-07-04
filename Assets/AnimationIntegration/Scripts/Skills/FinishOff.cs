using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class FinishOff : MonoBehaviour
{
    [SerializeField] private float _finishingAngle;
    [SerializeField] private float _finishingDistance;
    [SerializeField] private Text _tooltip;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _enemyPrefab;

    private GameObject _target;
    private Animator _animator;
    private CharacterController _controller;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_target == null || !Input.GetKeyDown(KeyCode.Space) || !_controller.enabled)
            return;

        FinishOffTarget();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Enemy>() == null || !CanFinish(otherCollider.gameObject))
            return;

        _tooltip.text = "Нажмите пробел для добивания";
        _target = otherCollider.gameObject;
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Enemy>() == null)
            return;

        _target = null;
        _tooltip.text = "";
    }

    private void FinishOffTarget()
    {
        var lookRotation = Quaternion.FromToRotation(_target.transform.forward, (transform.position - _target.transform.position).normalized);
        var position = _target.transform.position + lookRotation * _target.transform.forward * _finishingDistance;
        _tooltip.text = "";
        ToggleWepaon(true);
        _controller.enabled = false;
        transform.position = position;
        transform.LookAt(_target.transform);
        StartCoroutine(FinishingCoroutine());
    }

    private IEnumerator FinishingCoroutine()
    {
        _animator.SetBool("Finishing", true);
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Finishing"));
        _target.GetComponentInChildren<Animator>().enabled = false;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        ToggleWepaon(false);
        _weapon.SetActive(true);
        var target = _target;
        _animator.SetBool("Finishing", false);
        _controller.enabled = true;
        yield return new WaitForSeconds(5); 
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f)), Quaternion.identity);
        Destroy(target);
    }

    private void ToggleWepaon(bool state)
    {
        _sword.SetActive(state);
        _weapon.SetActive(!state);
    }

    private bool CanFinish(GameObject target)
    {
        var lookRotation = Quaternion.FromToRotation(target.transform.forward, (transform.position - target.transform.position).normalized);
        var rotationAngle = Quaternion.Angle(target.transform.rotation, lookRotation);
        return rotationAngle >= 180 - _finishingAngle / 2 && rotationAngle <= 180 + _finishingAngle / 2;
    }
}
