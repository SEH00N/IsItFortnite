using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    [SerializeField] float time = 0;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("test"))
            time += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("test"))
            time = 0;
    }
}
