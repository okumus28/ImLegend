using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CarController : MonoBehaviour {

	public enum WheelDrive
	{
		frontWD,
		backWD,
		fourWD,
	}

	//public WheelDrive wheelDrive;
	private Rigidbody rb;

	[Header("Wheels")]
	[SerializeField] private WheelCollider frontRightCollider;
	[SerializeField] private WheelCollider frontLeftCollider;
	[SerializeField] private WheelCollider backRightCollider;
	[SerializeField] private WheelCollider backLeftCollider;

	[Header("Vehicle")]
	[SerializeField] private float motorForce;
	private float currentMotorForce;
	[SerializeField] private float breakForce;
	private float currentBreakForce;
	[SerializeField] private float maxSteerAngle;

	[Header("Properties")]

	[SerializeField]private float maxSpeed;
	public float currentSpeed;

	[System.NonSerialized] public float maxArmor;
	public float currentArmor;

    [System.NonSerialized] public float maxFuel;
	public float currentFuel;

	//private WheelCollider[] wheels;

	// inputs

	private float horizontalInput;
	[SerializeField]private bool isBreaking;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{
		//wheels = GetComponentsInChildren<WheelCollider>();

		rb.centerOfMass = new Vector3(0,0.1f,0);

		//Get properties
        maxSpeed = GameManager.instance.properties.speed;
        maxArmor = GameManager.instance.properties.armor;
        maxFuel = GameManager.instance.properties.fuelTank;

        currentArmor = maxArmor;
		currentFuel = maxFuel;

		//Start speed -- maxspeed / 2
		rb.velocity = new Vector3(0, 0, maxSpeed / 7.2f);
	}

	public void Update()
	{
		ApplyBrake();
		if (GameManager.instance.gameOver)
		{			
			return;
		}
		GetInput();
		HandleMotor();
		SteeringAngle();
		DashBoard();
	}

	void DashBoard()
	{
        currentSpeed = rb.velocity.magnitude * 3.6f;

        GameManager.instance.ArmorMeter(currentArmor);
        GameManager.instance.FuelMeter(currentFuel);
        GameManager.instance.DistanceCal(transform.position.z);
        GameManager.instance.SpeedoMeter(currentSpeed);
        //GameManager.instance.MonsterMode(killedZombi);
    }

	void GetInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		horizontalInput *= isBreaking ? -1 : 1;
		

        transform.rotation = horizontalInput == 0 ? Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, 0, 1), 10 * Time.deltaTime) : transform.rotation;

		isBreaking = Input.GetKey(KeyCode.Space);
    }
    void HandleMotor()
	{
		currentMotorForce = currentSpeed >= maxSpeed ? 0 : motorForce;

		if (currentArmor <= 0 || currentFuel <= 0)
		{
			currentMotorForce = 0;
			//rb.velocity *= .98f;

			isBreaking = true;
			ApplyBrake();

			//return;
		}

		//currentArmor -= Time.fixedDeltaTime;
		currentFuel -= Time.fixedDeltaTime / 1.5f;


		backRightCollider.motorTorque = currentMotorForce;
		backLeftCollider.motorTorque = currentMotorForce;
	}

	void ApplyBrake()
	{
		currentBreakForce = isBreaking ? breakForce : 0;


		frontLeftCollider.brakeTorque = currentBreakForce;
		frontRightCollider.brakeTorque = currentBreakForce;
		backLeftCollider.brakeTorque = currentBreakForce;
		backRightCollider.brakeTorque = currentBreakForce;
	}


	void SteeringAngle()
	{
		float currentAngle = horizontalInput * maxSteerAngle;

		frontLeftCollider.steerAngle = currentAngle;
		frontRightCollider.steerAngle = currentAngle;
	}

	void OnCollisionEnter(Collision other)
	{
		Invoke(nameof(ObstacleTrigger), .5f);
	}

	void ObstacleTrigger()
	{
		if (currentSpeed <= 5)
		{
			currentArmor = 0;	
		}
		else
		{
			currentArmor -= currentSpeed / 4;
		}
	}
}
