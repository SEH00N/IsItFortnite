using System.Collections;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] protected PoolableMono healPack;
    [SerializeField] protected PoolableMono powerUp;
    [SerializeField] protected GameObject lightObj;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Transform lookAt;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float maxHP = 5f;
    [SerializeField] protected float patrolDelay = 5f;
    [SerializeField] protected float patrolDistance = 5f;
    [SerializeField] protected float knockBackDuration = 0.5f;
    [SerializeField] protected float knockBackPwr = 5f;
    protected SpriteRenderer sp = null;
    protected Rigidbody2D rb2d = null;
    protected Collider2D col2d = null;
    public float currentHP = 0;
    public StateEnum stateEnum = null;
    protected Transform player;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
        stateEnum = GetComponent<StateEnum>();
    }

    protected virtual void Update()
    {
        if(player.GetComponent<PlayerControl>().stateEnum.state.HasFlag(State.Ulti))
        {
            stateEnum.state |= State.Stop;
            rb2d.velocity = Vector2.zero;
        }
        if(!player.GetComponent<PlayerControl>().stateEnum.state.HasFlag(State.Ulti))
            stateEnum.state &= ~State.Stop;

        Rotation();
    }

    public override void Reset()
    {
        //player Transform 캐싱
        player = GameManager.Instance.player.transform;

        //위치 초기화
        transform.position = EnemySpawner.Instance.randPos;

        //enum(State) 초기화
        stateEnum.state = State.Idle;

        //체력 초기화
        currentHP = maxHP;

        sp.color = Color.white;
    }

    /// <summary>
    /// 플레이어 주변 순찰
    /// </summary>
    protected IEnumerator Patrol()
    {
        while (true)
        {
            //State가 Damaged면 break
            if (!stateEnum.state.HasFlag(State.Damaged))
            {
                //enum(State) 업데이트
                stateEnum.state |= State.Move;

                float angle = Random.Range(0, 360f) * Mathf.Rad2Deg;
                //반지름이 patrolDistance인 원주의 임의의 점을 구함
                Vector3 randPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * patrolDistance;
                //임의의 점을 기준으로 방향 설정
                Vector3 dir = (player.position + randPos - transform.position).normalized;

                rb2d.velocity = dir * speed;

                yield return new WaitForSeconds(patrolDelay);
            }
            yield return null;
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

    /// <summary>
    /// 에너미 넉백
    /// </summary>
    protected IEnumerator KnockBack()
    {
        //넉백 방향 구하기
        Vector2 dir = (lookAt.position - transform.position).normalized;

        //뒤로 knockBackPwr만큼 넉백
        rb2d.AddForce(-dir * knockBackPwr, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackDuration);

        rb2d.velocity = Vector2.zero;

        //State Flag에서 Damaged 제거
        stateEnum.state &= ~State.Damaged;
    }

    /// <summary>
    /// 순찰 범위 내 플레이어 감지
    /// </summary>
    protected bool IsNear()
    {
        return Physics2D.OverlapCircle(col2d.bounds.center, patrolDistance * 2, playerLayer);
    }

    /// <summary>
    /// 빛 깜빡임
    /// </summary>
    protected virtual IEnumerator Twinkle()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(knockBackDuration);
        sp.color = Color.white;
        yield return new WaitForSeconds(knockBackDuration);


        if (currentHP <= 0)
        {
            StopAllCoroutines();
            DropItem(powerUp);
            DropItem(healPack);
            PoolManager.Instance.Enqueue(this);
        }
    }

    protected void DropItem(PoolableMono item)
    {
        float randVal = Random.Range(0, 100);
        if (randVal >= 98)
        {
            PoolableMono temp = PoolManager.Instance.Dequeue(item);
            temp.transform.position = transform.position;
        }
    }
}
