using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private float _delayBeforeDestroy = 0.5f;

    public void DestroyObject()
    {
        Destroy(gameObject, _delayBeforeDestroy);
    }
}
