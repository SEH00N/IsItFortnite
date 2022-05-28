using UnityEngine;
using DG.Tweening;

public class ChangeScoreBoard : MonoBehaviour
{
    [SerializeField] RectTransform board;
    [SerializeField] float distance = 1920;
    
    public void Right()
    {
        board.DOLocalMoveX(board.localPosition.x - distance, 2f);
    }

    public void Left()
    {
        board.DOLocalMoveX(board.localPosition.x + distance, 2f);
    }
}
