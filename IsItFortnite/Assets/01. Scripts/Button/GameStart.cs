using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameStart : MonoBehaviour
{
    [SerializeField] RectTransform masterBoard;

    public void StartGame()
    {
        masterBoard.DOLocalMoveX(-1920, 2f).OnComplete(() => {
            SceneManager.LoadScene("InGame");
        });
    }
}
