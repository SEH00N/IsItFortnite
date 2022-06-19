using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaksalPyok : SkillBase
{
    [SerializeField] List<Transform> firePoses;
    [SerializeField] Laser bullet;
    [SerializeField] int count = 5;
    [SerializeField] float fireDelay = 0.5f;

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
            Laser cb = PoolManager.Instance.Dequeue(bullet) as Laser;
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
