using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteryBullet21 : Bullet
{   
    [SerializeField] GameObject boom;
    [SerializeField] float boomDuration = 0.5f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        IsNear(other);
    }

    protected override void Update()
    {
        currentTime += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(currentTime >= lifeTime)
            StartCoroutine(Split());
    }

    private IEnumerator Split()
    {
        if(currentTime >= lifeTime)
        {
            boom.SetActive(true);
            yield return new WaitForSeconds(boomDuration);
            DeSpawn();
        }
    }

    protected override void IsNear(Collision2D other)
    {
        base.IsNear(other);
    }
}
