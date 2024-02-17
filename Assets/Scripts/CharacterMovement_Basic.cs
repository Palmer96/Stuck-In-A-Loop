using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_Basic : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;

    private float m_currentSpeed = 2.0f;
    [SerializeField]
    private float m_walkSpeed = 2.0f;
    [SerializeField]
    private float m_runSpeed = 4.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_currentSpeed = m_runSpeed;
        }
        else
        {
            m_currentSpeed = m_walkSpeed;
        }
        

        UpdateMovement();
    }

    void UpdateMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 moveForward = Input.GetAxis("Vertical") * transform.forward;
        Vector3 moveRight = Input.GetAxis("Horizontal") * transform.right;

        Vector3 move = moveForward + moveRight;
        controller.Move(move * Time.deltaTime * m_currentSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


}
