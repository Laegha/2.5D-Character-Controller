using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObjectHP : MonoBehaviour
{
    [SerializeField] float maxHP;
    float remainingHP;
    
    private void Awake()
    {
        remainingHP = maxHP;    
    }

    public void TakeDamage(float incomingDamage)
    {
        remainingHP -= incomingDamage;
        print(remainingHP + " " + incomingDamage);
        if (remainingHP <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
