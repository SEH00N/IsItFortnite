using System.Collections;
using UnityEngine;

public class SquareEnemy : Enemy, IDamageable
{
    [SerializeField] float fireDelay = 1f;

    public void OnDamage(float dmg)
    {
        //State가 Damaged면 return
        if (EnemyState.Instance.state.HasFlag(EnemyState.State.Damaged)) return;

        //enum(State) 업데이트
        EnemyState.Instance.state |= EnemyState.State.Damaged;

        currentHP -= dmg;

        StartCoroutine(KnockBack());

        if (currentHP <= 0)
            PoolManager.Instance.Enqueue(this);
    }

    private void Start()
    {
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
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
            if(EnemyState.Instance.state.HasFlag(EnemyState.State.Damaged)) break;

            //enum(State) 업데이트
            EnemyState.Instance.state |= EnemyState.State.Fire;

            yield return new WaitForSeconds(fireDelay);
            SquareBullet temp =  PoolManager.Instance.Dequeue("SquareBullet") as SquareBullet;
            temp.transform.position = lookAt.position;
            temp.transform.rotation = transform.rotation;
        }
    }
}
