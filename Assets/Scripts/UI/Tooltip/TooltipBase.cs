using System.Collections;
using UnityEngine;

public abstract class TooltipBase : MonoBehaviour
{
    [SerializeField] private float _tooltipDelay = 0f;

    protected TooltipManager _tooltipManager;
    protected WaitForSecondsRealtime _waitForTooltip;
    protected Coroutine _lastShowRoutine;

    protected virtual void Start()
    {
        _tooltipManager = TooltipManager.Instance;
        _waitForTooltip = new WaitForSecondsRealtime(_tooltipDelay);
    }

    protected abstract void Show();

    protected void Hide()
    {
        if (_lastShowRoutine != null)
        {
            StopCoroutine(_lastShowRoutine);
        }

        _tooltipManager.HideTooltip();
    }

    protected IEnumerator ShowTooltipRoutine()
    {
        yield return _waitForTooltip;
        Show();
    }
}
