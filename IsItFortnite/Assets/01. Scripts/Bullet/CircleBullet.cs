using UnityEngine;

public class CircleBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player태그에 닿으면 Dequeue, OnDamage
        IsNear(other);
    }
}
