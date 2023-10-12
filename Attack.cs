using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;


    private IDamageable damageableTarget;

    public System.Action OnTakeAttack;


    private void Start()
    {
        damageableTarget = GetComponent<IDamageable>();

        OnTakeAttack += HandleTakeAttack;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HandleTakeAttack()
    {
        //if (Time.time > nextAttackTime)
        //{
        if (damageableTarget != null)
            damageableTarget.OnDamage(Constant.DAMAGE);
        else
            return;
            // 공격 로직
            nextAttackTime = Time.time + Constant.ATTACK_COLLTIME;
         //   
        //}
    }
}
