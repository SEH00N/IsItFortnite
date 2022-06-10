using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGPBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player태그에 닿으면 Dequeue, OnDamage
        IsNear(other);
    }

    protected override void IsNear(Collision2D other)
    {
        if(other.gameObject.CompareTag(tagName[0]))
        {
            IDamageable id = other.gameObject.GetComponent<IDamageable>();
            float knockBackPwr = other.gameObject.GetComponent<PlayerDamaged>().knockBackPwr;
            other.gameObject.GetComponent<PlayerDamaged>().knockBackPwr = knockBackPwr * 5;
            
            if (id != null)
                id.OnDamage(damage, () => {
                    TimeController.Instance.ModifyTimeScale(0.1f, 0.1f, () => {
                        TimeController.Instance.ModifyTimeScale(1f, 0.05f);
                    });

                    other.gameObject.GetComponent<PlayerDamaged>().knockBackPwr = knockBackPwr;
                });
            PoolManager.Instance.Enqueue(this);
        }
    }
}
