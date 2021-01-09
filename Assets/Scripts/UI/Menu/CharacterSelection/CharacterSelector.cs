using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private CharacterSelectionCard _characterCardPrefab = null;
    [SerializeField] private GridLayoutGroup _characterLayoutGroup = null;
    [SerializeField] private List<PlayerCharacter> _playerCharacters = null;

    private CanvasGroup _canvasGroup;
    private List<CharacterSelectionCard> _characterCards;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _characterCards = new List<CharacterSelectionCard>();

        for (int i = 0; i < _playerCharacters.Count; i++)
        {
            CharacterSelectionCard card = Instantiate(_characterCardPrefab,
                                                      _characterLayoutGroup.transform,
                                                      false);
            card.SetCharacter(_playerCharacters[i]);
            _characterCards.Add(card);
        }
    }

    public void ToggleSelector(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
    }
}
