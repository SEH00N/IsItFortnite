using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoFade : MonoBehaviour
{
    [SerializeField] Image fadeImage = null;

    private void Awake()
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.DOFade(0, 2f).SetEase(Ease.Linear).OnComplete(() => fadeImage.gameObject.SetActive(false));
    }
}
