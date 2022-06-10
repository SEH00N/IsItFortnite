using System.ComponentModel.Design;
using UnityEngine;

public class FireCannonBall : SkillBase
{
    [SerializeField] PoolableMono cannonBall;

    /// <summary>
    /// 대포 발사
    /// </summary>
    public void CannonBallFire()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            CannonBall cb = PoolManager.Instance.Dequeue(cannonBall) as CannonBall;
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
