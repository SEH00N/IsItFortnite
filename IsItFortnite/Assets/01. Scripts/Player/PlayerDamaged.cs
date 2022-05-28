using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerDamaged : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject fadeCavas;
    [SerializeField] Transform lookAt;
    [SerializeField] Image fadeImage;
    [SerializeField] Image hpImage;
    [SerializeField] float hp = 10f;
    [SerializeField] float knockBackDuration = 0.5f;
    [SerializeField] float knockBackPwr = 5f;
    [SerializeField] int damage = 1;
    private Rigidbody2D rb2d = null;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hpImage.fillAmount = hp / 6;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
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

        StartCoroutine(Twinkle());
        StartCoroutine(KnockBack());
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

    /// <summary>
    /// 빛 깜빡임
    /// </summary>
    private IEnumerator Twinkle()
    {
        lightObj.SetActive(false);
        yield return new WaitForSeconds(knockBackDuration);
        lightObj.SetActive(true);
        yield return new WaitForSeconds(knockBackDuration);
        lightObj.SetActive(false);
        yield return new WaitForSeconds(knockBackDuration);
        lightObj.SetActive(true);

        if (hp <= 0)
        {
            StartCoroutine(Twinkle());
            yield return new WaitForSeconds(knockBackDuration);

            GameManager.Instance.pooler.SetActive(false);
            yield return new WaitForSeconds(knockBackDuration);

            GameOver();
        }
    }

    /// <summary>
    /// 게임 오버 연출
    /// </summary>
    private void GameOver()
    {
        GameManager.Instance.SaveScore();

        fadeCavas.SetActive(true);
        fadeImage.DOFade(1, 3f).OnComplete(() => {
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        });
    }
}
