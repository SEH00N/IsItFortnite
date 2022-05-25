using UnityEngine;

public class SquareBullet : Bullet
{
    public override void Reset()
    {
        currentTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player태그에 닿으면 Dequeue, OnDamage
        IsNear(other);
    }
}
