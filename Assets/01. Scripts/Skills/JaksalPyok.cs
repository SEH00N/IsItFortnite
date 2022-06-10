using System.Collections;
using UnityEngine;

public class JaksalPyok : SkillBase
{
    [SerializeField] Laser bullet;
    [SerializeField] int count = 5;
    [SerializeField] float fireDelay = 0.5f;

    public void PyokPyok()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            StartCoroutine(Pyok());
            coolDown = 0;
        }
    }

    private IEnumerator Pyok()
    {
        for(int i = 0; i < count; i ++)
        {
            Laser cb = PoolManager.Instance.Dequeue(bullet) as Laser;
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            yield return new WaitForSeconds(fireDelay);
        }
    }
}
