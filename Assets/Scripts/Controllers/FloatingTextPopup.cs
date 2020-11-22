using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingTextPopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh = null;
    [SerializeField] private Color _textColor = new Color(0f, 0f, 0f, 1f);
    [SerializeField] private float _fontSize = 8f;
    [Header("Настройки DOTween:")]
    [SerializeField] private float _popupLifetime = 4f;
    [SerializeField] private float _fadeDelay = 2f;
    [SerializeField] private Vector2 _minMoveOffset = Vector3.zero;
    [SerializeField] private Vector2 _maxMoveOffset = Vector3.zero;
    [SerializeField] private Vector2 _scaleFactor = Vector2.one;
    [SerializeField] private AnimationCurve _scaleCurve = null;

    private Vector3 _currentMoveOffset;

    private Tween _scaleTween;
    private Tween _fadeTween;

    private void Awake()
    {
        CreateTweens();
    }

    private void OnEnable()
    {
        PlayTweens();
    }

    public FloatingTextPopup Create(string textValue, Vector2 position, float fontSize)
    {
        FloatingTextPopup textPopup = PoolManager.Instance.Take(tag)
                                                          .GetComponent<FloatingTextPopup>();

        if (textPopup == null) { return null; }

        textPopup.Setup(textValue, position, fontSize);

        return textPopup;
    }

    public FloatingTextPopup Create(string textValue, Vector2 position)
    {
        return Create(textValue, position, _fontSize);
    }

    public FloatingTextPopup Create(float numericValue, Vector2 position, float fontSize, bool scaleFontByNumericValue = false)
    {
        if (scaleFontByNumericValue)
        {
            fontSize = ScaleFontSize(fontSize, numericValue);
        }

        return Create(numericValue.ToString(), position, fontSize);
    }

    public FloatingTextPopup Create(float numericValue, Vector2 position, bool scaleFontByNumericValue = false)
    {
        return Create(numericValue, position, _fontSize, scaleFontByNumericValue);
    }

    private float ScaleFontSize(float fontSize, float numericValue)
    {
        if (numericValue > 10f)
        {
            return fontSize + Mathf.Round(fontSize / 10f);
        }

        return fontSize;
    }

    private void Setup(string textValue, Vector2 position, float fontSize)
    {
        transform.position = position;
        _textMesh.color = _textColor;
        _textMesh.fontSize = fontSize;
        _textMesh.SetText(textValue);
    }

    private void CreateTweens()
    {
        _scaleTween = transform.DOScale(_scaleFactor, _popupLifetime / 2)
                               .SetEase(_scaleCurve)
                               .SetAutoKill(false)
                               .Pause();

        _fadeTween = _textMesh.DOFade(0f, _popupLifetime - _fadeDelay)
                              .SetAutoKill(false)
                              .Pause()
                              .SetDelay(_fadeDelay)
                              .OnComplete(RemoveTextPopup);
    }

    private void RemoveTextPopup()
    {
        gameObject.SetActive(false);

        _fadeTween.Rewind();
        _scaleTween.Rewind();

        PoolManager.Instance.Store(gameObject);
    }

    private void PlayTweens()
    {
        float offsetX = Random.Range(_minMoveOffset.x, _maxMoveOffset.x);
        float offsetY = Random.Range(_minMoveOffset.y, _maxMoveOffset.y);

        _currentMoveOffset = new Vector3(offsetX, offsetY, 0);

        transform.DOMove(transform.position + _currentMoveOffset, _popupLifetime)
                 .OnKill(() => transform.position = Vector3.zero);

        _scaleTween.Play();
        _fadeTween.Play();
    }
}
