using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private const int maxHP = 100;
    private const int damage = 10;

    private const int dieHp = 0;
    private int hp;

    private bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision == null) 
            return;

    }

    public void OnDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        hp -= damage;

        if (hp < dieHp)
        {
            hp = dieHp;

            isAlive = true;
        }
    }

    public void Alive()
    {
        if(isAlive)
        {
            Destroy(gameObject);
        }
    }
}
