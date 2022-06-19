using UnityEngine;

public class PowerUp : PoolableMono
{
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
            other.GetComponent<PlayerControl>().bulletIndex++;
            PoolManager.Instance.Enqueue(this);
        }
    }
}
