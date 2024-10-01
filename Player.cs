using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float jumpDuration = 0.2f;
    private float jumpTime;
    private bool isJumping = false;

    private Vector3 originalPosition;
    private Vector3 jumpDirection;
    private Vector3 jumpTargetPosition;

    private Vector3 currentNormal = Vector3.up;

    public LayerMask layerMask;


    private void Update()
    {

        Move();
 
        if (!isJumping)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartJump();
            }
        }
        else
        {
            PerformJump();
        }

        
        UpdateGravityDirection();

    }
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (!isJumping && horizontalInput != 0)
        {
            transform.Translate(horizontalInput * 10 * Time.deltaTime, 0, 0);
        }

        if (horizontalInput < 0)
        {
            RaycastHit leftHit;
            if (Physics.Raycast(transform.position, -transform.right, out leftHit, 9999f, layerMask))
            {
                Debug.DrawRay(transform.position, -transform.right * leftHit.distance, Color.green);

                if (leftHit.distance < 0.5)
                {
                    transform.Rotate(0, 0, -90);
                }
            }

        }
        if (horizontalInput > 0)
        {
            RaycastHit rightHit;
            if (Physics.Raycast(transform.position, transform.right, out rightHit, 9999f, layerMask))
            {
                Debug.DrawRay(transform.position, transform.right * rightHit.distance, Color.yellow);

                if (rightHit.distance < 0.5)
                {
                    transform.Rotate(0, 0, 90);
                }
            }
        }

    }
    private void StartJump()
    {
        isJumping = true;
        originalPosition = transform.position;

        jumpDirection = -currentNormal; 

        jumpTargetPosition = originalPosition + jumpDirection * jumpHeight;

        jumpTime = 0f;
    }
    private void PerformJump()
    {
        jumpTime += Time.deltaTime;

        float t = jumpTime / jumpDuration;

        if (t <= 0.5f)
        {
            float phase = t * 2; 
            Vector3 jumpOffset = Vector3.Lerp(originalPosition, jumpTargetPosition, phase);

            transform.position = jumpOffset;
        }
        else
        {
            float phase = (t - 0.5f) * 2; 
            Vector3 returnOffset = Vector3.Lerp(jumpTargetPosition, originalPosition, phase);

            transform.position = returnOffset;
        }


        if (t >= 1f)
        {
            isJumping = false;
        }
    }
    private void UpdateGravityDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f, layerMask))
        {
            currentNormal = -hit.normal;
        }
        else if (Physics.Raycast(transform.position, transform.up, out hit, 1f, layerMask))
        {
            currentNormal = hit.normal;
        }
        else if (Physics.Raycast(transform.position, -transform.right, out hit, 1f, layerMask))
        {
            currentNormal = -hit.normal;
        }
        else if (Physics.Raycast(transform.position, transform.right, out hit, 1f, layerMask))
        {
            currentNormal = hit.normal;
        }
    }
}

