using System;
using System.Collections;
using UnityEngine;

public class AsteriskEnemy : Enemy, IDamageable
{
    [SerializeField] GameObject bombCollider;
    [SerializeField] float bombDelay = 1f;

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (state.HasFlag(EnemyState.State.Damaged)) return;

        freeze?.Invoke();

        currentHP -= dmg;

        //스코어 증가
        GameManager.Instance.SetScore((maxHP - currentHP));

        StartCoroutine(Bomb());
    }

    protected override void Update()
    {
        base.Update();
        Movement();
    }

    /// <summary>
    /// 폭발
    /// </summary>
    private IEnumerator Bomb()
    {
        //enum(State) 업데이트
        state |= EnemyState.State.Damaged;

        lightObj.SetActive(false);
        yield return new WaitForSeconds(bombDelay / 3f);
        lightObj.SetActive(true);
        yield return new WaitForSeconds(bombDelay / 3f);
        lightObj.SetActive(false);
        yield return new WaitForSeconds(bombDelay / 3f);
        lightObj.SetActive(true);

        bombCollider.SetActive(true);
        yield return new WaitForSeconds(bombDelay / 3);
        bombCollider.SetActive(false);

        //enum(State) 업데이트
        state &= ~EnemyState.State.Damaged;

        gameObject.SetActive(false);

        PoolManager.Instance.Enqueue(this);
        yield return null;
    }

    /// <summary>
    /// 움직임
    /// </summary>
    private void Movement()
    {
        //State가 Damaged면 return
        if (state.HasFlag(EnemyState.State.Damaged)) return;

        //enum(State) 업데이트
        state |= EnemyState.State.Move;

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}