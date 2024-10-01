using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    private ConstantForce constForce;
    private Vector3 forceDir;
    // Start is called before the first frame update
    void Start()
    {
        constForce = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if(constForce.force.x != 0)
        {
            if (verticalInput != 0)
            {
                forceDir = new Vector3(constForce.force.x, verticalInput * 10, 0);
                constForce.force = forceDir;
            }
 /*           else if (verticalInput < 0)
            {
                forceDir = new Vector3(0, verticalInput * 10, 0);
                constantForce.force = forceDir;
            }*/
        }
        if (constForce.force.y != 0)
        {
            if (horizontalInput != 0)
            {
                forceDir = new Vector3(horizontalInput*10, constForce.force.y * 10, 0);
                constForce.force = forceDir;
            }
        }



            
        }
}
