using System;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float currentSpeed;
    float maxSpeed;

    public float currentArmor;
    [SerializeField]float maxArmor;

    public float currentFuel;
    [SerializeField]float fuelTank;

    private float horizontalInput;
    [SerializeField]private float maxSteeringAngle;
    public float currentSteeringAngle;

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

    int killedZombi;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
        currentMotorForce = maxMotorForce;
        maxSpeed = GameManager.instance.properties.speed;
        maxArmor = GameManager.instance.properties.armor;
        fuelTank = GameManager.instance.properties.fuelTank;

        currentArmor = maxArmor;
        currentFuel = fuelTank;

        //rb.velocity = new Vector3(0,0,maxSpeed / 7.2f);
    }
    private void FixedUpdate()
    {
        float yAngle = transform.eulerAngles.y * 100;

        //if (horizontalInput == 0 && yAngle > .25f)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .05f);
        //}
        //else if (horizontalInput == 0 && yAngle < -.25f)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), .05f);
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            killedZombi++;
        }

        GameManager.instance.ArmorMeter(currentArmor);
        GameManager.instance.FuelMeter(currentFuel);
        GameManager.instance.DistanceCal(transform.position.z);
        GameManager.instance.MonsterMode(killedZombi);

        GetInput();
        //Deneme();
        SpeedOmeter();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    public void HandleMotor()
    {
        if (currentArmor <= 0 || currentFuel <= 0)
        {
            currentMotorForce = 0;
            rb.velocity *= .975f;
            rearLeftCollider.brakeTorque = 5000;
            rearRightCollider.brakeTorque = 5000;
            frontLeftCollider.brakeTorque = 5000;
            frontRightCollider.brakeTorque = 5000;

            return;
        }
        else if (currentSpeed >= maxSpeed)
        {
            currentMotorForce = 0;
        }
        else
        {
            currentMotorForce = maxMotorForce;
        }

        currentArmor -= Time.fixedDeltaTime;
        currentFuel -= Time.fixedDeltaTime;

        rearLeftCollider.motorTorque = currentMotorForce;
        rearRightCollider.motorTorque = currentMotorForce;

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //horizontalInput = Input.GetAxis("Horizontal");
    }

    public void Deneme()
    {
        transform.position += new Vector3(horizontalInput / 5, 0, 0);
    }

    private void HandleSteering()
    {
        //float currentSteerAngle = maxSteeringAngle * horizontalInput;
        currentSteeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftCollider.steerAngle = currentSteeringAngle;
        frontRightCollider.steerAngle = currentSteeringAngle;
    }

    private void SpeedOmeter()
    {
        var vel = rb.velocity;
        currentSpeed = vel.magnitude * 3.6f;
        GameManager.instance.SpeedoMeter(currentSpeed);
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
