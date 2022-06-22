using System;
using System.Collections;
using UnityEngine;

public class GGP : Enemy, IDamageable
{
    [SerializeField] PoolableMono bullet;
    [SerializeField] float fireDelay = 3f;

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (stateEnum.state.HasFlag(State.Damaged)) return;

        //enum(State) 업데이트
        stateEnum.state |= State.Damaged;

        freeze?.Invoke();

        StartCoroutine(Twinkle());

        currentHP -= dmg;

        //스코어 증가
        GameManager.Instance.SetScore((maxHP - currentHP));

        StartCoroutine(KnockBack());
        stateEnum.state = State.Idle;
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
            if (stateEnum.state.HasFlag(State.Damaged)|| stateEnum.state.HasFlag(State.Stop)) break;

            //순찰 범위 안에 플레이어가 있으면 발사
            if (IsNear())
            {
                //enum(State) 업데이트
                stateEnum.state |= State.Fire;

                GGPBullet temp = PoolManager.Instance.Dequeue(bullet) as GGPBullet;
                temp.transform.position = lookAt.position;
                temp.transform.rotation = transform.rotation;
            }

            yield return new WaitForSeconds(fireDelay);

            //enum(State) 업데이트
            stateEnum.state &= ~State.Fire;
        }
    }
}
