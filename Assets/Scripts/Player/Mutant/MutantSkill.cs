using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MutantSkill : Skill
{
    private PlayerHealth playerHealth;

    public float skillDamageIncrease;
    [SerializeField] private float skillHealthDrain;
    [SerializeField] private ParticleSystem buff;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        isSkillActive = false;
        isSkillAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        SkillDuration();
        SkillCooldown();
    }

    public override void SkillAction()
    {
        isSkillActive = true;
        isSkillAvailable = false;
        animator.SetBool(AnimationStrings.skillAttack, true);
        buff.Play();
        currDuration = skillDuration;
        currCooldown = skillCooldown;
    }

    protected override void SkillDuration()
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

    protected override void SkillCooldown()
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
}
