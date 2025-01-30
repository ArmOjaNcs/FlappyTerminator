using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _leadersBoardText;

    private LeadersBoard _leadersBoard;

    private void Awake()
    {
        _leadersBoard = new LeadersBoard();
        _leadersBoard.SetLeadersBoardBText(_leadersBoardText);
        _startButton.onClick.AddListener(() => BeginNewGame());
    }

    private void BeginNewGame()
    {
        SceneManager.LoadScene(GameUtils.MainScene);
    }
}