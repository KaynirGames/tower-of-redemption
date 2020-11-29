using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    public void LoadTutorial()
    {
        GameMaster.Instance.LoadScene(SceneType.Tutorial);
    }

    public void LoadDungeon()
    {
        GameMaster.Instance.LoadDungeon();
    }
}
