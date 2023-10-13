using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidbody;

    private Vector3 move = Vector3.right;
    private Vector3 startPosition;
    public System.Action onDelete; // ¿Ã∏ß

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
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null) 
        {
            health.CurrentHp -= Constant.DAMAGE;
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
