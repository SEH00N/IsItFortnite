using UnityEngine;

public class HealPack : PoolableMono
{
    [SerializeField] float healAmount = 10f;

    public override void Reset()
    {

    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamaged pd = other.GetComponent<PlayerDamaged>();
            if (pd.currentHP < pd.maxHP - healAmount)
                pd.currentHP += healAmount;
            else pd.currentHP = pd.maxHP;

            PoolManager.Instance.Enqueue(this);
        }
    }
}
