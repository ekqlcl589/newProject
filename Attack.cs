using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool isAttack = false;

    private BoxCollider boxCollider;

    private const int damage = 20;
    // Start is called before the first frame update

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackCheck()
    {
        IDamageable target = GetComponent<IDamageable>();

        if (target != null) 
        {
            target.OnDamage(damage, gameObject.transform.position, gameObject.transform.position);
        }
    }

    
}
