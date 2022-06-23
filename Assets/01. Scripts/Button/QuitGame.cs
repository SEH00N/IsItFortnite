using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuitGame : MonoBehaviour
{
    [SerializeField] Image fadeImage; 

    public void Quit()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1, 2f).SetEase(Ease.Linear).OnComplete(() => {
            Application.Quit();
        });
    }
}
