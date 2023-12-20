using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public static BGMController Instance;
    private CharSelect cs;

    [SerializeField] private AudioSource mainMenu;
    [SerializeField] private AudioSource charSelect;
    [SerializeField] private AudioSource gameplay;

    private void Start()
    {
        if (BGMController.Instance is null)
        {
            return;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        cs = FindObjectOfType(typeof(CharSelect)) as CharSelect;
        MuteAll();
    }

    void Update()
    {
        MuteAll();
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (cs.selectMenu)
            {
                charSelect.mute = false;
            }
            else
            {
                mainMenu.mute = false;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameplay.mute = false;
        }
    }

    private void MuteAll()
    {
        mainMenu.mute = true;
        charSelect.mute = true;
        gameplay.mute = true;
    }
}
