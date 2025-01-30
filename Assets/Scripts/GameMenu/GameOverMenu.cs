using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _leadersBoardText;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _backToStartMenu;
    [SerializeField] private EnterNewScore _enterNewScore;
    [SerializeField] private Player _player;
    [SerializeField] private Pause _pause;

    private LeadersBoard _leadersBoard;
    private UIAnimator _uiAnimator;

    private void Awake()
    {
        _leadersBoard = new LeadersBoard();
        MapButtons();
        _player.PlayerDead += OnPlayerDead;
        _enterNewScore.OnClosed += NewScoreClosed;
        _uiAnimator = GetComponent<UIAnimator>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void NewScoreClosed()
    {
        _leadersBoard.SetLeadersBoardBText(_leadersBoardText);
        ActivateButtons();
    }

    private void OnPlayerDead()
    {
        _pause.Stop();
        _pause.UnSubscribe();
        _uiAnimator.Show();
        GameUtils.UnlockCursor();

        if (_score.Value > _leadersBoard.GetLastElementScore())
        {
            _enterNewScore.Initialize(_leadersBoard, _score.Value);
        }
        else
        {
            _leadersBoard.SetLeadersBoardBText(_leadersBoardText);
            ActivateButtons();
        } 
    }

    private void MapButtons()
    {
        _restart.onClick.AddListener(() => ReloadGame());
        _backToStartMenu.onClick.AddListener(() => LoadStartMenu());
        DeactivateButtons();
    }

    private void DeactivateButtons()
    {
        _restart.gameObject.SetActive(false);
        _backToStartMenu.gameObject.SetActive(false);
    }

    private void ActivateButtons()
    {
        _restart.gameObject.SetActive(true);
        _backToStartMenu.gameObject.SetActive(true);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(GameUtils.MainScene);
    }

    private void LoadStartMenu()
    {
        SceneManager.LoadScene(GameUtils.MenuScene);
    }
}