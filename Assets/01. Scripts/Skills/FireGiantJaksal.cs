using UnityEngine;

public class FireGiantJaksal : SkillBase
{
    [SerializeField] PoolableMono fireAudio;
    [SerializeField] PoolableMono bullet;
    private CamControl cc = null;

    private void Awake()
    {
        cc = GameObject.Find("CM vcam1").GetComponent<CamControl>();
    }

    public void GiantJaksal()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
        {
            cc.Shake(100, 0.5f);
            PoolManager.Instance.Dequeue(fireAudio);
            PoolableMono cb = PoolManager.Instance.Dequeue(bullet);
            cb.transform.position = firePos.position;
            cb.transform.rotation = lookAt.rotation;
            coolDown = 0;
        }
    }
}
