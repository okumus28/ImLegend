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

    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;

    [Header("Vehicle")]
	[SerializeField] private float motorForce;
	private float currentMotorForce;
	[SerializeField] private float breakForce;
	private float currentBreakForce;
	[SerializeField] private float maxSteerAngle;

	[Header("Properties")]

	public float maxSpeed;
	public float currentSpeed;

	[System.NonSerialized] public float maxArmor;
	public float currentArmor;

    [System.NonSerialized] public float maxFuel;
	public float currentFuel;

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
		//SteeringAngle();
		LeftRightMove();
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
		//horizontalInput *= isBreaking ? -1 : 1;		

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

		frontLeftTransform.Rotate(currentSpeed * 100 * Time.deltaTime, frontLeftTransform.localEulerAngles.y, 0, Space.Self);
		frontRightTransform.Rotate(currentSpeed * 100 * Time.deltaTime , frontRightTransform.localEulerAngles.y, 0 , Space.Self);
		backLeftTransform.Rotate(currentSpeed * 100 * Time.deltaTime , 0 , 0 , Space.Self);
		backRightTransform.Rotate(currentSpeed * 100 * Time.deltaTime , 0 , 0 , Space.Self);

		backRightCollider.motorTorque = currentMotorForce;
		backLeftCollider.motorTorque = currentMotorForce;
	}
	void ApplyBrake()
	{
		//currentBreakForce = isBreaking ? breakForce : 0;

		if (isBreaking)
		{
			rb.velocity *= .975f;
		}

		//frontLeftCollider.brakeTorque = currentBreakForce;
		//frontRightCollider.brakeTorque = currentBreakForce;
		//backLeftCollider.brakeTorque = currentBreakForce;
		//backRightCollider.brakeTorque = currentBreakForce;
	}
	void SteeringAngle()
	{
		float currentAngle = horizontalInput * maxSteerAngle;

		frontLeftCollider.steerAngle = currentAngle;
		frontRightCollider.steerAngle = currentAngle;
	}
	void LeftRightMove()
	{
		transform.Rotate(Vector3.up * horizontalInput * maxSteerAngle * Time.deltaTime);

  //      frontLeftTransform.Rotate(Vector3.up * horizontalInput * maxSteerAngle * Time.deltaTime);
		//frontRightTransform.Rotate(Vector3.up * horizontalInput * maxSteerAngle * Time.deltaTime);

		Vector3 playerEulerAngles = transform.rotation.eulerAngles;

		playerEulerAngles.y = (playerEulerAngles.y > 180) ? playerEulerAngles.y - 360 : playerEulerAngles.y;
		playerEulerAngles.y = Mathf.Clamp(playerEulerAngles.y , -30 , 30);

		transform.rotation = Quaternion.Euler(playerEulerAngles);

		frontLeftTransform.localEulerAngles = new Vector3(frontLeftTransform.localEulerAngles.x,Input.GetAxis("Horizontal") * maxSteerAngle,0);
		frontRightTransform.localEulerAngles = new Vector3(frontRightTransform.localEulerAngles.x, Input.GetAxis("Horizontal") * maxSteerAngle,0);
	}
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

		yield return new WaitForSeconds(GameManager.instance.properties.monsterDuration);

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
