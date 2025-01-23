public class PlayerScore
{
    public PlayerScore(string name, int score)
    {
        PlayerName = name;
        Score = score;
    }

    public string PlayerName { get; }
    public int Score { get; }

    public override string ToString()
    {
        return PlayerName + SignUtils.DoubleDot + Score;
    }
}