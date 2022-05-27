using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CoMove()
    {
        transform.Translate(Vector3.up * 5 * Time.deltaTime);
        yield return new WaitForSeconds(3f);
    }
}
