using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutantUltimate : Ultimate
{
    
    [SerializeField] private ParticleSystem effect;
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

    public void UltimateHit()
    {
        currDuration = ultimateDuration;
        effect.Play();
        ultimateSound.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ultimateRange);
        float damage = GetComponent<PlayerAction>().GetDamage();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(ultimateDamage * damage);
            }
        }
    }

    protected override void UltimateDuration()
    {
        if (!isUltimateActive)
        {
            return;
        }
        if (currDuration > 0f)
        {
            currDuration -= Time.deltaTime;
        }
        else if (currDuration <= 0f)
        {
            effect.Stop();
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
}
