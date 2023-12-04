using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CamManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CineVC;
    [SerializeField] private Camera cam;

    private bool usingVC = true;

    private void Awake()
    {
        PlayerMovement player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        CineVC.Follow = player.gameObject.transform;
    }

    private void Update()
    {
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
            CineVC.gameObject.SetActive(true);
        }
        else
        {
            CineVC.gameObject.SetActive(false);
        }

    }
}
