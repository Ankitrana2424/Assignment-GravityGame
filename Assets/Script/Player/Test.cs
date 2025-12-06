using System;
using UnityEngine;

public class Test : MonoBehaviour
{

    public bool IsGrounded => CheckGrounded();
    private float jumpBufferTime = 0.15f;
    private float coyoteTime = .1f;

    private float jumpBufferCounter = 0;
    private float coyoteCounter = 0;
    Vector2 moveInput;

    private void Update()
    {
        ReadInput();
        HandleTimer();

    }



    private void ReadInput()
    {
        moveInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveInput.y = 1f;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1f;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1f;
    }
    private void HandleTimer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (IsGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        jumpBufferCounter = Mathf.Max(0, jumpBufferTime);
        coyoteCounter = Mathf.Max(0, coyoteTime);




    }
}
