using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuitGame : MonoBehaviour
{
    [SerializeField] Image fadeImage; 

    public void Quit()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.transform.DOScale(new Vector3(22.5f, 22.5f, 1), 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            Application.Quit();
        });
    }
}
