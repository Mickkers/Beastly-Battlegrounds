using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MutantSkill : MonoBehaviour
{
    private Animator animator;
    private PlayerHealth playerHealth;

    [Header("Skill Attributes")]
    [SerializeField] private float skillCooldown;
    [SerializeField] private float skillCost;
    [SerializeField] private float skillDuration;
    public float skillDamageIncrease;
    [SerializeField] private float skillHealthDrain;
    [SerializeField] private ParticleSystem buff;

    [Header("UI Eements")]
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillText;

    [HideInInspector] public bool isSkillAvailable;
    [HideInInspector] public bool isSkillActive;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        isSkillActive = false;
        isSkillAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        SkillDuration();
        SkillCooldown();
    }

    public void Skill()
    {
        isSkillActive = true;
        isSkillAvailable = false;
        animator.SetBool(AnimationStrings.skillAttack, true);
        buff.Play();
        currDuration = skillDuration;
        currCooldown = skillCooldown;
    }

    private void SkillDuration()
    {
        if (!isSkillActive)
        {
            return;
        }
        if (currDuration > 0f)
        {
            playerHealth.TakeDamage(playerHealth.MaxHealth * skillHealthDrain * Time.deltaTime);
            currDuration -= Time.deltaTime;
        }
        else if(currDuration <= 0f)
        {
            buff.Stop();
            animator.SetBool(AnimationStrings.skillAttack, false);
            isSkillActive = false;
            currDuration = 0f;
        }
    }

    private void SkillCooldown()
    {
        if (isSkillActive || isSkillAvailable)
        {
            return;
        }
        if (currCooldown > 0f)
        {
            currCooldown -= Time.deltaTime;
            UpdateSkillUI(currCooldown);
        }
        else if (currCooldown <= 0f)
        {
            skillIcon.fillAmount = 1f;
            skillText.text = "";

            isSkillAvailable = true;
            currCooldown = 0f;
        }
        
    }

    private void UpdateSkillUI(float cooldown)
    {
        skillIcon.fillAmount = 1f - cooldown / skillCooldown;
        skillText.text = "" + (int)cooldown;
    }

    public float GetManaCost()
    {
        return skillCost;
    }
}
