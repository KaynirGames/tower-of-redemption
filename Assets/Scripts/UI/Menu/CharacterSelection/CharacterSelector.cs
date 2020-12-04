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
            _characterCards.Add(Instantiate(_characterCardPrefab,
                                _characterLayoutGroup.transform,
                                false));
            _characterCards[i].SetCharacter(_playerCharacters[i]);
        }
    }

    public void ToggleSelector(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
    }
}
