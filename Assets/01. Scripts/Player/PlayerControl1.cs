using UnityEngine;

public class PlayerControl1 : Character
{
    [SerializeField] Transform firePos;
    [SerializeField] Transform lookAt;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] PoolableMono bullet;
    [SerializeField] float fireDelay = 0.5f;
    [SerializeField] float jumpDelay = 0.5f;
    [SerializeField] float jumpPwr = 5f;
    private Vector3 rotate;
    private Camera cam = null;
    private float jumpCooldown = 0;
    private float fireCooldown = 0;

    protected override void Awake()
    {
        base.Awake();
        rotate = lookAt.eulerAngles;
        cam = Camera.main;
    }

    private void Update()
    {
        jumpCooldown += Time.deltaTime;
        fireCooldown += Time.deltaTime;
        Movement();
        Fire();
        Down();
        Jump();
    }

    private void Movement()
    {
        if (stateEnum.state.HasFlag(State.Damaged)) return;
        stateEnum.state |= State.Move;

        float x = Input.GetAxisRaw("Horizontal");

        Vector2 dir = new Vector2(x * speed, rb2d.velocity.y);
        rb2d.velocity = dir;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCooldown > jumpDelay)
        {
            if (stateEnum.state.HasFlag(State.Damaged)) return;
            stateEnum.state |= State.Move;

            rb2d.AddForce(Vector2.up * jumpPwr, ForceMode2D.Impulse);
            jumpCooldown = 0;
        }

        Vector2 limit = new Vector2(rb2d.velocity.x, Mathf.Clamp(rb2d.velocity.y, -(jumpPwr / 3), jumpPwr));
        rb2d.velocity = limit;
    }

    private void Down()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stateEnum.state.HasFlag(State.Damaged)) return;
            stateEnum.state |= State.Move;

            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //State가 Damaged면 return
            if (stateEnum.state.HasFlag(State.Damaged)) return;

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            //카메라에서 마우스 위치로 쏜 레이캐스트가 enemyLayer와 닿으면 true
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, enemyLayer);

            if (hit && fireCooldown > fireDelay)
            {
                //각도 전환
                PlayerRotation(mousePos);

                stateEnum.state |= State.Fire;

                //총알 풀링
                PoolableMono temp = PoolManager.Instance.Dequeue(bullet) as PoolableMono;

                //총알 위치 초기화
                temp.transform.position = firePos.position;

                //총알의 각도 초기화(플레이어의 각도)
                temp.transform.rotation = Quaternion.Euler(rotate);

                fireCooldown = 0;
            }
        }
    }

    private void PlayerRotation(Vector3 mousePos)
    {
        Vector2 rotateDir = (transform.position - mousePos).normalized;
        float angle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90f;
        lookAt.rotation = Quaternion.Euler(rotate);
    }
}
