using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed = 5f;                                                                                                                                        
    public Rigidbody2D rb2d = null;
    protected StateEnum stateEnum = null;
    protected Collider2D col2d = null;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        stateEnum = GetComponent<StateEnum>();
    }
}
