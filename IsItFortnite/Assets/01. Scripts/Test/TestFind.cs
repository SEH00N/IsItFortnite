using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFind : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            List<GameObject> a = new List<GameObject>();
            a.Add(GameObject.Find("TestBB"));

            foreach(GameObject temp in a)
                temp.GetComponent<ITest>().TT();
        }
    }
}
