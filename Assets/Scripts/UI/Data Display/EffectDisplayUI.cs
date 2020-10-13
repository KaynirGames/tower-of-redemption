using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplayUI : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect = null;
    [SerializeField] private GameObject _effectSlotsParent = null;
    [SerializeField] private float _scrollSpeed = 2f;

    private void Start()
    {
        StartCoroutine(ScrollContentRoutine());
    }

    private IEnumerator ScrollContentRoutine()
    {
        _scrollRect.verticalNormalizedPosition = 1;

        while (true)
        {
            _scrollRect.verticalNormalizedPosition -= _scrollSpeed * Time.deltaTime;

            Debug.Log(_scrollRect.verticalNormalizedPosition);

            yield return null;

            //if (_scrollRect.verticalNormalizedPosition > 1)
            //{
            //    _scrollRect.verticalNormalizedPosition = 0;
            //}
        }
    }
}
