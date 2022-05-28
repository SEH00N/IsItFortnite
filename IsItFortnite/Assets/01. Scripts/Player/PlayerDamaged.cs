using System.Collections;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour, IDamageable
{
    [SerializeField] Transform lookAt;
    [SerializeField] float hp = 10f;
    [SerializeField] float knockBackDuration = 0.5f;
    [SerializeField] float knockBackPwr = 5f;
    [SerializeField] int damage = 1;
    private Rigidbody2D rb2d = null;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            IDamageable id = other.gameObject.GetComponent<IDamageable>();
            OnDamage(damage);
            if (id != null)
                id.OnDamage(damage);
        }
    }

    public void OnDamage(float dmg)
    {
        //State가 Damaged면 return
        if (PlayerState.Instance.state.HasFlag(PlayerState.State.Damaged)) return;

        //enum(State) 업데이트
        PlayerState.Instance.state |= PlayerState.State.Damaged;
        
        hp -= dmg;

        StartCoroutine(KnockBack());

        if(hp <= 0)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// 플레이어 넉백
    /// </summary>
    private IEnumerator KnockBack()
    {
        //넉백 방향 구하기
        Vector2 dir = (lookAt.position - transform.position).normalized;

        //뒤로 knockBackPwr만큼 넉백
        rb2d.AddForce(-dir * knockBackPwr, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockBackDuration);

        //State Flag에서 Damaged 제거
        PlayerState.Instance.state &= ~PlayerState.State.Damaged;
    }
}
