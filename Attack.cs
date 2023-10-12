using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public System.Action OnTakeAttack;

    private void Start()
    {
        OnTakeAttack += TakeDamage;
    }

    private void TakeDamage()
    {
        IDamageable damageableTarget = GetComponent<IDamageable>();

        if (damageableTarget != null)
            damageableTarget.OnDamage(Constant.DAMAGE);
    }
}
