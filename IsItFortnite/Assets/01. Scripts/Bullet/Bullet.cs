using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] protected List<string> tagName = new List<string>();
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float damage = 5f;
    [SerializeField] protected float lifeTime = 1f;
    protected float currentTime = 0;

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void Update()
    {
        float time = Time.deltaTime;

        currentTime += time;

        transform.Translate(Vector3.up * speed * time);

        DeSpawn();
    }

    /// <summary>
    /// 화면 밖으로 나가거나 경과시간이 lifeTime을 넘으면 Enqueue
    /// </summary>
    protected void DeSpawn()
    {
        Vector2 pos = transform.position;
        Vector2 minPos = GameManager.Instance.minPos.position;
        Vector2 maxPos = GameManager.Instance.maxPos.position;

        if (pos.x > maxPos.x || pos.x < minPos.x || pos.y > maxPos.y || pos.y < minPos.y || currentTime > lifeTime)
            PoolManager.Instance.Enqueue(this);
    }

    /// <summary>
    /// 콜라이더가 tagName과 닿았을 때 tagNamge의 OnDamage인터페이스 호출 and Enqueue
    /// </summary>
    protected void IsNear(Collision2D other)
    {
        if(other.gameObject.CompareTag(tagName[0]))
        {
            PoolManager.Instance.Enqueue(this);
            IDamageable id = other.gameObject.GetComponent<IDamageable>();
            if (id != null)
                id.OnDamage(damage);
        }
    }
}
