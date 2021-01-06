using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField] private float _popupLifetime = 2f;
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private Vector2 _minMoveOffset = Vector3.zero;
    [SerializeField] private Vector2 _maxMoveOffset = Vector3.zero;
    [SerializeField] private AnimationCurve _fadeCurve = null;
    [SerializeField] private AnimationCurve _scaleCurve = null;

    private TextMeshPro _textMesh;
    private Color _textColor;

    private Vector3 _targetPosition;
    private float _timer;

    private void Awake()
    {
        _textMesh = GetComponentInChildren<TextMeshPro>();
        _timer = _popupLifetime;
        _textColor = _textMesh.color;
    }

    private void OnEnable()
    {
        Setup(Vector2.zero);
    }

    private void Update()
    {
        if (_timer <= _popupLifetime)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                                     _targetPosition,
                                                     Time.unscaledDeltaTime * _moveSpeed);
            HandleScale();
            HandleFade();

            _timer += Time.unscaledDeltaTime;
        }
        else
        {
            PoolManager.Instance.Store(gameObject);
        }
    }

    public void Setup(string textValue, Vector2 position, float fontSize)
    {
        transform.position = position;
        transform.localScale = Vector3.one;

        _textColor.a = 1f;
        _textMesh.color = _textColor;
        _textMesh.fontSize = fontSize;

        if (!string.IsNullOrEmpty(textValue))
        {
            _textMesh.SetText(textValue);
        }

        float offsetX = Random.Range(_minMoveOffset.x, _maxMoveOffset.x);
        float offsetY = Random.Range(_minMoveOffset.y, _maxMoveOffset.y);

        _targetPosition = new Vector3(offsetX, offsetY, 0) + transform.position;
        _timer = 0f;
    }

    public void Setup(float numericValue, Vector2 position, float fontSize)
    {
        Setup(numericValue.ToString(), position, fontSize);
    }

    public void Setup(string textValue, Vector2 position)
    {
        Setup(textValue, position, _textMesh.fontSize);
    }

    public void Setup(float numericValue, Vector2 position)
    {
        Setup(numericValue, position, _textMesh.fontSize);
    }

    public void Setup(Vector2 position)
    {
        Setup(string.Empty, position);
    }

    private void HandleScale()
    {
        float scaleRate = _scaleCurve.Evaluate(_timer / _popupLifetime);
        transform.localScale = new Vector3(scaleRate, scaleRate, scaleRate);
    }

    private void HandleFade()
    {
        float fadeRate = _fadeCurve.Evaluate(_timer / _popupLifetime);
        _textColor.a = fadeRate;
        _textMesh.color = _textColor;
    }
}
