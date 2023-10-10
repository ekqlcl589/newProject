using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int maxHP = 100;
    private const int dieHp = 0;
    private int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHP;
    }

    public void TakeDamage(int damage)
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