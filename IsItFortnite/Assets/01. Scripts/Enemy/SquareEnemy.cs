using System.Collections;
using UnityEngine;

public class SquareEnemy : Enemy, IDamageable
{
    [SerializeField] Transform firePos;
    [SerializeField] float fireDelay = 1f;

    public void OnDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            PoolManager.Instance.Enqueue(this);
    }

    private void Start()
    {
        Reset();
    }

    public override void Reset()
    {
        base.Reset();
        StartCoroutine(Fire());
    }

    /// <summary>
    /// 플레이어를 향해 총알 발사
    /// </summary>
    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            SquareBullet temp =  PoolManager.Instance.Dequeue("SquareBullet") as SquareBullet;
            temp.transform.position = firePos.position;
            temp.transform.rotation = transform.rotation;
        }
    }
}
