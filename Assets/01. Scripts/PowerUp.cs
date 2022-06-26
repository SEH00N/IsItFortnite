using UnityEngine;

public class PowerUp : PoolableMono
{
    [SerializeField] PoolableMono audioPrefab;
    [SerializeField] Sprite sprite;

    public override void Reset()
    {
        
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PoolManager.Instance.Dequeue(audioPrefab);
            other.GetComponent<PlayerControl>().bulletIndex++;
            if(other.GetComponent<PlayerControl>().bulletIndex == 5)
                other.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
            PoolManager.Instance.Enqueue(this);
        }
    }
}
