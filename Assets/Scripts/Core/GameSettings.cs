using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private GameLanguage _gameLanguage = GameLanguage.Russian;

    public int CurrentLanguageID => (int)_gameLanguage;

    private bool _isInit;

    private void Start()
    {
        _isInit = false;
    }

    public void SetLanguage(int languageID)
    {
        _gameLanguage = (GameLanguage)languageID;
        GameMaster.Instance.LoadScene(SceneType.MainMenu);
    }

    public IEnumerator InitLocalizationRoutine()
    {
        if (!_isInit)
        {
            yield return LocalizationSettings.InitializationOperation;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales
                                                                      .Locales[CurrentLanguageID];

            _isInit = true;
        }
    }
}