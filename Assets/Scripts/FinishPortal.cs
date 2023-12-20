using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager gm = FindObjectOfType(typeof(GameManager)) as GameManager;
            gm.Win();
        }
    }
}
