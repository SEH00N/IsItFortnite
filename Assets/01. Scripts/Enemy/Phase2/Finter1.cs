using System;
using System.Collections;
using UnityEngine;

public class Finter1 : Enemy, IDamageable
{
    [SerializeField] Transform firePos1;
    [SerializeField] PoolableMono bullet;
    [SerializeField] float fireDelay = 1f;

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
        yield return new WaitForSeconds(fireDelay);

        while (true)
        {
            //State가 Damaged면 break
            if (stateEnum.state.HasFlag(State.Damaged) || stateEnum.state.HasFlag(State.Stop)) break;

            //순찰 범위 안에 플레이어가 있으면 발사
            if(IsNear())
            {
                //enum(State) 업데이트
                stateEnum.state |= State.Fire;

                FinterBullet temp =  PoolManager.Instance.Dequeue(bullet) as FinterBullet;
                FinterBullet temp1 =  PoolManager.Instance.Dequeue(bullet) as FinterBullet;
                temp1.transform.position = firePos1.position;
                temp1.transform.rotation = transform.rotation;
                temp.transform.position = lookAt.position;
                temp.transform.rotation = transform.rotation;
            }

            yield return new WaitForSeconds(fireDelay);
            
            //enum(State) 업데이트
            stateEnum.state &= ~State.Fire;
        }
    }
}
