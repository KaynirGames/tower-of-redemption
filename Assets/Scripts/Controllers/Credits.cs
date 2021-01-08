using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public static event System.Action OnCreditsEnd = delegate { };

    [SerializeField] private TextMeshProUGUI _creditsTextField = null;
    [SerializeField] private float _scrollSpeed = 2f;
    [SerializeField] private bool _play = false;

    private RectTransform _canvasRect;
    private RectTransform _creditsRect;

    private Vector2 _scrollEndPosition;
    private int _scrollSpeedRate;

    private void Awake()
    {
        _canvasRect = GetComponent<RectTransform>();
        _creditsRect = _creditsTextField.GetComponent<RectTransform>();
    }

    private void Start()
    {
        _creditsRect.anchoredPosition = Vector2.zero;

        if (_play)
        {
            SetCredits(_creditsTextField.text);
        }
    }

    private void Update()
    {
        if (!_play) { return; }

        if (!TryScrollCredits())
        {
            _play = false;
            OnCreditsEnd?.Invoke();
        }
    }

    public void SetCredits(string creditsText)
    {
        _creditsTextField.SetText(creditsText);

        float scrollEndPosY = LayoutUtility.GetPreferredHeight(_creditsRect) + _canvasRect.rect.height;
        _scrollEndPosition = new Vector2(_creditsRect.anchoredPosition.x, scrollEndPosY);
        _scrollSpeedRate = 1;

        _play = true;
    }

    public void AddScrollSpeedRate(int rate)
    {
        _scrollSpeedRate += rate;
    }

    private bool TryScrollCredits()
    {
        float speed = _scrollSpeed * _scrollSpeedRate * Time.deltaTime;
        _creditsRect.anchoredPosition += new Vector2(0f, speed);

        return _creditsRect.anchoredPosition.y <= _scrollEndPosition.y;
    }
}