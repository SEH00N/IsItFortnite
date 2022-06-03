using UnityEngine;

public class SpawnBlackHole : SkillBase
{
    /// <summary>
    /// 블랙홀 소환
    /// </summary>
    public void FireBlackHole()
    {
        if(coolDown > coolTime && Input.GetKeyDown(key))
        {
            BlackHole bh = PoolManager.Instance.Dequeue("BlackHole") as BlackHole;
            bh.transform.position = firePos.position;
            bh.transform.rotation = transform.parent.rotation;
            coolDown = 0;
        }
    }
}
