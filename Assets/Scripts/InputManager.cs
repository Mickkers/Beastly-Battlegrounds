using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.GameplayActions gameplay;

    private PlayerMovement playerMovement;
    private PlayerAction playerAction;
    private CamManager camManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        camManager = FindObjectOfType(typeof(CamManager)) as CamManager;
        gameplay = playerInput.Gameplay;
        playerMovement = GetComponent<PlayerMovement>();
        playerAction = GetComponent<PlayerAction>();

        gameplay.CameraLock.performed += ctx => camManager.CameraLock();
        gameplay.BasicAttack.performed += ctx => playerMovement.MoveToEnemy(Mouse.current.position.ReadValue());
        gameplay.Ultimate.performed += ctx => playerAction.Ultimate();
        gameplay.Skill.performed += ctx => playerAction.Skill();
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplay.Move.IsPressed())
        {
            playerMovement.Move(Mouse.current.position.ReadValue());
        }
    }

    public static Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }
}
