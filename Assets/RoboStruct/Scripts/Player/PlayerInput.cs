using UnityEngine;
using UnityEngine.InputSystem;

namespace RoboStruct
{
    /// <summary>
    /// Singleton that handles the player's input.
    /// </summary>
    public class PlayerInput : MonoBehaviourSingleton<PlayerInput>
    {
        [HideInInspector] public Rigidbody2D VirtualMouseRigidBody;
        public UnityEngine.InputSystem.PlayerInput UnityPlayerInput;

        [SerializeField] private float _virtualMouseSpeed;
        [SerializeField] private float _gamepadSpeed;
        [SerializeField] private float _gamepadDrag;
        private Vector2 _mousePosition;
        private Vector2 _virtualMousePosition;
        private Vector2 _moveInput;
        private bool _inputIsEnabled;

        public enum ControlScheme
        {
            KeyboardMouse,
            Gamepad
        }

        public ControlScheme CurrentControlScheme { get; private set; }
        public float SpeedMultipler { get; set; } = 1;

        private void Start()
        {
            UpdateControlScheme();
            CreateVirtualMouse();
            SetCursorMode(CursorLockMode.Confined);
        }

        private void Update()
        {
            _inputIsEnabled = !GameManager.Instance.IsPlayerControlDisabled;
            if (CurrentControlScheme == ControlScheme.KeyboardMouse && _inputIsEnabled) LerpMoveToMouse();
        }

        private void FixedUpdate()
        {
            if (_inputIsEnabled)
            {
                switch (CurrentControlScheme)
                {
                    case ControlScheme.KeyboardMouse:
                        VirtualMouseRigidBody.MovePosition(_virtualMousePosition);
                        break;
                    case ControlScheme.Gamepad:
                        VirtualMouseRigidBody.AddForce(_moveInput * _gamepadSpeed * SpeedMultipler);
                        break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (UnityEngine.Debug.isDebugBuild && VirtualMouseRigidBody)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(VirtualMouseRigidBody.position, 0.25f);
            }
        }

        public void OnMove(InputValue input) => _moveInput = input.Get<Vector2>();

        private void SetCursorMode(CursorLockMode mode)
        {
            UnityEngine.Cursor.lockState = mode;
        }

        private void UpdateControlScheme()
        {
            if (SettingsManager.Instance.CurrentSettings.IsVirtualGamepadEnabled)
            {
                CurrentControlScheme = ControlScheme.Gamepad;
            }
            else
            {
                CurrentControlScheme = ControlScheme.KeyboardMouse;
            }
        }

        private void CreateVirtualMouse()
        {
            GameObject virtualMouseGameObject = new GameObject();
            virtualMouseGameObject.name = "Virtual Mouse Pointer";
            virtualMouseGameObject.layer = LayerMask.NameToLayer("VirtualInput");
            VirtualMouseRigidBody = virtualMouseGameObject.AddComponent<Rigidbody2D>();
            VirtualMouseRigidBody.gravityScale = 0;
            VirtualMouseRigidBody.drag = _gamepadDrag;
            CircleCollider2D virtualMouseCollider = virtualMouseGameObject.AddComponent<CircleCollider2D>();
        }

        private void LerpMoveToMouse()
        {
            _mousePosition = Mouse.current.position.ReadValue();
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            _mousePosition.x = Mathf.Clamp(_mousePosition.x, -75, 75);
            _mousePosition.y = Mathf.Clamp(_mousePosition.y, -40, 40); // Gets canvas size, auto sizes clamp
            _virtualMousePosition = Vector2.Lerp(transform.position, _mousePosition, (_virtualMouseSpeed * SpeedMultipler) / 100); // Lerp to smooth speed and prevent laggy movements
        }
    }

}
