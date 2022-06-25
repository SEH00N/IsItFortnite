using System.Collections;
using UnityEngine;

public class Dashibal : SkillBase
{
    [SerializeField] GameObject panel;
    [SerializeField] Collider2D playerCol;
    [SerializeField] Collider2D dashRange;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float delay;
    private UltiAni ua = null;
    private PlayerControl pc = null;
    private CamControl cc = null;

    private void Awake()
    {
        ua = GetComponentInChildren<UltiAni>();
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
        Vector3 p = pc.transform.position;
        cc.vCam.transform.position = new Vector3(p.x, p.y, -10);
        pc.stateEnum.state |= State.Ulti;
        pc.rb2d.velocity = Vector2.zero;
        cc.SetFollow(null);
        ua.StartAni();
        yield return new WaitUntil(() => ua.isFinish);
        ua.EndAni();
        panel.SetActive(true);
        ua.isFinish = false;
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
        pc.stateEnum.state &= ~State.Ulti;
        pc.rb2d.velocity = Vector2.zero;
        playerCol.isTrigger = false;
        coolDown = 0;
        panel.SetActive(false);

        cc.SetFollow(GameManager.Instance.player.transform);
        yield return null;
    }
}
