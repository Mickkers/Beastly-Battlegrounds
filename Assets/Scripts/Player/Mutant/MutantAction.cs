using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAction : PlayerAction
{
    private MutantSkill mSkill;
    private MutantUltimate mUltimate;

    [SerializeField] private AudioSource basicAttackSound;
    [SerializeField] private AudioSource skillAttackSound;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        mSkill = GetComponent<MutantSkill>();
        mUltimate = GetComponent<MutantUltimate>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMana();
        BasicAttack();
    }


    private void BasicAttackHit()
    {
        float damage = GetDamage();

        if (mSkill.isSkillActive)
        {
            damage *= (1 + mSkill.skillDamageIncrease);
            skillAttackSound.Play();
        }
        else
        {
            basicAttackSound.Play();
        }
        if (target == null) return;
        target.GetComponent<Enemy>().TakeDamage(damage);
    }

    private void BasicAttackEnd()
    {
        nextAttackTime = attackInterval + Time.time;
        canBasicAttack = true;
        animator.SetBool(AnimationStrings.basicAttack, false);
    }

    public override void Skill()
    {
        if (mSkill.isSkillAvailable && HasMana(mSkill.GetManaCost()))
        {
            currMana -= mSkill.GetManaCost();
            mSkill.SkillAction();
        }
    }

    public override void Ultimate()
    {
        if (mUltimate.isUltimateAvailable && HasMana(mUltimate.GetManaCost()))
        {
            currMana -= mUltimate.GetManaCost();
            mUltimate.UltimateAction();
        }
    }
}
