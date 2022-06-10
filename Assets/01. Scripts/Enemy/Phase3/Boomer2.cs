using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomer2 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerDamaged pd = other.gameObject.GetComponent<PlayerDamaged>();
            pd.isPoison = true;
        }
    }
}
