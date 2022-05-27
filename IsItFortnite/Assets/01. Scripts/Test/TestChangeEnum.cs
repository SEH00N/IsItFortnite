using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeEnum : MonoBehaviour
{
    private TestEnum te = null;
    public TestEnum.State state = TestEnum.State.One;

    private void Start()
    {
        te = GetComponent<TestEnum>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            state = TestEnum.State.One;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            state = TestEnum.State.Two;
            
    }
}
