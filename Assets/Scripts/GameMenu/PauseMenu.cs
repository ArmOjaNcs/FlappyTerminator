using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(GameUtils.MainScene);
    }   

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(GameUtils.MenuScene);
    }
}