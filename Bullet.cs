using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBody;

    private Vector3 startPosition;

    private bool isAttack;

    public System.Action onDelete;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        BulletMove();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null) 
        {
            isAttack = true;
        }

        Delete();
    }

    public bool IsAttack
    {
        get { return isAttack; }
    }
    private void Delete()
    {
        // 총알이 죽었을 때 이벤트 발생
        onDelete?.Invoke();
        StopCoroutine(DeleteByDistance());
        Destroy(gameObject);
    }

    IEnumerator DeleteByDistance()
    {
        while(rigidBody != null) 
        {
            float distanceToStartPosition = Vector3.Distance(startPosition, rigidBody.position);
            
            if (distanceToStartPosition > Constant.BULLET_DELETE_DISTANCE)
            {
                isAttack = false;
                Delete();
                yield break;
            }

            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }

    private void BulletMove()
    {
        if (rigidBody != null)
        {
            rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        }

    }
}
