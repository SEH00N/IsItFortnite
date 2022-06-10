using UnityEngine;

public class SkillBase : MonoBehaviour
{
    [SerializeField] protected Transform lookAt;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected KeyCode key;
    public float coolTime = 10;
    public float coolDown = 0;
    protected float time = 0;

    protected virtual void Update()
    {
        time = Time.deltaTime;
        coolDown += time;
    }
}
