using UnityEngine;

public class TestKnockBack : MonoBehaviour
{
    [SerializeField] Transform firePos;
    private Rigidbody2D rb2d = null;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            Vector2 dir = firePos.position - transform.position;
            rb2d.AddForce(-dir, ForceMode2D.Impulse);
        }
    }
}
