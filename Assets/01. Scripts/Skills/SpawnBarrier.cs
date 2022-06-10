using UnityEngine;

public class SpawnBarrier : SkillBase
{
    [SerializeField] PoolableMono barrier;

    public void BarierOn()
    {
        if(coolDown > coolTime && Input.GetKeyDown(key))
        {
            Barrier temp = PoolManager.Instance.Dequeue(barrier) as Barrier;
            temp.transform.position = GameManager.Instance.player.position;
            coolDown = 0;
        }
    }
}
