using UnityEngine;

public class FanEnemy : Enemy, IDamageable
{
    public void OnDamage(float dmg)
    {
        //한 대 맞으면 사망
        PoolManager.Instance.Enqueue(this);
    }

    protected override void Start()
    {
        base.Start();
        Reset();
    }

    public override void Reset()
    {
        //플레이어 바라보기
        Vector3 rotate = transform.eulerAngles;
        Vector3 dir = (transform.position - player.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90;
        transform.rotation = Quaternion.Euler(rotate);
    }

    protected override void Update()
    {
        //한 방향으로 움직임
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
