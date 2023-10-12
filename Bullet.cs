using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;
    // 유도탄이 아니라 x 방향으로만 발사
    private Vector3 move = Vector3.right;
    private Vector3 startPosition;
    public System.Action onDelete; // 이름

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if (rigidbody != null)
        {
            rigidbody.AddForce(move * Constant.BULLET_POWER);

            DeleteByDistance();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableTarget = collision.gameObject.GetComponent<IDamageable>();
        if (damageableTarget != null)
        {
            damageableTarget.OnDamage(Constant.DAMAGE * 10f);
        }

        Delete();
    }

    private void Delete()
    {
        this.onDelete();

        Destroy(gameObject);
    }

    private void DeleteByDistance()
    {
        float distanceToStartPosition = Vector3.Distance(startPosition, rigidbody.position);

        if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
        {
            Delete();
        }

    }
}
