using UnityEngine;

public class Laser : Bullet
{
    public override void Reset()
    {
        currentTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Enemy태그에 닿으면 Enqueue, OnDamage
        IsNear(other);
    }
}
