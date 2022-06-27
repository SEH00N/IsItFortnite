using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private Image image = null;

    private void Start()
    {
        image = GetComponent<Image>();
        image.transform.DOScale(Vector3.zero, 1f).OnComplete(() => 
        {
            image.gameObject.SetActive(false);
        });
    }
}
