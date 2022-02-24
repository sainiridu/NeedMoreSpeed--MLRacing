using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody carSphere_RB;
    [Space(5)]

    [SerializeField] private Transform sphereParent;
    [Space(20)]
    [SerializeField] private Vector3 CarTransformOffset;
    [Space(20)]
    [SerializeField] private Transform groundRayOrigin;

    [SerializeField] private LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    [Space(20)]

    [SerializeField] private Transform wheel_FR;
    [SerializeField] private Transform wheel_FL;
    [SerializeField] private Transform wheel_RR;
    [SerializeField] private Transform wheel_RL;
    [Space(5)]
    [SerializeField] private float maxWheelTurn;
    [Space(20)]

    public float forwardAcceleration = 1f;
    [Space(5)]
    public float reverseAcceleration = 5f;
    [Space(5)]
    public float maxSpeed = 50f;
    [Space(5)]
    public float turnStrength = 180f;
    [Space(5)]
    public float gravityForce = 10f;
    [Space(5)]
    public float dragOnGround = 3f;
    [Space(5)]
    public float dragInAir = 0.1f;


    private bool isGrounded;
    private float horizontalInput;
    private float verticalInput;
    private float speedInput;
    private float turnInput;

    private float currentTurnValue;

    private float currentRotationValue = 0f;

    void Start()
    {
        carSphere_RB.transform.parent = sphereParent;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        UpdateCarPosition();
        UpdateCarRotation();
    }
    void FixedUpdate()
    {
        CheckGround();
        MoveSphere();
        //carSphere_RB.AddForce(transform.forward * forwardAcceleration * 1000);
    }


    private void ProcessInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");


        speedInput = verticalInput * forwardAcceleration * 1000f;

        turnInput = horizontalInput;
    }

    private void CheckGround()
    {
        isGrounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayOrigin.position, -this.gameObject.transform.up, out hit, groundRayLength, whatIsGround))
        {
            isGrounded = true;

            this.gameObject.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    private void MoveSphere()
    {
        if (isGrounded)
        {
            carSphere_RB.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                carSphere_RB.AddForce(this.gameObject.transform.forward * speedInput);
            }
        }
        else
        {
            carSphere_RB.drag = dragInAir;
            carSphere_RB.AddForce(Vector3.up * -gravityForce * 100);
        }
    }
    private void UpdateCarPosition()
    {
        this.gameObject.transform.position = carSphere_RB.transform.position + CarTransformOffset;
        HandleWheelRotation();
    }

    private void UpdateCarRotation()
    {
        if (isGrounded)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * verticalInput * Time.deltaTime, 0f));
            HandleWheelTurn();

        }
    }

    private void HandleWheelTurn()
    {
        currentTurnValue = turnInput * maxWheelTurn;
        wheel_FL.localRotation = Quaternion.Euler(wheel_FL.localRotation.eulerAngles.x, currentTurnValue, 180);
        wheel_FR.localRotation = Quaternion.Euler(wheel_FR.localRotation.eulerAngles.x, currentTurnValue, 0);
    }

    private void HandleWheelRotation()
    {
        Debug.Log(currentRotationValue);
        wheel_FR.localRotation = Quaternion.Euler(carSphere_RB.velocity.sqrMagnitude * Time.deltaTime * 1000f, wheel_FR.localRotation.eulerAngles.y, wheel_FR.localRotation.eulerAngles.z);
        wheel_FL.localRotation = Quaternion.Euler(carSphere_RB.velocity.sqrMagnitude * Time.deltaTime * 1000f, wheel_FL.localRotation.eulerAngles.y, wheel_FL.localRotation.eulerAngles.z);
        wheel_RL.localRotation = Quaternion.Euler(carSphere_RB.velocity.sqrMagnitude * Time.deltaTime * 1000f, wheel_RL.localRotation.eulerAngles.y, wheel_RL.localRotation.eulerAngles.z);
        wheel_RR.localRotation = Quaternion.Euler(carSphere_RB.velocity.sqrMagnitude * Time.deltaTime * 1000f, wheel_RR.localRotation.eulerAngles.y, wheel_RR.localRotation.eulerAngles.z);
    }
}
