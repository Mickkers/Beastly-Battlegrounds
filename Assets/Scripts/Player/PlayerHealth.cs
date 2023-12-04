using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;

    [Header("HealthUI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healtBarChar;
    [SerializeField] private Image healthBar;

    public float currHealth;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currHealth = Mathf.Clamp(currHealth, 0, MaxHealth);
        healthText.text = "<mspace=28>" + (int)currHealth + "/" + MaxHealth + "</mspace>";
        healthBar.fillAmount = currHealth / MaxHealth;
        healtBarChar.fillAmount = currHealth / MaxHealth;
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
    }
}
