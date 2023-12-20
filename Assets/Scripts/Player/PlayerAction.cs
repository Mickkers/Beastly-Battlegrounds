using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class PlayerAction : MonoBehaviour
{
    [Header("Mana Attributes")]
    public float currMana;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;
    [Header("Mana UI")]
    [SerializeField] private Image manaBar;
    [SerializeField] private Image manaBarChar;
    [SerializeField] private TextMeshProUGUI manaText;
    [Header("Basic Attack Attributes")]
    [SerializeField] private float damageMin;
    [SerializeField] private float damageMax;
    [SerializeField] private float attackRange;
    public float attackCooldown;
    [HideInInspector] public bool canBasicAttack;

    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public GameObject target;
    [HideInInspector] public float nextAttackTime;
    [HideInInspector] public float attackInterval;

    public void Initialization()
    {
        canBasicAttack = true;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currMana = maxMana;

        attackInterval = attackCooldown / (240 + attackCooldown) * 0.01f;
    }

    public bool HasMana(float value)
    {
        return value <= currMana;
    }

    public void UpdateMana()
    {
        if (currMana < maxMana) currMana += manaRegen * Time.deltaTime;
        currMana = Mathf.Clamp(currMana, 0, maxMana);
        manaText.text = "<mspace=28>" + (int)currMana + "/" + maxMana + "</mspace>";
        manaBar.fillAmount = currMana / maxMana;
        manaBarChar.fillAmount = currMana / maxMana;
    }

    public void BasicAttack()
    {
        target = playerMovement.target;

        if (target != null && canBasicAttack)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= GetAttackRange() && Time.time > nextAttackTime)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        canBasicAttack = false;
        animator.SetBool(AnimationStrings.basicAttack, true);

        yield return new WaitForSeconds(attackInterval);

        if (target == null || target.CompareTag("DeadEnemy"))
        {
            canBasicAttack = true;
            animator.SetBool(AnimationStrings.basicAttack, false);
        }
    }
    public abstract void Skill();
    public abstract void Ultimate();

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetDamage()
    {
        return Mathf.Lerp(damageMin, damageMax, Random.Range(0f, 1f));
    }

    public void ManaToFull()
    {
        currMana = maxMana;
    }
}
