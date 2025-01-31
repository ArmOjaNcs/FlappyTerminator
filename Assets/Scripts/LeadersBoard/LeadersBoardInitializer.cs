public static class LidersBoardInitializer
{
    public readonly static int MaxElements = 10;

    private readonly static int _maxScore = 2200;
    private readonly static int _subtractor = 200;

    public static PlayerScore[] GetNewPlayersScores()
    {
        int currentScore = _maxScore;
        PlayerScore[] newPlayersScores = new PlayerScore[MaxElements];
        string[] names = GetNames();

        for (int i = 0; i < MaxElements; i++)
            newPlayersScores[i] = new PlayerScore(names[i], currentScore -= _subtractor);

        return newPlayersScores;
    }

    private static string[] GetNames()
    {
        string[] names = new string[]
        {
        "Kevin", "Angela", "Robert", "Maria", "Sam",
        "Betty", "Max", "Samantha", "Nicholas", "Catherine"
        };

        return names;
    }
}