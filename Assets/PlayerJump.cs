using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float fallMulitiplier = 2.5f;
    public float lowJumpMulitiplier = 2f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMulitiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.JoystickButton0))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMulitiplier - 1) * Time.deltaTime;
        }
    }
}
