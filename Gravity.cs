using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public WallOrientation currentWall = WallOrientation.Floor;
    public float gravityStrength = 9.8f;

    private Vector3 currentGravityDirection;
    private Rigidbody playerRigidbopdy;


    private void Start()
    {
        playerRigidbopdy = GetComponent<Rigidbody>();
        SetGravityDirection();
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.RightShift)))
        {
            if (Input.GetKeyDown(KeyCode.W)) ChangeWall(WallOrientation.Ceiling);
            if (Input.GetKeyDown(KeyCode.S)) ChangeWall(WallOrientation.Floor);
            if (Input.GetKeyDown(KeyCode.A)) ChangeWall(WallOrientation.LeftWall);
            if (Input.GetKeyDown(KeyCode.D)) ChangeWall(WallOrientation.RightWall);


        }
        ApplyGravity();

    }
    private void ChangeWall(WallOrientation newWall)
    {
        currentWall = newWall;
        SetGravityDirection();
        RotatePlayerToWall();
    }
    private void SetGravityDirection()
    {
        switch (currentWall)
        {
            case WallOrientation.Floor:
                currentGravityDirection = Vector3.down;
                break;
            case WallOrientation.Ceiling:
                currentGravityDirection = Vector3.up;
                break;
            case WallOrientation.LeftWall:
                currentGravityDirection = Vector3.left;
                break;
            case WallOrientation.RightWall:
                currentGravityDirection = Vector3.right;
                break;
        }
    }
  
    private void ApplyGravity()
    {
        playerRigidbopdy.AddForce(currentGravityDirection * gravityStrength *Time.deltaTime);
    }
    private void RotatePlayerToWall()
    {
        Quaternion targetRotation = Quaternion.identity;
        switch (currentWall)
        {
            case WallOrientation.Floor:
                targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case WallOrientation.Ceiling:
                targetRotation = Quaternion.Euler(0, 0, 180);
                break;
            case WallOrientation.LeftWall:
                targetRotation = Quaternion.Euler(0, 0, -90);
                break;
            case WallOrientation.RightWall:
                targetRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
        playerRigidbopdy.rotation = targetRotation;
    }
}
public enum WallOrientation
{
    Floor,
    Ceiling,
    LeftWall,
    RightWall
}
