using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemstoneUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _gemstoneIcon = null;

    private EnergyGenerator _energyGenerator;
    private Gemstone _gemstoneInstance;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _energyGenerator = BattleManager.Instance.EnergyGenerator;
    }

    public void UpdateDisplay(Gemstone gemstone)
    {
        if (gemstone == null)
        {
            ClearGemstone();
        }
        else
        {
            SetGemstone(gemstone);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_energyGenerator.IsSelectingGems)
        {
            SelectGemStone();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _energyGenerator.IsSelectingGems = true;
        SelectGemStone();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _energyGenerator.IsSelectingGems = false;
        _energyGenerator.ConsumeMatchingGemstones();
    }

    public void ToggleSelectionAnimation(bool enable)
    {
        if (!enable)
        {
            _animator.Rebind();
        }
        _animator.enabled = enable;
    }

    private void SetGemstone(Gemstone gemStone)
    {
        _gemstoneInstance = gemStone;
        _gemstoneIcon.sprite = gemStone.GemstoneSO.GemSprite;
        _gemstoneIcon.enabled = true;
    }

    private void ClearGemstone()
    {
        Instantiate(_gemstoneInstance.GemstoneSO.ConsumedGemstoneParticles,
                    _gemstoneIcon.transform.position,
                    Quaternion.identity);

        _gemstoneInstance = null;
        _gemstoneIcon.sprite = null;
        _gemstoneIcon.enabled = false;

        ToggleSelectionAnimation(false);
    }

    private void SelectGemStone()
    {
        if (_energyGenerator.TrySelectGemstone(_gemstoneInstance))
        {
            ToggleSelectionAnimation(true);
        }
    }
}
