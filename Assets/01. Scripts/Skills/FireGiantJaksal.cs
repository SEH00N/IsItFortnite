using UnityEngine;

public class FireGiantJaksal : SkillBase
{
    [SerializeField] PoolableMono fireAudio;
    [SerializeField] PoolableMono bullet;

    public void GiantJaksal()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            PoolManager.Instance.Dequeue(fireAudio);
            PoolableMono cb = PoolManager.Instance.Dequeue(bullet);
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
