using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAction : PlayerAction
{
    private MutantSkill mSkill;
    private MutantUltimate mUltimate;
    private GameObject target;

    private float nextAttackTime;
    private float attackInterval;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
        mSkill = GetComponent<MutantSkill>();
        mUltimate = GetComponent<MutantUltimate>();
        attackInterval = attackCooldown / (240 + attackCooldown) * 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMana();
        BasicAttack();
    }
    public override void BasicAttack()
    {
        target = playerMovement.target;

        if(target != null && canBasicAttack)
        {
            if(Vector3.Distance(transform.position, target.transform.position) <= GetAttackRange() && Time.time > nextAttackTime)
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

        if(target == null || target.CompareTag("DeadEnemy"))
        {
            canBasicAttack = true;
            animator.SetBool(AnimationStrings.basicAttack, false);
        }
    }

    private void BasicAttackHit()
    {
        float damage = GetDamage();
        if (mSkill.isSkillActive) damage *= (1 + mSkill.skillDamageIncrease);
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
            mSkill.Skill();
        }
    }

    public override void Ultimate()
    {
        if (mUltimate.isUltimateAvailable && HasMana(mUltimate.GetManaCost()))
        {
            currMana -= mUltimate.GetManaCost();
            mUltimate.Ultimate();
        }
    }
}
