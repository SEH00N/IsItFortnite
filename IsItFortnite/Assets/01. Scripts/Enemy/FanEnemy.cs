using System;
using UnityEngine;

public class FanEnemy : Enemy, IDamageable
{
    private float currentTime = 0;

    public void OnDamage(float dmg, Action freeze = null)
    {
        freeze?.Invoke();

        currentHP -= dmg;

        //스코어 증가
        GameManager.Instance.SetScore((maxHP - currentHP));

        //한 대 맞으면 사망
        PoolManager.Instance.Enqueue(this);
    }

    public override void Reset()
    {
        base.Reset();

        currentTime = 0;

        //플레이어 바라보기
        Vector3 rotate = transform.eulerAngles;
        Vector3 dir = (transform.position - player.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90f;

        transform.rotation = Quaternion.Euler(rotate);
    }

    protected override void Update()
    {
        //한 방향으로 움직임
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        currentTime += Time.deltaTime;
        //3초 뒤 사망
        if(currentTime >= 3f)
            PoolManager.Instance.Enqueue(this);
    }
}
