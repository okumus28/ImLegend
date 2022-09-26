using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour 
{
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
		
		GetComponent<CarAnimation>().enabled = SceneManager.GetActiveScene().name != "GameScene";
		GetComponent<AudioSource>().enabled = SceneManager.GetActiveScene().name != "GameScene";

        frontLeftCollider.enabled = SceneManager.GetActiveScene().name == "GameScene";
        frontRightCollider.enabled = SceneManager.GetActiveScene().name == "GameScene";
		backLeftCollider.enabled = SceneManager.GetActiveScene().name == "GameScene";
		backRightCollider.enabled = SceneManager.GetActiveScene().name == "GameScene";
        enabled = SceneManager.GetActiveScene().name == "GameScene";
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

		//print("magnitude"+rb.velocity.magnitude);
		//print("velocity"+rb.velocity);

        GameManager.instance.ArmorMeter(currentArmor);
        GameManager.instance.FuelMeter(currentFuel);
        GameManager.instance.DistanceCal(transform.position.z);
        GameManager.instance.SpeedoMeter(currentSpeed);
        //GameManager.instance.MonsterMode(killedZombi);
    }

	void GetInput()
	{

#if UNITY_EDITOR || PLATFORM_WEBGL
        horizontalInput = Input.GetAxisRaw("Horizontal");
#else
		horizontalInput = GameManager.instance.horizontalInput;
#endif
		horizontalInput *= isBreaking ? -1 : 1;		

        transform.rotation = horizontalInput == 0 ? Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, 0, 1), 10 * Time.deltaTime) : transform.rotation;

#if UNITY_EDITOR || PLATFORM_WEBGL
        isBreaking = Input.GetKey(KeyCode.Space) && currentSpeed >= 30;
#else
		isBreaking = GameManager.instance.isBreaking && currentSpeed >= 30;
#endif
    }
    void HandleMotor()
	{
		currentMotorForce = currentSpeed >= maxSpeed ? 0 : motorForce;
		//currentMotorForce = motorForce;

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

	//void OnCollisionStay(Collision other)
	//{
	//	if (other.collider.CompareTag("Obstacle"))
	//	{
	//		Debug.Log(other.collider.name);
	//		Invoke(nameof(ObstacleTrigger), .3f);
 //       }
 //   }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Obstacle"))
		{
			currentArmor = 0;
		}
	}

	void ObstacleTrigger()
	{
		if (currentSpeed <= 25)
		{
			currentArmor = 0;	
		}
		else
		{
			currentArmor -= currentSpeed / 2;
		}
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("RoadTag"))
        {
            RoadController.instance.RoadCreate();
        }

		if (other.CompareTag("Obstacle"))
		{
			Destroy(other.gameObject);
		}
	}

	public IEnumerator	MMM()
	{
		transform.tag = "Monster";
		//GetComponent<BoxCollider>().isTrigger = true;
		rb.velocity = Vector3.zero;
		rb.velocity = new Vector3(0, 0, maxSpeed / 2f);
		maxSpeed *= 2;

		print("basladı");

		yield return new WaitForSeconds(GameManager.instance.properties.monsterDuration);

        print("bitti");

        transform.tag = "Car";
		GameManager.instance.monsterMode = false;
        GetComponent<BoxCollider>().isTrigger = false;
		maxSpeed = GameManager.instance.properties.speed;
		rb.velocity = new Vector3(0, 0, maxSpeed / 3.6f);
		StopCoroutine(nameof(MMM));
    }
	public void MonsterMode()
	{
		transform.tag = "Monster";
	}
}
