using System;
using UnityEngine;

public class Striker : Enemy, IDamageable
{
    [SerializeField] float damage = 2f;

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

    protected override void Update()
    {
        if(stateEnum.state.HasFlag(State.Damaged)) return;

        
        base.Update();
        stateEnum.state |= State.Move;
        Vector2 dir = (player.position - transform.position).normalized;
        rb2d.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IDamageable id = other.gameObject.GetComponent<IDamageable>();
            if (id != null)
                id.OnDamage(damage);
        }
    }
}
