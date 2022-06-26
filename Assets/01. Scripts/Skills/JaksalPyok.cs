using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaksalPyok : SkillBase
{
    [SerializeField] PoolableMono FireAudio;
    [SerializeField] List<Transform> firePoses;
    [SerializeField] int count = 5;
    [SerializeField] float fireDelay = 0.5f;
    private CamControl cc = null;
    private PlayerControl pc = null;

    private void Start()
    {
        pc = GetComponentInParent<PlayerControl>();
        cc = GameObject.Find("CM vcam1").GetComponent<CamControl>();
    }

    public void PyokPyok()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            coolDown = 0;
            StartCoroutine(Pyok());
        }
    }

    private IEnumerator Pyok()
    {
        int c = 0;
        for(int i = 0; i < count; i ++)
        {
            cc.Shake(8, 0.1f);
            PoolManager.Instance.Dequeue(FireAudio);
            PoolableMono cb = PoolManager.Instance.Dequeue(pc.bulletList[pc.bulletIndex]) as PoolableMono;
            cb.transform.position = firePoses[c].position;
            cb.transform.rotation = lookAt.rotation;
            yield return new WaitForSeconds(fireDelay);
            if(c >= firePoses.Count - 1)
                c = 0;
            else
                c++;
        }
    }
}
