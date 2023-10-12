using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static Attack;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;


    private IDamageable damageableTarget;

    public System.Action OnTakeAttack;

    public delegate void OnTakeDamageable();
    public OnTakeDamageable onTakeDamageable;

    public void TakeAttack()
    {
        // 贸府 肺流
        damageableTarget.OnDamage(Constant.DAMAGE);

        OnTakeAttack?.Invoke();
    }

    private void Start()
    {
        damageableTarget = GetComponent<IDamageable>();

        OnTakeAttack = () => { HandleTakeAttack(); };
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
        onTakeDamageable.Invoke();
            // 傍拜 肺流
            nextAttackTime = Time.time + Constant.ATTACK_COLLTIME;
         //   
        //}
    }
}
