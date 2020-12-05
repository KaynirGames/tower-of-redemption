using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button _continueButton = null;

    private void Start()
    {
        if (_continueButton != null)
        {
            _continueButton.interactable = GameMaster.Instance.TryLoadSaveData();
        }
    }

    public void LoadGameMenu()
    {
        GameMaster.Instance.LoadScene(SceneType.GameMenu);
    }

    public void LoadTutorial()
    {
        GameMaster.Instance.LoadScene(SceneType.Tutorial);
    }

    public void ExitGame()
    {
        // save
        Application.Quit();
    }
}
