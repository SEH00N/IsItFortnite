using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class PlayerDamaged : Character, IDamageable
{
    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject fadeCavas;
    [SerializeField] Transform lookAt;
    [SerializeField] Image fadeImage;
    [SerializeField] Image hpImage;
    [SerializeField] float maxHP = 10f;
    [SerializeField] int damage = 1;
    private SpriteRenderer sp = null;
    private float currentHP = 0;
    private float twinkleDuration = 0.3f;
    public float knockBackDuration = 0.5f;
    public float knockBackPwr = 5f;
    public bool isSlow = false;
    public bool isPoison = false;

    private void OnEnable()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
        currentHP = maxHP;
    }

    private void Update()
    {
        hpImage.fillAmount = currentHP / maxHP;

        if(isSlow)
            StartCoroutine(Slow(4, 2));
        if(isPoison)
            StartCoroutine(Poison(5));
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

    public IEnumerator Poison(float count)
    {
        isPoison = false;

        for(int i = 0; i < count; i ++)
        {
            sp.color = Color.green;
            OnDamage(1f, () => {
                Slow(5f, 0.5f);
            });
            yield return new WaitForSeconds(1f);
        }
        sp.color = Color.white;
    }

    public IEnumerator Slow(float val, float time)
    {
        isSlow = false;
        PlayerControl pc = GetComponent<PlayerControl>();
        float fSpeed = pc.speed;
        pc.speed -= val;
        yield return new WaitForSeconds(time);
        pc.speed = fSpeed;
    }

    public void OnDamage(float dmg, Action freeze = null)
    {
        //State가 Damaged면 return
        if (stateEnum.state.HasFlag(State.Damaged)) return;

        //enum(State) 업데이트
        stateEnum.state |= State.Damaged;

        freeze?.Invoke();

        StartCoroutine(Twinkle());
        StartCoroutine(KnockBack());

        currentHP -= dmg;

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
        stateEnum.state &= ~State.Damaged;
    }

    /// <summary>
    /// 빛 깜빡임
    /// </summary>
    private IEnumerator Twinkle()
    {
        lightObj.SetActive(false);
        yield return new WaitForSeconds(twinkleDuration);
        lightObj.SetActive(true);
        yield return new WaitForSeconds(twinkleDuration);
        lightObj.SetActive(false);
        yield return new WaitForSeconds(twinkleDuration);
        lightObj.SetActive(true);

        if (currentHP <= 0)
        {
            StartCoroutine(Twinkle());
            yield return new WaitForSeconds(twinkleDuration);

            EnemySpawner.Instance.StopMethod();
            PoolManager.Instance.pools.Clear();
            GameManager.Instance.pooler.SetActive(false);
            yield return new WaitForSeconds(twinkleDuration);

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
