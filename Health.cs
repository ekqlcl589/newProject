using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float currentHp;

    // Start is called before the first frame update
    private void Start()
    {
        currentHp = Constant.MAX_HP;
    }

    public virtual void OnDamage(float damage)
    {

        currentHp -= damage;
        Debug.Log(currentHp);
        if (currentHp <= Constant.DIE_HP)
            Die();
    }

    private void Die()
    {
        //Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        OnDamage(Constant.DAMAGE);
    }
}