using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MjlSkill : Skill
{
    private PlayerAction pAction;
    private NavMeshAgent agent;

    [SerializeField] private float skillRange;
    [SerializeField] private float skillDamage;
    [SerializeField] private MjlSkillProjectile projectilePrefab;
    public GameObject effect;
    [SerializeField] private AudioSource skillSound;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pAction = GetComponent<PlayerAction>();
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
        if (!isSkillActive) return;
        agent.isStopped = true;
        Vector2 val = InputManager.GetMousePosition();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(val), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                isSkillActive = false;
                effect.SetActive(true);
                isSkillAvailable = false;
                animator.SetTrigger(AnimationStrings.skillAttack);
                currDuration = skillDuration;
                currCooldown = skillCooldown;
            }
        }
    }

    private void SkillHit()
    {
        if (pAction.target is null) return;
        skillSound.Play();
        Vector3 pos = transform.position;
        pos.y += 1.2f;
        MjlSkillProjectile proj = Instantiate(projectilePrefab, pos, transform.rotation);
        proj.damage = pAction.GetDamage() * skillDamage;
        proj.range = skillRange;
        Transform targetTransform = pAction.target.transform;
        targetTransform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 1.2f, targetTransform.position.z);
        proj.SetTarget(targetTransform);
        effect.SetActive(false);
        agent.isStopped = false;
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

    protected override void SkillDuration()
    {
        if (!isSkillActive || isSkillAvailable)
        {
            return;
        }
        if (currDuration > 0f)
        {
            currDuration -= Time.deltaTime;
        }
        else if (currDuration <= 0f)
        {
            isSkillActive = false;
            currDuration = 0f;
        }
    }
}
