using UnityEngine;

public class Laser : Bullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Enemy태그에 닿으면 Enqueue, OnDamage
        IsNear(other);
    }
}
