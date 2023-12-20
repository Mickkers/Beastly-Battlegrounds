using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlProjectile : MonoBehaviour
{
    private Transform target;
    private Transform original;
    private Rigidbody rb;

    public float damage;
    [SerializeField] private float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        original = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else if (original != null)
        {
            Vector3 direction = original.position - transform.position;
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            Enemy enemy = target.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(original != null && ReferenceEquals(other.gameObject, original.gameObject))
        {
            Enemy enemy = original.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
