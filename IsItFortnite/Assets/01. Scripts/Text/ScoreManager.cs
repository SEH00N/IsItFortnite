using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentTime;
    public int currentScore;
    public int bestTime;
    public int bestScore;

    private void Awake()
    {
        GetScore();
    }

    private void GetScore()
    {
        currentTime = PlayerPrefs.GetInt("CurrentTime", 0);
        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestTime = PlayerPrefs.GetInt("BestTime", 0);
    }
}
