using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    private CharSelect cs;

    [SerializeField] RectTransform mainMenu;
    [SerializeField] RectTransform settingsMenu;
    [SerializeField] RectTransform charSelectMenu;

    // Start is called before the first frame update
    void Start()
    {
        cs = FindObjectOfType(typeof(CharSelect)) as CharSelect;
    }

    // Update is called once per frame
    void Update()
    {
        if (cs.selectMenu)
        {
            charSelectMenu.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            charSelectMenu.gameObject.SetActive(false);
        }
    }

    public void SelectCharacter()
    {
        cs.selectMenu = true;
    }

    public void CancelSelectCharacter()
    {
        mainMenu.gameObject.SetActive(true);
        cs.selectMenu = false;
    }

    public void OpenSettings()
    {
        settingsMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameplay(int val)
    {
        if(val == 0)
        {
            cs.charSelection = EnumCharType.Melee;
        }
        else if (val == 1)
        {
            cs.charSelection = EnumCharType.Ranged;
        }

        SceneManager.LoadScene(1);
    }
}
