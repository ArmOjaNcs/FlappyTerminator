using UnityEngine;

public class LeadersBoard
{
    private readonly string _key = "LeadersBoard";

    private PlayerScore[] _playersScores;

    public LeadersBoard()
    {
        _playersScores = new PlayerScore[LidersBoardInitializer.MaxElements];
        
        if (PlayerPrefs.GetString(_key) == string.Empty)
        {
            _playersScores = LidersBoardInitializer.GetNewPlayersScores();
            string newBoardList = ToString();
            PlayerPrefs.SetString(_key, newBoardList);
        }
        else
        {
            string savedData = PlayerPrefs.GetString(_key);
            SetBoard(savedData);
        }
    }

    public void AddElement(PlayerScore playerScore)
    {
        PlayerScore tempPlayerScore = null;

        for (int i = _playersScores.Length - 1; i >= 0; i--)
        {
            for (int j = _playersScores.Length - 1; j > 0; j--)
            {
                if (_playersScores[j].Score >= _playersScores[j - 1].Score)
                {
                    tempPlayerScore = _playersScores[j - 1];
                    _playersScores[j - 1] = _playersScores[j];
                    _playersScores[j] = tempPlayerScore;
                }
            }
        }

        PlayerPrefs.SetString(_key, ToString());
    }

    public override string ToString()
    {
        string allElements = string.Empty;

        for (int i = 0; i < _playersScores.Length; i++)
            allElements += _playersScores[i].ToString() + SignUtils.Space;

        return allElements;
    }

    public int GetLastElementScore()
    {
        return _playersScores[_playersScores.Length - 1].Score;
    }

    private void SetBoard(string savedData)
    {
        string[] liders = savedData.Split(SignUtils.Space);

        for(int i = 0; i < _playersScores.Length; i++)
        {
            string[] playerScoreText = liders[i].Split(SignUtils.DoubleDot);
            int.TryParse(playerScoreText[1], out int result);
            _playersScores[i] = new PlayerScore(playerScoreText[0], result);
        }
    }
}