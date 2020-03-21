using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public void Initialize(GameObject character)
    {
        m_animator = character.GetComponent<Animator>();
        m_rigidBody = character.GetComponent<Rigidbody>();
    }

    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    [SerializeField] private bool m_wasGrounded;

    [SerializeField] private float m_inAirTimer;
    [SerializeField] private float fallingAnimationMargin = 0.25f;
    [SerializeField] private float maxFallingSpeed = -30.0f;
    [SerializeField] private float jumpMargin = 0.25f;
    [SerializeField] private float lateJumpCompensationScale = 1.2f;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.3f;

    private CapsuleCollider col;
    [SerializeField] private bool m_isGrounded;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool isCollisionEntered;

    public GameObject m_key;
    private bool m_isPickingUp;

    public bool keyInRange;

    public bool hasKey;
    public bool isKeyboard;
    public bool isRespawning;
    [SerializeField] public bool onWood;
    [SerializeField] public bool isPlayingFootsteps;
    [SerializeField] public bool jumped;
    private List<Collider> m_collisions = new List<Collider>();

    private AudioController ac;
    
    void Awake()
    {
        col = gameObject.GetComponent<CapsuleCollider>();
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        isCollisionEntered = false;
        //isKeyboard = true;
        jumped = false;
        keyInRange = false;
        hasKey = false;
        m_inAirTimer = 0f;
        isRespawning = false;
        ac = GameObject.FindWithTag("AudioController").GetComponent<AudioController>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            onWood = true;
            ac.setFootstepsTextrue(1.0f);
        }
        else
        {
            onWood = false;
            ac.setFootstepsTextrue(0f);
        }

        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
                jumped = false;
                isCollisionEntered = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isCollisionEntered)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                jumped = false;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) 
                { 
                    m_isGrounded = false;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0)
        {
            m_isGrounded = false;
        }
    }
    

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayer);
    }

    void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);
        
        if ( m_rigidBody.velocity.y < maxFallingSpeed )
        {
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, maxFallingSpeed, m_rigidBody.velocity.z);
        }
        
        if (!m_isGrounded)
        {
            m_inAirTimer += Time.deltaTime;
        }
        else
        {
            if (m_inAirTimer > 0.5f)
                ac.playLand();
            m_inAirTimer = 0f;
        }

            switch (m_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }

            m_wasGrounded = m_isGrounded;

    }

    private void TankUpdate()
    {
        detectInputMethod();

        float v = 0, h = 0;

        if (isKeyboard)
        {
            if (Input.GetKey(KeyCode.W))
                v = 1.0f;
            if (Input.GetKey(KeyCode.S))
                v = -1.0f;
            if (Input.GetKey(KeyCode.A))
                h = -1.0f;
            if (Input.GetKey(KeyCode.D))
                h = 1.0f;

        }
        else
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
        }

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        if (!isRespawning)
        {
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            JumpingAndLanding();
        }

    }

    private void DirectUpdate()
    {
        detectInputMethod();

        float v = 0, h = 0;

        if (isKeyboard)
        {
            if (Input.GetKey(KeyCode.W))
                v = 1.0f;
            if (Input.GetKey(KeyCode.S))
                v = -1.0f;
            if (Input.GetKey(KeyCode.A))
                h = -1.0f;
            if (Input.GetKey(KeyCode.D))
                h = 1.0f;

        }
        else
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");
        }
        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        if (!isRespawning)
        {
            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

                

                m_animator.SetFloat("MoveSpeed", direction.magnitude);

            }
            
        }

        JumpingAndLanding();

    }

    private void detectInputMethod()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isKeyboard = true;
            if (m_inAirTimer <= jumpMargin && !isPlayingFootsteps && !jumped)
            {
                ac.playFootsteps(true);
                isPlayingFootsteps = true;
            }
        }
        else if (Mathf.Abs(Input.GetAxis("Horizontal_L")) > 0.19f || Mathf.Abs(Input.GetAxis("Vertical_L")) > 0.19f && !jumped)
        {
            isKeyboard = false; 

            if (m_inAirTimer <= jumpMargin && !isPlayingFootsteps && !jumped)
            {
                ac.playFootsteps(true);
                isPlayingFootsteps = true;
            }
        }
        else 
        {
            ac.playFootsteps(false);
            isPlayingFootsteps = false;
        }
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if ((Input.GetKey(KeyCode.Joystick1Button0) && !isKeyboard) || (Input.GetKey(KeyCode.Space) && isKeyboard))
        {
            if (jumpCooldownOver)
            {
                if (m_inAirTimer <= jumpMargin)
                {
                    m_jumpTimeStamp = Time.time;
                    if (m_inAirTimer == 0f)
                    {
                        m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                    }
                    else
                    {
                        m_rigidBody.AddForce(Vector3.up * m_jumpForce * lateJumpCompensationScale, ForceMode.Impulse);
                    }
                    ac.playJump();
                    jumped = true;
                    m_animator.SetTrigger("Jump");
                    m_isGrounded = false;
                    ac.playFootsteps(false);
                    isPlayingFootsteps = false;
                    isCollisionEntered = false;
                }
                
            }
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0f, m_rigidBody.velocity.z);
        }

        
        if (!m_isGrounded && !m_wasGrounded && m_inAirTimer >= fallingAnimationMargin && isCollisionEntered)
        {
            m_animator.SetTrigger("Jump");
        }
        
    }

    public void pickingUpKey()
    {
        ac.playPickupKey();
        m_animator.SetTrigger("Pickup");
    }

    public bool pickUpAnimation()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).IsName("Pickup");
    }

    public void setGrounded(bool b)
    {
        m_isGrounded = b;
    }
}
