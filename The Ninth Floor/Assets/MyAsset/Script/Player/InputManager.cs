using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool InteractPressed { get; private set; } // Added to check if 'F' is pressed

        public PlayerInputSystem _input;


        private void Awake()
        {
            _input = new PlayerInputSystem();

            _input.Player.Movement.performed += ctx => Move = ctx.ReadValue<Vector2>();
            _input.Player.Movement.canceled += ctx => Move = Vector2.zero;

            _input.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
            _input.Player.Look.canceled += ctx => Look = Vector2.zero;

            _input.Player.Sprint.performed += ctx => IsSprinting = true;
            _input.Player.Sprint.canceled += ctx => IsSprinting = false;

            _input.Player.Interact.performed += ctx => InteractPressed = true;  // Set InteractPressed to true when 'F' is pressed
            _input.Player.Interact.canceled += ctx => InteractPressed = false;  // Reset InteractPressed when 'F' is released
        }

        private void Start()
        {
            HideAndLockCursor();
            ToggleActionMap(_input.Player);
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void HideAndLockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ToggleActionMap(InputActionMap actionMap)
        {


            _input.Disable();

            actionMap.Enable();

            UpdateCursorState(actionMap);
        }

        private void UpdateCursorState(InputActionMap activeActionMap)
        {
            if (activeActionMap == _input.UI.Get()) 
            {
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true; 
            }
            else 
            {
                Cursor.lockState = CursorLockMode.Locked; 
                Cursor.visible = false; 
            }
        }

    }
}
