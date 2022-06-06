using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letis : Enemy, IDamageable
{
    [SerializeField] float sleepDelay = 7f;
    [SerializeField] float sleepDuration = 2f;
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
    }

    public override void Reset()
    {
        base.Reset();
        StartCoroutine(Patrol());
    }

    protected override void Update()
    {
        nearTime += Time.deltaTime;

        Sleeping();
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
