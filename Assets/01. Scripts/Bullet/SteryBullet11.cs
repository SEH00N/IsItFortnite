using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteryBullet11 : Bullet
{   
    private void OnCollisionEnter2D(Collision2D other)
    {
        IsNear(other);
    }
}
