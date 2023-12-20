using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlUltimate : Ultimate
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float burnDamage;
    [SerializeField] private AudioSource ultimateSound;

    private float currDuration;
    private float currCooldown;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isUltimateActive = false;
        isUltimateAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        UltimateDuration();
        UltimateCooldown();
    }
    public override void UltimateAction()
    {
        isUltimateActive = true;
        isUltimateAvailable = false;

        animator.SetTrigger(AnimationStrings.ultimate);
        currDuration = 100f;
        currCooldown = ultimateCooldown;
    }

    private void UltimateHit()
    {
        UltDamage(ultimateDamage);
        ultimateSound.Play();
        currDuration = ultimateDuration;
        effect.SetActive(true);
    }

    protected override void UltimateDuration()
    {
        if (!isUltimateActive)
        {
            return;
        }
        if (currDuration > 0f)
        {
            if(currDuration <= 4f)
            {
                UltDamage(burnDamage * Time.deltaTime);
            }
            currDuration -= Time.deltaTime;
        }
        else if (currDuration <= 0f)
        {
            effect.SetActive(false);

            isUltimateActive = false;
            currDuration = 0f;
        }
    }

    protected override void UltimateCooldown()
    {
        if (isUltimateActive || isUltimateAvailable)
        {
            return;
        }
        if (currCooldown > 0f)
        {
            currCooldown -= Time.deltaTime;
            UpdateUltUI(currCooldown);
        }
        else if (currCooldown <= 0f)
        {
            ultimateIcon.fillAmount = 1f;
            ultimateText.text = "";

            isUltimateAvailable = true;
            currCooldown = 0f;
        }

    }

    private void UltDamage(float damageMultiplier)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ultimateRange);
        float damage = GetComponent<PlayerAction>().GetDamage();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damageMultiplier * damage);
            }
        }
    }
}

