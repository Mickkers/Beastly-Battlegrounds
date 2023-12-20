using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource takeDamageSound;
    [SerializeField] private AudioSource deathSound;
    [Header("Health")]
    [SerializeField] private float maxHealth;

    [Header("HealthUI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBarChar;
    [SerializeField] private Image healthBar;

    private bool isAlive;

    public float currHealth;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currHealth = MaxHealth;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHealth < 0 && isAlive)
        {
            isAlive = false;
            Death();
        }
        currHealth = Mathf.Clamp(currHealth, 0, MaxHealth);
        healthText.text = "<mspace=28>" + (int)currHealth + "/" + MaxHealth + "</mspace>";
        healthBar.fillAmount = currHealth / MaxHealth;
        healthBarChar.fillAmount = currHealth / MaxHealth;
    }

    private void Death()
    {
        animator.SetTrigger(AnimationStrings.death);
        deathSound.Play();
        GetComponent<InputManager>().enabled = false;
    }

    private void GameOver()
    {
        GameManager gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        gm.GameOver();
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
        takeDamageSound.Play();
    }

    public void HealToFull()
    {
        currHealth = maxHealth;
    }
}
