using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscController : MonoBehaviour
{
    float rollInput;
    float pitchInput;
    Quaternion targetRotation;
    float start;
    bool started = false;
    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void GetInput()
    {
        rollInput = Input.GetAxis("Horizontal");
        pitchInput = Input.GetAxis("Vertical");
        start = Input.GetAxis("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (!started)
        {
            if (Mathf.Abs(start) > inputDelay)
            {
                rigidBody.velocity = transform.forward * forwardVel;
                rigidBody.useGravity = true;
                started = false;
            }
        }
    }

    void Turn()
    {
        if (Mathf.Abs(rollInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVel * rollInput * Time.deltaTime, Vector3.forward);
        }
        if (Mathf.Abs(pitchInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVel * pitchInput * Time.deltaTime, Vector3.right);
        }
        transform.rotation = targetRotation;
    }
}
