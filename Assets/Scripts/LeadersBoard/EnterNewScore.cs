using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        gameObject.SetActive(false);
    }

    public void Initialize(LeadersBoard leadersBoard, int score)
    {
        gameObject.SetActive(true);
        _leadersBoard = leadersBoard;
        _score = score;
    }

    private void EnterNewLeadersBoardElement()
    {
        string tempName = _inputField.text;
        List<char> chars = new(); 
        
        for(int i = 0; i < tempName.Length; i++)
        {
            if (tempName[i] != SignUtils.Space && tempName[i] != SignUtils.DoubleDot)
                chars.Add(tempName[i]);
        }

        string name = new string(chars.ToArray());
        var playerScore = new PlayerScore(name, _score);
        _leadersBoard.AddElement(playerScore);
        gameObject.SetActive(false);
        OnClosed?.Invoke();
    }
}