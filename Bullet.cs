using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;
    private Vector3 move = new Vector3(1f, 0f, 0f);

    public System.Action onDie;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rigidbody != null) 
        {
            rigidbody.AddForce(move * Constant.BULLET_POWER * Time.time);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableTarget = collision.gameObject.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            damageableTarget.OnDamage(Constant.DAMAGE);
        }

        Debug.Log(collision.gameObject);

        Debug.Log("ªË¡¶");
        Die();

    }

    private void Die()
    {
        this.onDie();
        Destroy(gameObject);
    }
}
