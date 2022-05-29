using System;
using System.Collections;
using UnityEngine;

public class CircleEnemy : Enemy, IDamageable
{
    [SerializeField] Transform firePos;
    [SerializeField] float fireDelay = 1f;
    [SerializeField] int fireCount = 8;

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (state.HasFlag(EnemyState.State.Damaged)) return;

        freeze?.Invoke();

        //enum(State) 업데이트
        state |= EnemyState.State.Damaged;

        StartCoroutine(Twinkle());

        currentHP -= dmg;

        //스코어 증가
        GameManager.Instance.SetScore((maxHP - currentHP));

        StartCoroutine(KnockBack());
    }

    public override void Reset()
    {
        base.Reset();
        StartCoroutine(Patrol());
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(fireDelay);

        while (true)
        {
            //State가 Damaged면 break
            if (state.HasFlag(EnemyState.State.Damaged)) break;


            //순찰 범위 안에 플레이어가 있으면 발사
            if (IsNear())
            {
                //enum(State) 업데이트
                state |= EnemyState.State.Fire;

                //8방향으로 방사형 발사
                for (int i = 0; i < fireCount; i++)
                {
                    CircleBullet temp = PoolManager.Instance.Dequeue("CircleBullet") as CircleBullet;
                    temp.transform.position = firePos.position;
                    temp.transform.rotation = Quaternion.Euler(0, 0, i * 360 / fireCount);
                }
            }

            yield return new WaitForSeconds(fireDelay);

            //enum(State) 업데이트
            state &= ~EnemyState.State.Fire;
        }
    }
}
