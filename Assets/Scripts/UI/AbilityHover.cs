using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AbilityHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static EnumCharType playerChar;
    [SerializeField] private EnumAbilityType abilityType;
    [SerializeField] private string[] descriptions;
    [SerializeField] private RectTransform hoverInfo;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI coooldownText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private PlayerAction player;
    private bool asigned;

    private void Start()
    {
        asigned = false;
    }

    private void Update()
    {
        if (!asigned)
        {
            player = FindObjectOfType(typeof(PlayerAction)) as PlayerAction;
            if (player is null) return;
            int index;
            if (abilityType == EnumAbilityType.Skill)
            {
                nameText.text = "Skill";
                costText.text = "Mana: " + player.GetComponent<Skill>().skillCost;
                coooldownText.text = "Cooldown: " + player.GetComponent<Skill>().skillCooldown + "s";
                index = 0;
            }
            else
            {
                nameText.text = "Ultimate";
                costText.text = "Mana: " + player.GetComponent<Ultimate>().ultimateCost;
                coooldownText.text = "Cooldown: " + player.GetComponent<Ultimate>().ultimateCooldown + "s";
                index = 2;
            }

            if (playerChar == EnumCharType.Melee)
            {

            }
            else
            {
                index++;
            }

            descriptionText.text = descriptions[index];
            asigned = true;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverInfo.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverInfo.gameObject.SetActive(false);
    }
}

internal enum EnumAbilityType
{
    Skill,
    Ultimate
}