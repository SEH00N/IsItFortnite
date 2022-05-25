using System.Collections;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while(true)
        {
            Debug.Log("Is Coroutine");
            yield return new WaitForSeconds(1f);
        }
    }
}
