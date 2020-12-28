using UnityEngine;

public class Destructible : Interactable
{
    [SerializeField] private float _destroyDelay = 3f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        _animator.SetTrigger("Destroy");

        _onInteraction?.Invoke();

        if (_interactOnce)
        {
            Destroy(gameObject, _destroyDelay);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerAttackHit attackHit))
        {
            Interact();
        }
    }
}
