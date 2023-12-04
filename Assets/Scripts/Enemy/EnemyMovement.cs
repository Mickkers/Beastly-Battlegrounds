using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Enemy enemy;
    private PlayerMovement player;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float motionSmoothTime;
    private float rotateVelocity;

    [SerializeField] private float chaseRange;
    private float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        attackRange = enemy.GetAttackRange();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isAlive) enabled = false;
        CheckPlayerDistance();
        Animate();
    }

    private void Animate()
    {
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat(AnimationStrings.speed, speed, motionSmoothTime, Time.deltaTime);
    }

    private void CheckPlayerDistance()
    {
        if(Vector3.Distance(player.gameObject.transform.position, transform.position) < chaseRange)
        {
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.stoppingDistance = attackRange - 1;
            RotationLook(player.transform.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.stoppingDistance = 0;
        }
    }

    private void RotationLook(Vector3 position)
    {
        Quaternion rotationLook = Quaternion.LookRotation(position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLook.eulerAngles.y, ref rotateVelocity, rotateSpeed * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}
