using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterNewScore : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;

    private LeadersBoard _leadersBoard;
    private int _score;

    public event Action OnClosed;

    private void Awake()
    {
        _button.onClick.AddListener(EnterNewLeadersBoardElement);
    }

    public void Initialize(LeadersBoard leadersBoard, int score)
    {
        gameObject.SetActive(true);
        _leadersBoard = leadersBoard;
        _score = score;
    }

    private void EnterNewLeadersBoardElement()
    {
        var playerScore = new PlayerScore(_inputField.text, _score);
        _leadersBoard.AddElement(playerScore);
        gameObject.SetActive(false);
        OnClosed?.Invoke();
    }
}