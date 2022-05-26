using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : Enemy, IDamageable
{
    [SerializeField] float fireDelay = 1f;

    public void OnDamage(float dmg)
    {
        //State가 Damaged면 return
        if (state.HasFlag(EnemyState.State.Damaged)) return;

        //enum(State) 업데이트
        state |= EnemyState.State.Damaged;

        currentHP -= dmg;

        StartCoroutine(KnockBack());

        if (currentHP <= 0)
            PoolManager.Instance.Enqueue(this);
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            //State가 Damaged면 break
            if(state.HasFlag(EnemyState.State.Damaged)) break;

            //enum(State) 업데이트
            state |= EnemyState.State.Fire;

            yield return new WaitForSeconds(fireDelay);
            SquareBullet temp =  PoolManager.Instance.Dequeue("SquareBullet") as SquareBullet;
            temp.transform.position = lookAt.position;
            temp.transform.rotation = transform.rotation;
        }
    }
}
