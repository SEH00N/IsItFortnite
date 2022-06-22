using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoFade : MonoBehaviour
{
    private Image fadeImage = null;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();

        fadeImage.DOFade(0, 3f).OnComplete(() => fadeImage.gameObject.SetActive(false));
    }
}
