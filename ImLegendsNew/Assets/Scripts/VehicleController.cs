using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;

    private float horizontalInput;
    [SerializeField]private float maxSteeringAngle;

    public float maxMotorForce;
    private float currentMotorForce;

    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,1,0);
        currentMotorForce = maxMotorForce;
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

        GetInput();
        SpeedOmeter();
        HandleMotor();
        HandleSteering();
    }
    public void HandleMotor()
    {
        if (speed >= maxSpeed)
        {
            currentMotorForce = 0;
        }
        else
        {
            currentMotorForce = maxMotorForce;
        }

        frontLeftCollider.motorTorque = currentMotorForce;
        frontRightCollider.motorTorque = currentMotorForce;

        Debug.Log(currentMotorForce);

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
    }
}
