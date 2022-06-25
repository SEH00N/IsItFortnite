using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiAni : MonoBehaviour
{
    public bool isFinish = false;
    private SpriteRenderer sp = null;
    private Animator ani = null;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    public void Finish()
    {
        isFinish = true;
    }

    public void StartAni()
    {
        ani.enabled = true;
        sp.enabled = true;
    }

    public void EndAni()
    {
        ani.enabled = false;
        sp.enabled = false;
    }
}
