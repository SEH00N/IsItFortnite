using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour, IDamageable
{
    [SerializeField] float hp = 10f;
    [SerializeField] float knockBackDuration = 0.5f;

    public void OnDamage(float dmg)
    {
        //State가 Damaged면 return
        if (PlayerState.Instance.state.HasFlag(PlayerState.State.Damaged)) return;

        //enum(State) 업데이트
        PlayerState.Instance.state |= PlayerState.State.Damaged;
        hp -= dmg;

        if(hp <= 0)
            gameObject.SetActive(false);
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(knockBackDuration);

        //State Flag에서 Damaged 제거
        PlayerState.Instance.state &= ~PlayerState.State.Damaged;
    }
}
