using DG.Tweening;
using UnityEngine;

public class Explosion : PoolableMono
{
    [SerializeField] SpriteRenderer sp = null;
    [SerializeField] float delay;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public override void Reset()
    {
        sp.DOFade(1, 0);
        AutoDie();
    }

    private void AutoDie()
    {
        sp.DOFade(0, delay).OnComplete(() => PoolManager.Instance.Enqueue(this));
    }
}
