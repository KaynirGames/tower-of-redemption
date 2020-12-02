using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    public void LoadGameMenu()
    {
        GameMaster.Instance.LoadScene(SceneType.GameMenu);
    }

    public void LoadTutorial()
    {
        GameMaster.Instance.LoadScene(SceneType.Tutorial);
    }

    public void LoadDungeon()
    {
        GameMaster.Instance.LoadScene(SceneType.Dungeon);
    }

    public void ExitGame()
    {
        // save
        Application.Quit();
    }
}
