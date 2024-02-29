using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Class for handling player inputs & applying physics to the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region variables
    
    // Property for player position
    public static Vector3 Position => _instance.transform.position;

    [Header("Ground Movement")] [Tooltip("Move Speed")] [SerializeField]
    private float moveSpeed;

    [Tooltip("Deceleration Strength")] [SerializeField]
    private float deceleration;

    [Header("Air Movement")]
    [Tooltip("Deceleration in-air Strength (percentage of base deceleration)")]
    [Range(0f, 1f)]
    [SerializeField]
    private float inAirDecelerationPercent;

    [Tooltip("Number of Jumps (Including First Jump)")] [Min(1)] [SerializeField]
    private int numJumps;

    [Tooltip("Jump Strength")] [SerializeField]
    private float jumpStrength;

    [Header("Other")] [Tooltip("Mouse Sensitivity (higher means more sensitive)")] [SerializeField]
    private float mouseSensitivity = 1;
    
    [Tooltip("Ground Layer")] [SerializeField]
    private LayerMask groundLayer;

    // Singleton instance of the player controller
    private static PlayerController _instance;

    // Tracks the player's current movement direction
    private Vector3 _movementDirection;

    // Tracks whether the player is currently grounded
    private bool _isGrounded;

    // Tracks num jumps player has performed
    private int _jumpCount;

    // Cache the capsule colliders
    private CapsuleCollider[] _capsuleColliders;

    // Cache the keyboard
    private Keyboard _keyboard;

    // Cache the mouse
    private Mouse _mouse;

    // Cache the camera
    private Camera _camera;
    
    // Cache the mouse input as a 2D vector
    private Vector2 _mouseInput;

    private bool _x_pressed;

    [SerializeField] RawImage fader;

    private bool _done_fading;

    [SerializeField] ChangeSprite scheduleChange;

    [SerializeField] Go go;

    [SerializeField] GameObject xPressText;

    [SerializeField] AudioSource sound;

    private bool stopped_sound;

    #endregion

    /// <summary>
    /// Assign components
    /// </summary>
    void Start()
    {
        _instance = this;
        _capsuleColliders = GetComponents<CapsuleCollider>();
        _keyboard = Keyboard.current;
        _mouse = Mouse.current;
        _camera = Camera.main;
        _mouseInput = Vector2.zero;
        _x_pressed = false;
        _done_fading = false;
        stopped_sound = false;
    }

    /// <summary>
    /// Determine necessary movement from keypresses
    /// </summary>
    void Update()
    {
        // Rotate the player based on mouse movement
        _mouseInput = _mouse.delta.ReadValue() * (mouseSensitivity * Time.deltaTime);
        transform.Rotate(Vector3.up, _mouseInput.x);
        _camera.transform.Rotate(Vector3.right, -_mouseInput.y);

        if (!_x_pressed && Input.GetKeyDown(KeyCode.X)) {
            _x_pressed = true;
            Destroy(xPressText);
        }

        if (_x_pressed) {
            if (!_done_fading)
            {
                Color faderColor = fader.color;
                faderColor.a += Time.deltaTime;
                fader.color = faderColor;
                if (fader.color.a >= 1)
                {
                    _done_fading = true;
                }
            }
            else {
                Color faderColor = fader.color;
                faderColor.a -= Time.deltaTime;
                fader.color = faderColor;
                scheduleChange.change();
                if (!stopped_sound) {
                    stopped_sound = true;
                    sound.Stop();
                }
                go.go();
            }
        }
    }
}