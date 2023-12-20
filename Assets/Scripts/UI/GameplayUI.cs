using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private RectTransform pauseMenu;
    [SerializeField] private RectTransform gameplayUI;
    [SerializeField] private RectTransform gameoverMenu;

    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {

        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    public void GoToMainMenu(int val)
    {
        Time.timeScale = 1;
        CharSelect cs = FindObjectOfType(typeof(CharSelect)) as CharSelect;
        if (val == 0)
        {
            cs.selectMenu = true;
        }
        else
        {
            cs.selectMenu = false;
        }
        SceneManager.LoadScene(0);

    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        isPaused = false;
        pauseMenu.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
