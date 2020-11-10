using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLayerSorter : MonoBehaviour
{
    [SerializeField] private bool _sortOnlyOnce = false;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100f);

        if (_sortOnlyOnce)
        {
            Destroy(this);
        }
    }
}
