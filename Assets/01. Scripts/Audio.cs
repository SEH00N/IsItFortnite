using UnityEngine;

public class Audio : PoolableMono
{
    public override void Reset()
    {
        Invoke("AutoDie", 5f);
    }

    private void AutoDie()
    {
        PoolManager.Instance.Enqueue(this);
    }
}
