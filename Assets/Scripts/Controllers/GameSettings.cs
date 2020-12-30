using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private GameLanguage _gameLanguage = GameLanguage.Russian;

    private bool _isInit;

    private void Start()
    {
        _isInit = false;
    }

    public void SetLanguage(int languageID)
    {
        _gameLanguage = (GameLanguage)languageID;
        GameMaster.Instance.LoadScene(SceneType.GameMenu);
    }

    public IEnumerator InitLocalizationRoutine()
    {
        if (!_isInit)
        {
            yield return LocalizationSettings.InitializationOperation;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales
                                                                      .Locales[(int)_gameLanguage];
            _isInit = true;
        }
    }
}