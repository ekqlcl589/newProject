using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;


    private IDamageable damageableTarget;

    public System.Action OnTakeAttack;

    public void TakeAttack()
    {
        // ó�� ����
        damageableTarget.OnDamage(Constant.DAMAGE);

        OnTakeAttack?.Invoke();
    }

    private void Start()
    {
        damageableTarget = GetComponent<IDamageable>();

        OnTakeAttack += HandleTakeAttack;
    }

    // Update is called once per frame
    void Update()
    {
        //TakeAttack();
        //HandleTakeAttack();
    }

    private void HandleTakeAttack()
    {
            //OnTakeAttack.Invoke();
        //if (Time.time > nextAttackTime)
        //{
            damageableTarget.OnDamage(Constant.DAMAGE);

            // ���� ����
            nextAttackTime = Time.time + Constant.ATTACK_COLLTIME;
         //   
        //}
    }
}
