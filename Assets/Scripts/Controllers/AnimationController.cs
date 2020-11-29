using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private const string ANIM_BASE_LAYER = "Base Layer";

    [SerializeField] private List<string> _stateNames = new List<string>();

    private Animator _animator;
    private Dictionary<string, int> _stateHashDic;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stateHashDic = new Dictionary<string, int>();

        foreach (string state in _stateNames)
        {
            _stateHashDic.Add(state, Animator.StringToHash($"{ANIM_BASE_LAYER}.{state}"));
        }
    }

    public void PlayAnimation(string stateName)
    {
        _animator.Play(_stateHashDic[stateName]);
    }

    public IEnumerator PlayAndWaitForAnimRoutine(string stateName, bool unscaled = false, Action onAnimation = null)
    {
        int stateHash = _stateHashDic[stateName];

        _animator.Play(stateHash);

        while (_animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateHash)
        {
            yield return null;
        }

        onAnimation?.Invoke();

        float timer = 0;
        float delta = unscaled
                ? Time.unscaledDeltaTime
                : Time.deltaTime;
        float waitTime = _animator.GetCurrentAnimatorStateInfo(0).length;

        while (timer < waitTime)
        {
            timer += delta;
            yield return null;
        }
    }
}
