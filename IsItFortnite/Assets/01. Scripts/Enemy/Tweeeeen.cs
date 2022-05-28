using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Tweeeeen : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Tweening());
    }

    private IEnumerator Tweening()
    {
        while(true)
        {
            transform.DOLocalRotate(new Vector3(0, 0, transform.localRotation.z - 360), 1f, RotateMode.LocalAxisAdd);
            yield return new WaitForSeconds(1.005f);
        }
    }
}
