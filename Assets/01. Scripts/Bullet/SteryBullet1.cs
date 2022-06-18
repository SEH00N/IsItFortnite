using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteryBullet1 : Bullet
{   
    [SerializeField] PoolableMono subBullet;
    [SerializeField] int count = 5;

    protected override void Update()
    {
        currentTime += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Split();
    }

    private void Split()
    {
        if(currentTime >= lifeTime)
        {
            for(int i = 0; i < count; i ++)
            {
                PoolableMono temp = PoolManager.Instance.Dequeue(subBullet) as PoolableMono;
                temp.transform.eulerAngles = new Vector3(0, 0, 360 / count * i);
                temp.transform.position = transform.position;
            }

            DeSpawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IsNear(other);
    }
}
