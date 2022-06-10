using System.Collections;
using UnityEngine;


public class FinterBullet : Bullet
{
    public enum State
    {
        Following = 0,
        Up,
    }
    public State state = State.Following;

    [SerializeField] float followingDuration = 3f;

    public override void Reset()
    {
        StartCoroutine(SetState());
    }

    protected override void Update()
    {
        float time = Time.deltaTime;

        currentTime += time;

        // State가 Following일 때 플레이어 바라보기
        if(state == State.Following)
            ChangeRotate();
        transform.Translate(Vector3.up * time * speed);

        DeSpawn();
    }

    /// <summary>
    /// 상태 전환 (Follow -> Up)
    /// </summary>
    private IEnumerator SetState()
    {
        state = State.Following;
        yield return new WaitForSeconds(followingDuration);
        state = State.Up;
    }

    /// <summary>
    /// 총알 방향 전환
    /// </summary>
    private void ChangeRotate()
    {
        Vector3 playerPos = GameManager.Instance.player.position;
        Vector3 rotate = transform.eulerAngles;
        Vector2 rotateDir = (transform.position - playerPos).normalized;
        float angle = Mathf.Atan2(rotateDir.y, rotateDir.x) * Mathf.Rad2Deg;
        rotate.z = angle + 90;
        transform.rotation = Quaternion.Euler(rotate);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player태그에 닿으면 Dequeue, OnDamage
        IsNear(other);
    }
}
