using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leadersBoardText;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _backToStartMenu;
    [SerializeField] private EnterNewScore _enterNewScore;
    [SerializeField] private Player _player;
    [SerializeField] private Pause _pause;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private UIAnimator _playerScore;
    [SerializeField] private TextMeshProUGUI _playerScoreText;

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
        _playerScore.gameObject.SetActive(false);
    }

    private void NewScoreClosed()
    {
        _leadersBoard.SetLeadersBoardBText(_leadersBoardText);
        ActivateButtons();
    }

    private void OnPlayerDead()
    {
        _pause.EndGame();
        _musicPlayer.MusicSource.clip = _gameOverClip;
        _musicPlayer.MusicSource.loop = false;
        _musicPlayer.MusicSource.Play();
        _musicPlayer.StopPlaying();
        _uiAnimator.Show();
        GameUtils.UnlockCursor();

        if (_player.Score.Value > _leadersBoard.GetLastElementScore())
        {
            _enterNewScore.Initialize(_leadersBoard, _player.Score.Value);
        }
        else
        {
            _leadersBoard.SetLeadersBoardBText(_leadersBoardText);
            _playerScoreText.text = _player.Score.Value.ToString();
            _playerScore.Show();
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