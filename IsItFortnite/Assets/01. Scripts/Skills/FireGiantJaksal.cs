using UnityEngine;

public class FireGiantJaksal : SkillBase
{
    [SerializeField] Laser bullet;

    public void GiantJaksal()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            Laser cb = PoolManager.Instance.Dequeue(bullet) as Laser;
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
