using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _liderBoardText;

    private LeadersBoard _lidersBoard;

    private void Awake()
    {
        _lidersBoard = new LeadersBoard();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ShowLiderBoardText(GetElementsFromLiderBoard());
        }
    }

    private string[] GetElementsFromLiderBoard()
    {
        string[] allElements = _lidersBoard.ToString().Split(SignUtils.Space);
        string[] correctedBordElements = new string[allElements.Length - 1];
        
        for (int i = 0; i < correctedBordElements.Length; i++)
            correctedBordElements[i] = (i + 1).ToString() + SignUtils.Dot + allElements[i];

        return correctedBordElements;
    }

    private void ShowLiderBoardText(string[] liderBoardText)
    {
        _liderBoardText.text = string.Empty;
       
        for (int i = 0; i < liderBoardText.Length; i++)
            _liderBoardText.text += liderBoardText[i] + SignUtils.NextLine;
    }
}