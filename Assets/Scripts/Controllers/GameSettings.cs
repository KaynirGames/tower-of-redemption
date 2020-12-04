using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private GameLanguage _gameLanguage = GameLanguage.Russian;

    public void SetLanguage(int languageID)
    {
        _gameLanguage = (GameLanguage)languageID;
        GameMaster.Instance.LoadScene(SceneType.GameMenu);
    }

    public IEnumerator InitLocalizationRoutine()
    {
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales
                                                                  .Locales[(int)_gameLanguage];
    }
}