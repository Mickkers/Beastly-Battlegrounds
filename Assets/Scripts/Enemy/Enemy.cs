using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;
    private NavMeshAgent navMeshAgent;

    public float currHealth;
    [SerializeField] private float maxHealth;
    [Header("Attack Attributes")]
    [SerializeField] private float damageMin;
    [SerializeField] private float damageMax;
    [SerializeField] private float attackRange;
    [Header("UI")]
    [SerializeField] private RectTransform healthCanvas;
    [SerializeField] private Image healthBar;

    private float nextAttackTime;
    private float attackInterval;
    public float attackCooldown;
    [HideInInspector] public bool canAttack;

    public bool isAlive;

    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DisableOutlineOnStart", 0.01f);
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        currHealth = maxHealth;
        isAlive = true;
        canAttack = true;
        attackInterval = attackCooldown / (110 + attackCooldown) * 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
        UpdateHealth();
    }

    private void AttackPlayer()
    {
        if (player != null && canAttack)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < GetAttackRange() && Time.time > nextAttackTime && navMeshAgent.velocity.magnitude < 0.1f)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        animator.SetBool(AnimationStrings.basicAttack, true);

        yield return new WaitForSeconds(attackInterval);

        if (player == null || Vector3.Distance(transform.position, player.transform.position) > GetAttackRange())
        {
            canAttack = true;
            animator.SetBool(AnimationStrings.basicAttack, false);
        }
    }

    private void AttackHit()
    {
        float damage = GetDamage();
        if (Vector3.Distance(transform.position, player.transform.position) < GetAttackRange())
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void AttackEnd()
    {
        nextAttackTime = attackInterval + Time.time;
        canAttack = true;
        animator.SetBool(AnimationStrings.basicAttack, false);
    }


    private void UpdateHealth()
    {
        if(currHealth <= 0f && isAlive)
        {
            isAlive = false;
            Death();
        }
        Mathf.Clamp(currHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if(currHealth > 0f)
        {
            healthCanvas.gameObject.SetActive(true);
            healthBar.fillAmount = currHealth / maxHealth;
        }
        else
        {
            healthCanvas.gameObject.SetActive(false);
        }
    }

    private void Death()
    {
        healthCanvas.gameObject.SetActive(false);
        animator.SetTrigger(AnimationStrings.death);
        gameObject.tag = "DeadEnemy";
        gameObject.layer = 9;
    }

    private float GetDamage()
    {
        return Mathf.Lerp(damageMin, damageMax, Random.Range(0f, 1f));
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
    }

    private void DisableOutlineOnStart()
    {
        GetComponent<Outline>().enabled = false;
    }
}
