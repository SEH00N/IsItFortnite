using System.Collections;
using UnityEngine;

public class SquareEnemy : Enemy, IDamageable
{
    [SerializeField] float fireDelay = 1f;

    public void OnDamage(float dmg)
    {
        //State가 Damaged면 return
        if (state.HasFlag(EnemyState.State.Damaged)) return;

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

    /// <summary>
    /// 플레이어를 향해 총알 발사
    /// </summary>
    private IEnumerator Fire()
    {
        while (true)
        {
            //State가 Damaged면 break
            if (state.HasFlag(EnemyState.State.Damaged)) break;


            //순찰 범위 안에 플레이어가 있으면 발사
            if(IsNear())
            {
                //enum(State) 업데이트
                state |= EnemyState.State.Fire;

                SquareBullet temp =  PoolManager.Instance.Dequeue("SquareBullet") as SquareBullet;
                temp.transform.position = lookAt.position;
                temp.transform.rotation = transform.rotation;
            }

            yield return new WaitForSeconds(fireDelay);

            //enum(State) 업데이트
            state &= ~EnemyState.State.Fire;
        }
    }
}
