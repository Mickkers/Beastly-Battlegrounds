using DigitalRuby.PyroParticles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlAction : PlayerAction
{
    private MjlSkill mjlSkill;
    private MjlUltimate mjlUltimate;

    [SerializeField] private MjlProjectile basicProjectile;
    [SerializeField] private AudioSource basicSound;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        mjlSkill = GetComponent<MjlSkill>();
        mjlUltimate = GetComponent<MjlUltimate>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMana();
        BasicAttack();
    }

    private void BasicAttackHit()
    {
        if (target is null) return;
        basicSound.Play();
        Vector3 pos = transform.position;
        pos.y += 1.2f;
        MjlProjectile proj = Instantiate(basicProjectile, pos, transform.rotation);
        proj.damage = GetDamage();
        Transform targetTransform = target.transform;
        targetTransform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 1.2f, targetTransform.position.z);
        proj.SetTarget(targetTransform);
    }

    private void BasicAttackEnd()
    {
        nextAttackTime = attackInterval + Time.time;
        canBasicAttack = true;
        animator.SetBool(AnimationStrings.basicAttack, false);
    }

    public override void Skill()
    {
        if(mjlSkill.isSkillAvailable && !mjlSkill.isSkillActive && HasMana(mjlSkill.GetManaCost()))
        {
            Debug.Log(mjlSkill.isSkillAvailable + " " + mjlSkill.isSkillActive + " " + HasMana(mjlSkill.GetManaCost()));
            currMana -= mjlSkill.GetManaCost();
            mjlSkill.effect.SetActive(true);
            mjlSkill.isSkillActive = true;
            Debug.Log(mjlSkill.isSkillAvailable + " " + mjlSkill.isSkillActive + " " + HasMana(mjlSkill.GetManaCost()));
        }
    }

    public override void Ultimate()
    {
        if (mjlUltimate.isUltimateAvailable && HasMana(mjlUltimate.GetManaCost()))
        {
            currMana -= mjlUltimate.GetManaCost();
            mjlUltimate.UltimateAction();
        }
    }
}
