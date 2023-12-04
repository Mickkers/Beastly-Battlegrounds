using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutantUltimate : MonoBehaviour
{
    private Animator animator;

    [Header("Ultimate Attributes")]
    [SerializeField] private float ultimateCooldown;
    [SerializeField] private float ultimateCost;
    [SerializeField] private float ultimateDamage;
    [SerializeField] private float ultimateRange;
    [SerializeField] private float ultimateDuration;
    [SerializeField] private ParticleSystem effect;

    [Header("UI Eements")]
    [SerializeField] private Image ultimateIcon;
    [SerializeField] private TextMeshProUGUI ultimateText;

    public bool isUltimateAvailable;
    private bool isUltimateActive;

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

    public void Ultimate()
    {
        isUltimateActive = true;
        isUltimateAvailable = false;

        animator.SetTrigger(AnimationStrings.ultimate);
        currDuration = 100f;
        currCooldown = ultimateCooldown;
    }

    private void UltimateAction()
    {
        currDuration = ultimateDuration;
        effect.Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ultimateRange);
        float damage = GetComponent<MutantAction>().GetDamage();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(ultimateDamage * damage);
            }
        }
    }

    private void UltimateDuration()
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

    private void UltimateCooldown()
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

    private void UpdateUltUI(float cooldown)
    {
        ultimateIcon.fillAmount = 1f - cooldown / ultimateCooldown;
        ultimateText.text = "" + (int)cooldown;
    }

    public float GetManaCost()
    {
        return ultimateCost;
    }
}
