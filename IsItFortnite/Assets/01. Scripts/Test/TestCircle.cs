using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCircle : MonoBehaviour
{
    [SerializeField] GameObject testPrefab;
    [SerializeField] float range = 10f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            for(int i = 0; i < 50; i++)
            {
                float angle = Random.Range(0, 360f) * Mathf.Deg2Rad;
                Vector3 randPos_1 = Random.insideUnitCircle * range;
                Vector3 randPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * range;

                Instantiate(testPrefab, randPos_1, Quaternion.identity);
            }
        }
    }
}
