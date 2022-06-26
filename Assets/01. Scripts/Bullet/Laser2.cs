using UnityEngine;

public class Laser2 : Bullet
{
    [SerializeField] PoolableMono exp;

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Enemy태그에 닿으면 Enqueue, OnDamage
        IsNear(other);
    }

    protected override void IsNear(Collision2D other)
    {
        if(other.gameObject.CompareTag(tagName[0]))
        {
            IDamageable id = other.gameObject.GetComponent<IDamageable>();
            if (id != null)
                id.OnDamage(damage, () => {
                    TimeController.Instance.ModifyTimeScale(0.1f, 0.1f, () => {
                        TimeController.Instance.ModifyTimeScale(1f, 0.05f);
                    });
                });
            PoolableMono temp = PoolManager.Instance.Dequeue(exp);
            temp.transform.position = transform.position;
            PoolManager.Instance.Enqueue(this);
        }
    }
}
