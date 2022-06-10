using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour, ITest
{
    [SerializeField] string nameSpace = null;
    public void TT()
    {
        Debug.Log(nameSpace);
    }
}
