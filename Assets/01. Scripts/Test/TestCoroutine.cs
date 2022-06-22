using System.Collections;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(Test());
        StartCoroutine(StopTest());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            StopCoroutine("StopTest");
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

    private IEnumerator StopTest()
    {
        while(true)
        {
            Debug.Log($"123");
            yield return new WaitForSeconds(1f);
        }
    }
}
