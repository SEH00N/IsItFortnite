using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentTimeText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    private ScoreManager sm = null;

    private void Awake()
    {
        sm = GetComponent<ScoreManager>();
    }

    private void Start()
    {
        if (currentScoreText != null)
            currentScoreText.text = $"Score\n\n{sm.currentScore}";
        if (bestScoreText != null)
            bestScoreText.text = $"BestScore\n\n{sm.bestScore}";
        if (currentTimeText != null)
            currentTimeText.text = $"PlayTime\n\n{sm.currentTime}";
        if (bestTimeText != null)
            bestTimeText.text = $"BestPlayTime\n\n{sm.bestTime}";
    }
}
