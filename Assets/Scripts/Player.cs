using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerStates { IDLING, MOVING, DASHING };
    public PlayerStates state;

    Rigidbody rigidBody;
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 250f;

    float cameraRotationY;
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerStates.IDLING;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputHandler();
    }

    private void inputHandler()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        //XBOX Controller
        if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f)
        {
            float x = Input.GetAxis("Horizontal_L"), y = Input.GetAxis("Vertical_L");
            transform.eulerAngles = new Vector3(0, cameraRotationY, 0);
            float angle = get_angle(x, y), currentAngle = (transform.localEulerAngles.y % 360 + 360) % 360;
            transform.eulerAngles = new Vector3(0, angle + currentAngle, 0);
            //transform.Rotate(Vector3.up,angle- currentAngle);
            //Debug.Log("camera:"+cameraRotationY);
            //Debug.Log("character:"+angle);
            //rigidBody.AddForce(transform.forward * moveSpeed);
            state = PlayerStates.MOVING;
            rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            state = PlayerStates.IDLING;

            //Keyboard
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                rigidBody.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
                state = PlayerStates.MOVING;

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                    state = PlayerStates.MOVING;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                    state = PlayerStates.MOVING;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                    state = PlayerStates.IDLING;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                    state = PlayerStates.IDLING;
                }
            }
        }
    }

    float get_angle(float x, float y)
    {
        float theta = Mathf.Atan2(x, y) - Mathf.Atan2(0, 1.0f);
        if (theta > (float)Mathf.PI)
            theta -= (float)Mathf.PI;
        if (theta < -(float)Mathf.PI)
            theta += (float)Mathf.PI;

        theta = (float)(theta * 180.0f / (float)Mathf.PI);
        return theta;

    }

    public void changeCameraY(float y)
    {
        cameraRotationY = y;
    }
}
