using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class BaseCube : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected Collider _collider;

    public Rigidbody Rb => _rb;
    public Collider Colider => _collider;

    private void OnValidate()
    {
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();
        if(_collider == null)
            _collider = GetComponent<Collider>();
    }
}
