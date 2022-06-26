using UnityEngine;

public class Audio : PoolableMono
{
    private AudioSource source = null;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetFloat("volume", 1);
    }

    public override void Reset()
    {
        Invoke("AutoDie", 5f);
    }

    private void AutoDie()
    {
        PoolManager.Instance.Enqueue(this);
    }
}
