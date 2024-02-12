using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Cache the rigidbody
    private Rigidbody _rigidbody;

    // Cache the keyboard
    private Keyboard _keyboard;

    // Cache the mouse
    private Mouse _mouse;

    // Cache the camera
    private Camera _camera;
    
    // Cache the mouse input as a 2D vector
    private Vector2 _mouseInput;
    
    // Cache the light component (flashlight)
    private Light _flashlight;

    #endregion

    /// <summary>
    /// Assign components
    /// </summary>
    void Start()
    {
        _instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleColliders = GetComponents<CapsuleCollider>();
        _keyboard = Keyboard.current;
        _mouse = Mouse.current;
        _camera = Camera.main;
        _mouseInput = Vector2.zero;
        _flashlight = GetComponentInChildren<Light>();
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
        
        if (_mouse.leftButton.wasPressedThisFrame)
        {
            _flashlight.enabled = !_flashlight.enabled;
        }

        // Allow jump if player is grounded or has not performed double jump
        if (_keyboard.spaceKey.wasPressedThisFrame && (_isGrounded || _jumpCount < numJumps - 1))
        {
            _rigidbody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            _jumpCount++;
        }

        // Handle Inputs
        if (_keyboard.aKey.isPressed)
        {
            _movementDirection = -transform.right;
        }
        else if (_keyboard.dKey.isPressed)
        {
            _movementDirection = transform.right;
        }
        else if (_keyboard.wKey.isPressed)
        {
            _movementDirection = transform.forward;
        }
        else if (_keyboard.sKey.isPressed)
        {
            _movementDirection = -transform.forward;
        }
        else
        {
            _movementDirection = Vector3.zero;
        }
    }

    /// <summary>
    /// Update the physics of the rigidbody based on current movement direction
    /// </summary>
    void FixedUpdate()
    {
        // Check if player is grounded
        _isGrounded = IsGrounded();
        if (_isGrounded)
        {
            _jumpCount = 0;
        }

        // Initialize movement vector and apply to the rigidbody
        Vector3 movement = _movementDirection * (moveSpeed * Time.fixedDeltaTime);

        if (movement == Vector3.zero)
        {
            // No movement input, we should decelerate
            Vector3 curVelocity = _rigidbody.velocity;
            // Apply the deceleration force (not on y-axis) via a Lerp
            if (_isGrounded)
            {
                _rigidbody.velocity =
                    Vector3.Lerp(curVelocity, new Vector3(0, curVelocity.y, 0),
                        deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _rigidbody.velocity =
                    Vector3.Lerp(curVelocity, new Vector3(0, curVelocity.y, 0),
                        (deceleration * inAirDecelerationPercent) * Time.fixedDeltaTime);
            }
        }
        else
        {
            // Movement input requested, apply movement force in that direction
            _rigidbody.AddForce(movement, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// Determines if the player is currently grounded
    /// </summary>
    /// <returns> True if the player is grounded, false otherwise </returns>
    bool IsGrounded()
    {
        // The position, height, and radius of the capsule's collider.
        Vector3 capsulePosition = _capsuleColliders[0].transform.position;
        float capsuleHeight = _capsuleColliders[0].height;
        float capsuleRadius = _capsuleColliders[0].radius;

        // Check for overlapping colliders below the capsule to determine if it's grounded.
        Collider[] hitColliders = Physics.OverlapCapsule(
            capsulePosition + new Vector3(0, capsuleHeight / 2 - capsuleRadius, 0),
            capsulePosition - new Vector3(0, capsuleHeight / 2 - capsuleRadius, 0),
            capsuleRadius, groundLayer);

        print(hitColliders.Length);

        return hitColliders.Length > 0;
    }
}