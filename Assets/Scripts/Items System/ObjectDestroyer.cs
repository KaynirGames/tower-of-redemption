using UnityEngine;
using UnityEngine.Events;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private float _delayBeforeDestroy = 0.5f;
    [SerializeField] private UnityEvent _onDestroy = null;

    public void DestroyObject()
    {
        _onDestroy?.Invoke();
        Destroy(gameObject, _delayBeforeDestroy);
    }
}
