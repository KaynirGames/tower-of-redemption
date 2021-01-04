using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button _continueButton = null;

    private GameMaster _gameMaster;

    private void Start()
    {
        _gameMaster = GameMaster.Instance;

        if (_continueButton != null)
        {
            _continueButton.gameObject.SetActive(_gameMaster.TryLoadSaveData());
        }
    }

    public void LoadGameMenu()
    {
        _gameMaster.LoadScene(SceneType.GameMenu);
    }

    public void LoadTutorial()
    {
        _gameMaster.LoadScene(SceneType.Tutorial);
    }

    public void ExitGame()
    {
        // save
        Application.Quit();
    }

    public void TogglePause(bool enable)
    {
        _gameMaster.TogglePause(enable);
    }
}
