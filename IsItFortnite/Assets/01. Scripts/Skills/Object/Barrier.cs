using UnityEngine;

public class Barrier : PoolableMono
{
    [SerializeField] float lifeTime = 5f;
    private Transform playerTrm;
    private float currentTime = 0;
    private float time = 0;

    private void Start()
    {
        playerTrm = GameManager.Instance.player;
    }

    private void Update()
    {
        time = Time.deltaTime;
        currentTime += time;

        transform.position = playerTrm.position;

        DeSpawn();
    }

    public override void Reset()
    {
        currentTime = 0;
    }

    private void DeSpawn()
    {
        if(currentTime > lifeTime)
            PoolManager.Instance.Enqueue(this);
    }
}
