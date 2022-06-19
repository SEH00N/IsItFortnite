using UnityEngine;
using DG.Tweening;

public class ChangeScoreBoard : MonoBehaviour
{
    [SerializeField] RectTransform board;
    [SerializeField] float distance = 1920;
    private bool isMooving = false;

    public void Right()
    {
        if (!isMooving)
        {
            isMooving = true;
            board.DOLocalMoveX(board.localPosition.x - distance, 2f).OnComplete(() =>
            {
                isMooving = false;
            });
        }
    }

    public void Left()
    {
        if (!isMooving)
        {
            isMooving = true;
            board.DOLocalMoveX(board.localPosition.x + distance, 2f).OnComplete(() =>
            {
                isMooving = false;
            });
        }
    }
}
