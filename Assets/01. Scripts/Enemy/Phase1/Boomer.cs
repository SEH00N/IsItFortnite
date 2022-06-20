using System;
using System.Collections;
using UnityEngine;

public class Boomer : Enemy, IDamageable
{
    [SerializeField] GameObject bombCollider;
    [SerializeField] float bombDelay = 1f;

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (stateEnum.state.HasFlag(State.Damaged)) return;

        freeze?.Invoke();

        currentHP -= dmg;

        //스코어 증가
        GameManager.Instance.SetScore((maxHP - currentHP));

        StartCoroutine(Twinkle());
        StartCoroutine(KnockBack());
        if (currentHP <= 0)
            StartCoroutine(Bomb());
    }

    public override void Reset()
    {
        base.Reset();
        lightObj.SetActive(true);
    }

    protected override IEnumerator Twinkle()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(knockBackDuration);
        sp.color = Color.white;
        yield return new WaitForSeconds(knockBackDuration);
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
        stateEnum.state |= State.Damaged;

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
        stateEnum.state &= ~State.Damaged;

        DropItem(powerUp);
        DropItem(healPack);

        PoolManager.Instance.Enqueue(this);
        yield return null;
    }

    /// <summary>
    /// 움직임
    /// </summary>
    private void Movement()
    {
        //State가 Damaged면 return
        if (stateEnum.state.HasFlag(State.Damaged)) return;

        //enum(State) 업데이트
        stateEnum.state |= State.Move;

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
