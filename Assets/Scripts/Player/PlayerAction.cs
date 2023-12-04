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

    public void Initialization()
    {
        canBasicAttack = true;
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currMana = maxMana;
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

    public abstract void BasicAttack();
    public abstract void Skill();
    public abstract void Ultimate();

    public float GetAttackRange()
    {
        return attackRange / transform.localScale.x;
    }

    public float GetDamage()
    {
        return Mathf.Lerp(damageMin, damageMax, Random.Range(0f, 1f));
    }
}
