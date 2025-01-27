using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _leaderBoardText;
    [SerializeField] private Button _restart;
    [SerializeField] private EnterNewScore _enterNewScore;
    [SerializeField] private Player _player;

    private LeadersBoard _leadersBoard;

    private void Awake()
    {
        _leadersBoard = new LeadersBoard();
        MapButtons();
        _player.PlayerDead += OnPlayerDead;
        _enterNewScore.OnClosed += NewScoreClosed;
        gameObject.SetActive(false);
    }

    private void NewScoreClosed()
    {
        SetLeadersBoardBText();
        ActivateButtons();
    }

    private void OnPlayerDead()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        GameUtils.UlockCursor();

        if (_score.Value > _leadersBoard.GetLastElementScore())
        {
            _enterNewScore.Initialize(_leadersBoard, _score.Value);
        }
        else
        {
            SetLeadersBoardBText();
            ActivateButtons();
        } 
    }

    private void SetLeadersBoardBText()
    {
        string[] leaderBoardText = GetElementsFromLiderBoard();
        ShowLiderBoardText(leaderBoardText);
    }

    private void MapButtons()
    {
        _restart.onClick.AddListener(() => ReloadGame());
        DeactivateButtons();
    }

    private void DeactivateButtons()
    {
        _restart.gameObject.SetActive(false);
    }

    private void ActivateButtons()
    {
        _restart.gameObject.SetActive(true);
    }

    private static void ReloadGame()
    {
        SceneManager.LoadScene(GameUtils.MainScene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowLiderBoardText(GetElementsFromLiderBoard());
        }
    }

    private string[] GetElementsFromLiderBoard()
    {
        string[] allElements = _leadersBoard.ToString().Split(SignUtils.Space);
        string[] correctedBordElements = new string[allElements.Length - 1];

        for (int i = 0; i < correctedBordElements.Length; i++)
            correctedBordElements[i] = (i + 1).ToString() + SignUtils.Dot + allElements[i];

        return correctedBordElements;
    }

    private void ShowLiderBoardText(string[] liderBoardText)
    {
        _leaderBoardText.text = string.Empty;

        for (int i = 0; i < liderBoardText.Length; i++)
            _leaderBoardText.text += liderBoardText[i] + SignUtils.NextLine;
    }
}