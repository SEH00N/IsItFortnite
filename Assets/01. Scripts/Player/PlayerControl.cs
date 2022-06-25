using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Character
{
    public List<PoolableMono> bulletList;
    [SerializeField] PoolableMono fireAudio;
    [SerializeField] List<Transform> firePos;
    [SerializeField] Transform minTrm;
    [SerializeField] Transform maxTrm;
    [SerializeField] Transform lookAt;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float fireDelay;
    private Camera cam = null;
    private float currentTime = 0;
    private int c = 0;
    public int bulletIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        bulletIndex = Mathf.Clamp(bulletIndex, 0, bulletList.Count - 1);
        Blocking();
        if(stateEnum.state.HasFlag(State.Ulti)) return;
        PlayerRotation(mousePos);
        Controlling(mousePos);

    }

    /// <summary>
    /// (플레이어 컨트롤) 땅 우클릭 시 움직임, 적 우클릭 시 발사
    /// </summary>
    private void Controlling(Vector3 mousePos)
    {
        currentTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            //State가 Damaged면 return
            if(stateEnum.state.HasFlag(State.Damaged)) return;

            //카메라에서 마우스 위치로 쏜 레이캐스트가 enemyLayer와 닿으면 true
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, enemyLayer);

            if (hit) PlayerFire(); //적을 감지하면 발사
            else PlayerMovement(mousePos); //적이 아닌 것을 감지하면 움직임
        }
    }

    /// <summary>
    /// 플레이어 레이저 발사
    /// </summary>
    private void PlayerFire()
    {
        Vector3 rotate = lookAt.eulerAngles;

        //딜레이 부여
        if (currentTime > fireDelay)
        {
            //enum(State) 업데이트
            stateEnum.state |= State.Fire;

            Vector3 pos = transform.position;

            //총알 풀링
            PoolableMono bullet = PoolManager.Instance.Dequeue(bulletList[bulletIndex]) as PoolableMono;

            //총알 위치 초기화
            bullet.transform.position = firePos[c].position;

            //총알의 각도 초기화(플레이어의 각도)
            bullet.transform.rotation = Quaternion.Euler(rotate);

            PoolManager.Instance.Dequeue(fireAudio);

            //딜레이 시간 초기화
            currentTime = 0;

            if(c >= firePos.Count - 1)
                c = 0;
            else
                c++;
        }
    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    private void PlayerMovement(Vector3 mousePos)
    {
        //enu(State) 업데이트
        stateEnum.state = State.Move;

        Vector3 pos = transform.position;

        Vector2 dir = (mousePos - pos).normalized;
        rb2d.velocity = dir * speed;
    }

    /// <summary>
    /// 플레이어 각도 전환
    /// </summary>
    private void PlayerRotation(Vector3 mousePos)
    {
        Vector3 rotate = lookAt.eulerAngles;
        Vector2 rotateDir = (transform.position - mousePos).normalized;
        float angle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90f;
        lookAt.rotation = Quaternion.Euler(rotate);
    }

    /// <summary>
    /// 화면 탈출 방지
    /// </summary>
    private void Blocking()
    {
        Vector2 minPos = minTrm.position;
        Vector2 maxPos = maxTrm.position;
        Vector2 pos = transform.position;

        Vector3 limitPos = new Vector3(Mathf.Clamp(pos.x, minPos.x, maxPos.x), Mathf.Clamp(pos.y, minPos.y, maxPos.y));
        transform.position = limitPos;
    }
}
