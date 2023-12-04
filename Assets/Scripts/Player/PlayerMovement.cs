using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private PlayerAction playerAction;
    private HighlightManager highlightManager;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float motionSmoothTime;
    private float rotateVelocity;
    private float attackRange;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>();
        attackRange = playerAction.GetAttackRange();
        highlightManager = GetComponent<HighlightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        CheckTargetDistance();
    }

    private void CheckTargetDistance()
    {
        
        if (target != null)
        {
            if (target.CompareTag("DeadEnemy"))
            {
                target = null;
                highlightManager.DeselectOutline();
                return;
            }
            if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
            {
                navMeshAgent.SetDestination(target.transform.position);
            }
        }
    }

    public void Move(Vector2 val)
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(val), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Enemy"))
            {
                navMeshAgent.SetDestination(hit.point);
                navMeshAgent.stoppingDistance = 0;

                RotationLook(hit.point);

                if (target != null)
                {
                    target = null;
                    highlightManager.DeselectOutline();
                }
            }
        }
    }

    public void MoveToEnemy(Vector2 val)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(val), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                target = hit.collider.gameObject;
                navMeshAgent.SetDestination(target.transform.position);
                navMeshAgent.stoppingDistance = attackRange;
                highlightManager.SelectOutline();
                RotationLook(target.transform.position);
            }
        }
    }

    private void RotationLook(Vector3 position)
    {
        Quaternion rotationLook = Quaternion.LookRotation(position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLook.eulerAngles.y, ref rotateVelocity, rotateSpeed * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }


    private void Animate()
    {
        float speed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        animator.SetFloat(AnimationStrings.speed, speed, motionSmoothTime, Time.deltaTime);
    }
}
