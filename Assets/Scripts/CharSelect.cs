using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    public static CharSelect Instance;
    public EnumCharType charSelection;
    public bool selectMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        if (CharSelect.Instance is null)
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
    }

    public void SetChar(EnumCharType val)
    {
        charSelection = val;
    }
}
