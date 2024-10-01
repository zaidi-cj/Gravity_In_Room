using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody playerRigidBody;
    private Gravity wallgravity;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        wallgravity = GetComponent<Gravity>();
    }

    private void Update()
    {
        Vector3 movement = GetMovementInput();
        MovePlayer(movement);
    }
    private Vector3 GetMovementInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;
        switch (wallgravity.currentWall)
        {
            case WallOrientation.Floor:
            case WallOrientation.Ceiling:
                movement = new Vector3(moveHorizontal,moveVertical,0);
                break;
            case WallOrientation.LeftWall:
            case WallOrientation.RightWall:
                movement = new Vector3(moveHorizontal,moveVertical,0);
                break;
        }
        return movement.normalized;
    }
    private void MovePlayer(Vector3 movement)
    {
        playerRigidBody.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
    }

}
