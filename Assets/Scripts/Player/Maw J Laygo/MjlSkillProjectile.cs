using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlSkillProjectile : MonoBehaviour
{
    private Transform target;
    private Transform original;
    private Rigidbody rb;

    public float damage;
    public float range;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private MjlSkillExplosion explosionPrefab;

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
        if (target != null && ReferenceEquals(other.gameObject, target.gameObject))
        {
            CreateExplosion();
        }
        else if (original != null && ReferenceEquals(other.gameObject, original.gameObject))
        {
            CreateExplosion();
        }
    }

    private void CreateExplosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        MjlSkillExplosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
