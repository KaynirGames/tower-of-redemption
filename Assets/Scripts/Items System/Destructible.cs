using UnityEngine;

public class Destructible : Interactable
{
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
            Destroy(gameObject,
                    _animator.GetCurrentAnimatorClipInfo(0).Length);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerAttackHit>() != null)
        {
            Interact();
        }
    }
}
