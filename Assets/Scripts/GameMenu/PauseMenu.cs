using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private UIAnimator _upgradeMenu;
    [SerializeField] private UIAnimator _pauseUI;

    private void OnEnable()
    {
        if(UpgradeUtils.NotAcceptedPlayerLevel > 0)
            _upgradeButton.gameObject.SetActive(true);
        else
            _upgradeButton.gameObject.SetActive(false);
    }

    public void ShowUpgradeMenu()
    {
        _upgradeMenu.Show();
        _pauseUI.Hide();
    }

    public void Restart()
    {
        SceneManager.LoadScene(GameUtils.MainScene);
    }   

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(GameUtils.MenuScene);
    }
}