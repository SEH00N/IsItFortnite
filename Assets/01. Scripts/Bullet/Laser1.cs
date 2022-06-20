using UnityEngine;

public class Laser1 : Bullet
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(tagName[0]))
        {
            IDamageable id = other.GetComponent<IDamageable>();
            if (id != null)
                id.OnDamage(damage, () => {
                    TimeController.Instance.ModifyTimeScale(0.1f, 0.1f, () => {
                        TimeController.Instance.ModifyTimeScale(1f, 0.05f);
                    });
                });
        }
    }
}
