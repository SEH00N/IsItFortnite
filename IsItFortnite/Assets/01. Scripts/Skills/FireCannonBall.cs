using System.ComponentModel.Design;
using UnityEngine;

public class FireCannonBall : SkillBase
{
    /// <summary>
    /// 대포 발사
    /// </summary>
    public void CannonBallFire()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            CannonBall cb = PoolManager.Instance.Dequeue("CannonBall") as CannonBall;
            cb.transform.position = firePos.position;
            cb.transform.rotation = transform.parent.rotation;
            coolDown = 0;
        }
    }
}
