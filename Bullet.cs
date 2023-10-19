using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidBody;

    private Vector3 startPosition;

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
        Delete();
    }

    private void Delete()
    {
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
