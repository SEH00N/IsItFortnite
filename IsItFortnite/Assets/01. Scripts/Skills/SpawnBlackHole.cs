using UnityEngine;

public class SpawnBlackHole : SkillBase
{
    [SerializeField] PoolableMono blackHole;

    /// <summary>
    /// 블랙홀 소환
    /// </summary>
    public void FireBlackHole()
    {
        if(coolDown > coolTime && Input.GetKeyDown(key))
        {
            BlackHole bh = PoolManager.Instance.Dequeue(blackHole) as BlackHole;
            bh.transform.position = firePos.position;
            bh.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
