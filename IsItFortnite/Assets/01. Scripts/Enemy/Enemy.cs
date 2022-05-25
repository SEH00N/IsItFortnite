using System.Collections;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] protected Transform player;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float maxHP = 5f;
    [SerializeField] protected float patrolDelay = 5f;
    [SerializeField] protected float patrolDistance = 5f;
    [SerializeField] protected float currentHP = 0;
    protected Rigidbody2D rb2d = null;
    protected Collider2D col2d = null;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        Rotation();
    }

    public override void Reset()
    {
        //체력 초기화
        currentHP = maxHP;
        //순찰 시작
        StartCoroutine(Patrol());
    }

    /// <summary>
    /// 플레이어 주변 순찰
    /// </summary>
    protected IEnumerator Patrol()
    {
        while (true)
        {
            float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
            //반지름이 patrolDistance인 원주의 임의의 점을 구함
            Vector3 randPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * patrolDistance;
            //임의의 점을 기준으로 방향 설정
            Vector3 dir = (player.position + randPos - transform.position).normalized;

            rb2d.velocity = dir * speed;

            yield return new WaitForSeconds(patrolDelay);
        }
    }

    /// <summary>
    /// 플레이어 바라보기
    /// </summary>
    protected void Rotation()
    {
        Vector3 rotate = transform.eulerAngles;
        Vector3 dir = (transform.position - player.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90f;

        transform.rotation = Quaternion.Euler(rotate);
    }
}
