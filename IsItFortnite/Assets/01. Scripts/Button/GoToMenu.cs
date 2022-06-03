using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GoToMenu : MonoBehaviour
{
    [SerializeField] RectTransform masterBoard;

    public void Menu()
    {
        Time.timeScale = 1;
        masterBoard.DOLocalMoveX(-1920, 2f).OnComplete(() => {
            SceneManager.LoadScene("Start");
        });
    }
}
