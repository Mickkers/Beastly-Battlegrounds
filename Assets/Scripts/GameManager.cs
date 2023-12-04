using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = CountEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGoalsUI();
    }

    private void UpdateGoalsUI()
    {
        enemyCountText.text = "(" + (enemyCount - CountEnemies()) + "/" + enemyCount + ")";
    }

    private int CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length;
    }

    public void CharSelect()
    {

    }

    public void MainMenu()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
}
