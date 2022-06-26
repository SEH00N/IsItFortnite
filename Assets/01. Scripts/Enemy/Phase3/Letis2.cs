using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letis2 : Enemy, IDamageable
{
    [SerializeField] float sleepDistance = 7f;
    [SerializeField] float sleepDelay = 7f;
    [SerializeField] float sleepDuration = 2f;
    [SerializeField] float slowedSpeed = 11f;
    private float firstSpeed = 0;
    private float nearTime = 0f;
    private float stunTime = 0f;

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (stateEnum.state.HasFlag(State.Damaged)) return;

        //enum(State) 업데이트
        stateEnum.state |= State.Damaged;

        rb2d.velocity = Vector2.zero;

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
        pc = GameManager.Instance.player.GetComponent<PlayerControl>();
        firstSpeed = pc.speed;
    }

    protected override void Update()
    {
        if(OnNear())
            nearTime += Time.deltaTime;

        Sleeping();

        if(OnNear())
            pc.speed = slowedSpeed;
        else if(!OnNear())
            pc.speed = firstSpeed;
    }

    private bool OnNear()
    {
        return Physics2D.OverlapCircle(col2d.bounds.center, sleepDistance, playerLayer);
    }

    private void Sleeping()
    {
        if (nearTime > sleepDelay)
        {
            stunTime = 0;
            nearTime = 0;

            player.gameObject.GetComponent<IDamageable>().OnDamage(0, () =>
            {
                TimeController.Instance.ModifyTimeScale(0.1f, 0.1f, () =>
                {
                    TimeController.Instance.ModifyTimeScale(1f, 0.05f);
                });
            });

            StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        while (stunTime < sleepDuration)
        {
            player.gameObject.GetComponent<PlayerControl>().rb2d.velocity = Vector2.zero;

            stunTime += Time.deltaTime;

            yield return null;
        }
    }
}
