using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private CharSelect cSelect;


    [SerializeField] private GameObject portal;
    [SerializeField] private TextMeshProUGUI goverHeader;
    [SerializeField] private TextMeshProUGUI goverText;
    [SerializeField] private RectTransform gameoverUI;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI headToFinishText;

    [HideInInspector] public bool isGameover;
    private int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        cSelect = FindObjectOfType(typeof(CharSelect)) as CharSelect;
        AbilityHover.playerChar = cSelect.charSelection;
        if (cSelect.charSelection == EnumCharType.Melee)
        {
            characters[0].SetActive(true);
            
        }
        else
        {
            characters[1].SetActive(true);
        }
        enemyCount = CountEnemies();
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGoalsUI();
    }

    private void UpdateGoalsUI()
    {
        enemyCountText.text = "(" + (enemyCount - CountEnemies()) + "/" + enemyCount + ")";
        if(enemyCount - CountEnemies() == 0)
        {
            PlayerCanFinish();
        }
    }

    private void PlayerCanFinish()
    {
        portal.SetActive(true);
        headToFinishText.text = "Head to the portal!";
    }

    public void Win()
    {
        goverHeader.text = "You Win!";
        goverText.text = "Congratulations!";

        Time.timeScale = 0;
        gameoverUI.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        goverHeader.text = "Game Over";
        goverText.text = "Better luck next time!";

        isGameover = true;
        Time.timeScale = 0;
        gameoverUI.gameObject.SetActive(true);
    }

    private int CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length;
    }
}
