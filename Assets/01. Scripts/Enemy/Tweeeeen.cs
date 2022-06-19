using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Tweeeeen : MonoBehaviour
{
    [SerializeField] float duration = 1f;

    private void OnEnable()
    {
        StartCoroutine(Tweening());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private IEnumerator Tweening()
    {
        while(true)
        {
            Sequence seq = DOTween.Sequence();
            transform.DOLocalRotate(new Vector3(0f, 0f, -360f), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
        }
    }
}
