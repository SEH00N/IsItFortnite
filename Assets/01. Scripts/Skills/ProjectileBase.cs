using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : PoolableMono
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float lifeTime = 5f;
    protected float currentTime = 0f;
    protected float time = 0f;

    protected virtual void Update()
    {
        time = Time.deltaTime;
        DeSpawn();
        Movement();
    }

    private void Movement()
    {
        //위쪽으로 이동
        transform.Translate(Vector3.up * speed * time);
    }

    private void DeSpawn()
    {
        currentTime += time;

        //lifeTime이 지나면 디스폰
        if(currentTime >= lifeTime)
            PoolManager.Instance.Enqueue(this);
    }

    public override void Reset()
    {
        //currentTime 초기화
        currentTime = 0;
    }
}
