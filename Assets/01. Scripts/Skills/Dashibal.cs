using System.Collections;
using UnityEngine;

public class Dashibal : SkillBase
{
    [SerializeField] Collider2D playerCol;
    [SerializeField] Collider2D dashRange;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject panel;
    [SerializeField] float delay;
    private PlayerControl pc = null;
    private CamControl cc = null;

    private void Awake()
    {
        pc = GetComponentInParent<PlayerControl>();
        cc = GameObject.Find("CM vcam1").GetComponent<CamControl>();
    }

    public void StartShibalDash()
    {
        if (coolDown > coolTime && Input.GetKeyDown(key))
            StartCoroutine(ShibalDash());
    }

    private IEnumerator ShibalDash()
    {
        panel.SetActive(true);
        cc.SetFollow(null);
        pc.stateEnum.state |= State.Ulti;
        pc.rb2d.velocity = Vector2.zero;
        Collider2D[] arr = Physics2D.OverlapBoxAll(dashRange.bounds.center, dashRange.bounds.size, 0, enemyLayer);
        playerCol.isTrigger = true;

        yield return new WaitForSeconds(delay * 5);
        
        for (int i = 0; i < arr.Length; i++)
        {
            Vector3 dir = (arr[i].transform.position - transform.position).normalized;
            pc.rb2d.velocity = dir * 200;
            yield return new WaitForSeconds(delay);
            if(arr[i].GetComponent<IDamageable>() != null)
                arr[i].GetComponent<IDamageable>().OnDamage(10, () => {
                    arr[i].GetComponent<Enemy>().currentHP = 0;
                });
            pc.rb2d.velocity = Vector2.zero;
        }
        
        GameManager.Instance.player.transform.position = Camera.main.transform.position;
        
        yield return new WaitForSeconds(delay * 5);
        panel.SetActive(false);
        pc.stateEnum.state &= ~State.Ulti;
        pc.rb2d.velocity = Vector2.zero;
        playerCol.isTrigger = false;
        coolDown = 0;

        cc.SetFollow(GameManager.Instance.player.transform);
        yield return null;
    }
}
