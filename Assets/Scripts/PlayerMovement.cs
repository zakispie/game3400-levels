using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] Transform direction;

    float xInput;
    float yInput;

    Rigidbody rb;

    Vector3 moveDirection;

    [SerializeField] float drag;
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;

    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump = true;

    [SerializeField] GameObject orb;

    [SerializeField] Transform respawnPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        moveDirection = direction.forward * yInput + direction.right * xInput;

        rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);

        //check if grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        if(isGrounded)
        {
            rb.drag = drag;
        }
        else
        {
            rb.drag = 0;
        }

        Vector3 playerVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (playerVelocity.magnitude > playerSpeed)
        {
            Vector3 limitVelocity = playerVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitVelocity.x, rb.velocity.y, limitVelocity.z);
        }

        //jump
        if (Input.GetKey(KeyCode.Space) && readyToJump && isGrounded)
        {
            //Debug.Log("jump");
            readyToJump = false;
            Jump();

            Invoke(nameof(JumpCooldownReset), jumpCooldown);
        }

        if (isGrounded)
            rb.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);

        // in air
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * playerSpeed * airMultiplier, ForceMode.Force);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void JumpCooldownReset()
    {
        readyToJump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "orb")
        {
            Debug.Log("orb");
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "death")
        {
            Debug.Log("death");
            transform.position = respawnPos.position;
        }
    }
}
