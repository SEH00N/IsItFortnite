using UnityEngine;

public class FireGiantJaksal : SkillBase
{
    [SerializeField] PoolableMono bullet;

    public void GiantJaksal()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            Laser1 cb = PoolManager.Instance.Dequeue(bullet) as Laser1;
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
