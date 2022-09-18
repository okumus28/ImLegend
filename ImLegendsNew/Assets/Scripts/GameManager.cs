using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform carHolder;
    [SerializeField] Car currentCar;

    public  Properties properties;

    public Transform speedCursor;
    public TextMeshProUGUI speedText;

    public Transform fuelCursor;
    public Transform armorCursor;

    public TextMeshProUGUI distanceText;

    [SerializeField] TextMeshProUGUI killedZombiText;
    [SerializeField] Image monsterMeter;
    [SerializeField] Image monsterMeter2;

    public int killedZombi;
    public float blood;


    private void Awake()
    {
        instance = this;
        currentCar = carHolder.GetChild(PlayerPrefs.GetInt("CurrentCarIndex")).GetComponent<Car>();
    }

    private void Start()
    {
        properties = currentCar.properties;
        KilledZombi(0);
    }

    public void SpeedoMeter(float currentSpeed)
    {
        speedCursor.localEulerAngles = new Vector3(0, 0, 270 - (currentSpeed * 0.9f));
        speedText.text = currentSpeed.ToString("f0");
    }
    public void FuelMeter(float currentFuel)
    {
        fuelCursor.localEulerAngles = new Vector3(0, 0, 105 - ((100 - currentFuel) * 2.3f));
    }
    public void ArmorMeter(float currentArmor)
    {
        armorCursor.localEulerAngles = new Vector3(0, 0, 75 + (300 - currentArmor) * .75f);
    }

    public void DistanceCal(float zPosition)
    {
        distanceText.text = (zPosition / 1000).ToString("f2") + " km";
    }
    public void MonsterMode(float b)
    {
        this.blood += b;
        monsterMeter.fillAmount = (float)blood / 100;
        monsterMeter2.fillAmount = (float)blood / 100;
    }

    public void KilledZombi(int k)
    {
        killedZombi += k;
        killedZombiText.text = killedZombi.ToString();
    }
}
