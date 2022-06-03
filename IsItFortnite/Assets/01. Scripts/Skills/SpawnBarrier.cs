using UnityEngine;

public class SpawnBarrier : SkillBase
{
    public void BarierOn()
    {
        if(coolDown > coolTime && Input.GetKeyDown(key))
        {
            Barrier barrier = PoolManager.Instance.Dequeue("Barrier") as Barrier;
            barrier.transform.position = GameManager.Instance.player.position;
            coolDown = 0;
        }
    }
}
