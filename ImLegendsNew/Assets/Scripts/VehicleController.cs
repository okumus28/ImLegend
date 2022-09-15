using System;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed;
    float maxSpeed;

    public float currentArmor;
    [SerializeField]float maxArmor;

    public float currentFuel;
    [SerializeField]float fuelTank;

    private float horizontalInput;
    [SerializeField]private float maxSteeringAngle;

    public float maxMotorForce;
    private float currentMotorForce;

    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;

    Rigidbody rb;
    [SerializeField]
    private Transform frontLeftTransform;
    [SerializeField]
    private Transform frontRightTransform;
    [SerializeField]
    private Transform backLeftTransform;
    [SerializeField]
    private Transform backRightTransform;

    public Transform ibre;
    public Transform armorIbre;
    public Transform fuelIbre;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0,0,0);
        currentMotorForce = maxMotorForce;
        maxSpeed = GameManager.instance.properties.speed;
        maxArmor = GameManager.instance.properties.armor;
        fuelTank = GameManager.instance.properties.fuelTank;

        currentArmor = maxArmor;
        currentFuel = fuelTank;

        rb.velocity = new Vector3(0,0,maxSpeed / 7.2f);
    }
    private void FixedUpdate()
    {
        float yAngle = transform.eulerAngles.y * 100;

        if (horizontalInput == 0 && yAngle > .25f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .05f);
        }
        else if (horizontalInput == 0 && yAngle < -.25f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .05f);
        }

        currentArmor -= Time.fixedDeltaTime;
        currentFuel -= Time.fixedDeltaTime;

        armorIbre.localEulerAngles = new Vector3(0, 0, 75 + (300 - currentArmor));
        fuelIbre.localEulerAngles = new Vector3(0, 0, 105 - ((100 - currentFuel) * 2.3f));

        GetInput();
        SpeedOmeter();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        //Deneme();

        //Debug.Log((frontLeftCollider.rpm / 60 * 360 * Time.deltaTime, 0, 90));
    }
    public void HandleMotor()
    {
        if (currentArmor <= 0 || currentFuel <= 0)
        {
            currentMotorForce = 0;
            rearLeftCollider.brakeTorque = 15000;
            rearRightCollider.brakeTorque = 15000;
        }
        else if (speed >= maxSpeed)
        {
            currentMotorForce = 0;
            Debug.Log("else if");
        }
        else
        {
            currentMotorForce = maxMotorForce;
        }

        rearLeftCollider.motorTorque = currentMotorForce;
        rearRightCollider.motorTorque = currentMotorForce;

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleSteering()
    {
        float currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void SpeedOmeter()
    {
        var vel = rb.velocity;
        speed = vel.magnitude * 3.6f;
        ibre.localEulerAngles = new Vector3(0, 0, 270 - (speed * 0.9f));
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftTransform);
        UpdateSingleWheel(frontRightCollider, frontRightTransform);
        UpdateSingleWheel(rearLeftCollider, backLeftTransform);
        UpdateSingleWheel(rearRightCollider, backRightTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos , out rot);
        rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, 90);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadTag"))
        {
            RoadController.instance.RoadCreate();
        }
    }
}
