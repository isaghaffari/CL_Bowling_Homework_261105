using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollBall : MonoBehaviour
{
    private Rigidbody rigidBody;

    private InputAction bowlButt;

    private bool holdingButt;
    private bool roll;
    public float rollPower;
    
    public LayerMask laneLayer;
    public GameManager myManager;
    private float currentTimer = 0f;
    private bool activeTimer = false;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private bool ballRollable;
    private bool rollTimerActive;
    private float rollTimer;
    public float maxLaunch = 100f;
    public float minLaunch = 20f;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //rigidBody.AddForce(transform.forward * 8000);
        bowlButt = InputSystem.actions.FindAction("Bowl");
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        ballRollable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTimer)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= 2.5f)
            {
                BallReset();
            }
        }
        if (ballRollable)
        {
            if (bowlButt.WasPressedThisFrame())
            {
                holdingButt = true;
                rollTimerActive = true;
                //StartRoll();
            } else if (bowlButt.WasReleasedThisFrame())
            {
                holdingButt = false;
                Debug.Log(rollPower);
                if (rollPower < minLaunch)
                {
                    rollPower = minLaunch;
                }

                if (rollPower > maxLaunch)
                {
                    rollPower = maxLaunch;
                }
                roll = true;
                rollTimerActive = false;
            }
           
        }

    }

    private void FixedUpdate()
    {
        if (holdingButt && rigidBody.linearVelocity == Vector3.zero)
        {
            Debug.DrawRay(transform.position, FindDirection() * 1000f, Color.red);
        }
        if (rollTimerActive)
        {
            rollPower += .3f;
        }
        if (roll)
        {
            StartRoll();
        }
        
    }

    void StartRoll()
    {
        if (Physics.Raycast(transform.position, FindDirection(),10000f,laneLayer))
        {
            Debug.Log(rollPower);
            rigidBody.isKinematic = false;
            rigidBody.AddForce(rollPower * transform.TransformDirection(FindDirection()), ForceMode.Impulse);
            roll = false;
            ballRollable = false;
            rollPower = 20f;
        }
        //roll based on mouse direction
    }

    Vector3 FindDirection()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition);
        direction.z = 10f;
        return direction.normalized;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gutter"))
        {
            //myManager.activeTimer = true;
            Debug.Log("Gutter");
            activeTimer = true;
        }
        if (other.CompareTag("Pins"))
        {
            //myManager.activeTimer = true;
            Debug.Log("Pins");
            activeTimer = true;
        }
    }
    
    void BallReset()
    {
        Debug.Log("Reset");
        transform.position = startingPosition;
        rigidBody.linearVelocity = Vector3.zero; 
        rigidBody.angularVelocity = Vector3.zero;
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        activeTimer = false;
        currentTimer = 0f;
        ballRollable = true;
    }
}
