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
            if(Input.GetKeyDown(KeyCode.E)) break;
            yield return new WaitForSeconds(1f);
        }
    }
}
