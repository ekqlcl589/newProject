using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rigidBody;

    private Vector3 startPosition;
    public System.Action onDelete;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPosition = transform.position;

        StartCoroutine(DeleteByDistance());
    }
    // ��Ȯ�� �����Ӻ��� �������� �����ϵ��� Update -> FixedUpdate�� �̵� 
    private void FixedUpdate()
    {
        if (rigidBody != null)
        {
            rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null) 
        {
            health.CurrentHp = Constant.DAMAGE;
        }

        Delete();
    }

    private void Delete()
    {
        this.onDelete();

        Destroy(gameObject);
    }

    IEnumerator DeleteByDistance()
    {
        while(rigidBody != null) 
        {
            yield return new WaitForFixedUpdate();

            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                Delete();
            }
        }
    }
}
