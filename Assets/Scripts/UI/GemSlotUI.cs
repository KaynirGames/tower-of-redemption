using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _gemStoneIcon = null;

    private EnergyGenerator _energyGenerator;
    private GemStone _gemStone;

    private void Start()
    {
        _energyGenerator = BattleManager.Instance.EnergyGenerator;
    }

    public void UpdateGemSlot(GemStone gemStone)
    {
        if (gemStone == null)
        {
            ClearGemSlot();
        }
        else
        {
            SetGemSlot(gemStone);
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
        Debug.Log("Selection complete.");
        _energyGenerator.ConsumeMatchingGems();
    }

    private void SetGemSlot(GemStone gemStone)
    {
        _gemStone = gemStone;
        _gemStoneIcon.sprite = gemStone.GemObject.GemSprite;
        _gemStoneIcon.enabled = true;
    }

    private void ClearGemSlot()
    {
        _gemStone = null;
        _gemStoneIcon.sprite = null;
        _gemStoneIcon.enabled = false;
    }

    private void SelectGemStone()
    {
        if (_energyGenerator.TrySelectGem(_gemStone))
        {
            Debug.Log($"Выбран {_gemStone.GemObject.name}.");
        }
        else
        {
            Debug.Log("Отмена выбора.");
        }
    }
}
