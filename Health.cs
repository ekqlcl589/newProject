using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float maxHP = 100f;
    private const float dieHp = 0f;
    private float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log(currentHp);

        if (currentHp <= dieHp)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}