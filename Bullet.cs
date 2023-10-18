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
        startPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(DeleteByDistance());
    }
    
    private void FixedUpdate()
    {
        if (rigidBody != null)
        {
            // 물리적인 움직임을 행하므로 고정된 프레임 마다 총알에 힘을 가해줌
            rigidBody.AddForce(transform.forward * Constant.BULLET_POWER);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null) 
        {
            health.CurrentHp = Constant.DAMAGE * 10f;
        }

        Delete();
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
                Delete();
                yield break;
            }

            yield return new WaitForSeconds(Constant.BULLET_DELETE_TIME);
        }
    }
}
