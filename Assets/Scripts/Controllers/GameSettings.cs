using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private GameLanguage _gameLanguage = GameLanguage.Russian;

    public IEnumerator SetLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales
                                                                  .Locales[(int)_gameLanguage];
    }

    private enum GameLanguage
    {
        English,
        Russian
    }
}
