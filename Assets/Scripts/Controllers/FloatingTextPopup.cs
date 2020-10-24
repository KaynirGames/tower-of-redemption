using TMPro;
using UnityEngine;

public class FloatingTextPopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh = null;
    [SerializeField] private Color _textColor = new Color(0f, 0f, 0f, 1f);
    [SerializeField] private float _fontSize = 8f;
    [SerializeField] private Vector2 _positionOffset = Vector2.zero;
    [Header("Настройки анимации:")]
    [SerializeField] private float _minHorizontalMove = -1f;
    [SerializeField] private float _maxHorizontalMove = 1f;
    [SerializeField] private float _popupVerticalSpeed = 2f;
    [SerializeField] private float _popupScaleFactor = 1f;
    [SerializeField] private float _timeBeforeFading = 1f;

    public float FontSize => _fontSize;

    private Vector3 _popupMoveVector;

    private void Start()
    {
        _popupMoveVector = new Vector3(Random.Range(_minHorizontalMove, _maxHorizontalMove),
                                       _popupVerticalSpeed);
    }

    private void Update()
    {
        transform.position += _popupMoveVector * Time.deltaTime;
        _popupMoveVector -= _popupMoveVector * Time.deltaTime;

        HandlePopupScaling();
        HandlePopupFading();
    }

    public FloatingTextPopup Create(string textValue, Vector2 position)
    {
        return Create(textValue, position, _fontSize);
    }

    public FloatingTextPopup Create(string textValue, Vector2 position, float fontSize)
    {
        FloatingTextPopup textPopup = Instantiate(this,
                                          position + _positionOffset,
                                          Quaternion.identity);

        textPopup.Setup(textValue, fontSize);

        return textPopup;
    }

    public void Setup(string textValue)
    {
        Setup(textValue, _fontSize);
    }

    public void Setup(string textValue, float fontSize)
    {
        _textMesh.color = _textColor;
        _textMesh.fontSize = fontSize;
        _textMesh.SetText(textValue);
    }

    private void HandlePopupScaling()
    {
        if (_timeBeforeFading >= _timeBeforeFading / 2f)
        {
            transform.localScale += Vector3.one * _popupScaleFactor * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * _popupScaleFactor * Time.deltaTime;
        }
    }

    private void HandlePopupFading()
    {
        _timeBeforeFading -= Time.deltaTime;

        if (_timeBeforeFading < 0)
        {
            _textColor.a -= Time.deltaTime;
            _textMesh.color = _textColor;

            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
