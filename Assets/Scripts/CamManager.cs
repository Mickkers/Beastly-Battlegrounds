using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;

public class CamManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CineVC;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI camLockUI;

    private bool usingVC = true;
    private bool asigned;

    private void Awake()
    {
        asigned = false;
        usingVC = false;
    }

    private void Update()
    {
        if (!asigned)
        {
            PlayerMovement player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
            if (player is null) return;
            CineVC.Follow = player.gameObject.transform;
            asigned = true;
            usingVC = true;
            
        }
        if (!usingVC)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            if(mousePos.x < 10)
            {
                cam.transform.position -= Vector3.forward * 10 * Time.deltaTime;
                cam.transform.position -= Vector3.left * 10 * Time.deltaTime;
            }
            else if(mousePos.x > Screen.width - 10)
            {
                cam.transform.position -= Vector3.right * 10 * Time.deltaTime;
                cam.transform.position -= Vector3.back * 10 * Time.deltaTime;
            }

            if (mousePos.y < 10)
            {
                cam.transform.position -= Vector3.back * 10 * Time.deltaTime;
                cam.transform.position -= Vector3.left * 10 * Time.deltaTime;
            }
            else if (mousePos.y > Screen.height - 10)
            {
                cam.transform.position -= Vector3.forward * 10 * Time.deltaTime;
                cam.transform.position -= Vector3.right * 10 * Time.deltaTime;
            }
        }
    }

    public void CameraLock()
    {
        usingVC = !usingVC;

        if (usingVC)
        {
            camLockUI.text = "Camera Locked\n(Alt)";
            CineVC.gameObject.SetActive(true);
        }
        else
        {
            camLockUI.text = "Camera Unlocked\n(Alt)";
            CineVC.gameObject.SetActive(false);
        }

    }
}
