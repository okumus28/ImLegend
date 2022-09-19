using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform carHolder;
    [SerializeField] Car currentCar;

    public  Properties properties;

    [Header("Cursors")]
    public Transform speedCursor;
    public TextMeshProUGUI speedText;

    public Transform fuelCursor;
    [SerializeField] Image fuelImage;
    public Transform armorCursor;
    [SerializeField] Image armorImage;

    [Header("Distance & Zombi & Point")]
    public TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI killedZombiText;
    [SerializeField] TextMeshProUGUI pointText;


    [SerializeField] Image monsterMeter;
    [SerializeField] Image monsterMeter2;

    public int killedZombi;
    public float blood;
    public float distance;

    public int point;

    public int cash;

    [Header("GameOverPanel")]
    public GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI cashText;
    [SerializeField] TextMeshProUGUI defaultCashText;

    public bool gameOver;

    private void Awake()
    {
        instance = this;
        currentCar = carHolder.GetChild(PlayerPrefs.GetInt("CurrentCarIndex")).GetComponent<Car>();
        cash = PlayerPrefs.GetInt("PlayerCash");
    }

    private void Start()
    {
        properties = currentCar.properties;
        KilledZombi(0);
        Point(0);
    }

    public void SpeedoMeter(float currentSpeed)
    {
        speedCursor.localEulerAngles = new Vector3(0, 0, 270 - (currentSpeed * 0.9f));
        speedText.text = currentSpeed.ToString("f0");
    }
    public void FuelMeter(float currentFuel)
    {
        if (gameOver)
            return;
        currentFuel = currentFuel <= 0 ? 0 : currentFuel;

        fuelCursor.localEulerAngles = new Vector3(0, 0, 110 - ((100 - currentFuel) * 2.45f));
        fuelImage.color = currentFuel < 100 / 5 ? currentFuel < 100 / 10 ? Color.red : Color.yellow : Color.black;

        gameOver = currentFuel <= 0;
        GameOver();

    }
    public void ArmorMeter(float currentArmor)
    {
        if (gameOver)
            return;
        currentArmor = currentArmor <= 0 ? 0 : currentArmor;

        armorCursor.localEulerAngles = new Vector3(0, 0, 70 + (300 - currentArmor) * .817f);
        armorImage.color = currentArmor <= 300 / 5 ? currentArmor <= 300 / 10 ? Color.red : Color.yellow : Color.black;

        gameOver = currentArmor <= 0;
        GameOver();
        
    }

    public void DistanceCal(float zPosition)
    {
        Point((zPosition - distance) < 0.5f ? 0 : 1);

        distance = zPosition;

        distanceText.text = (distance / 1000).ToString("f2") + " km";
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
        Point(5);
    }

    public void Point(int earned)
    {
        point += earned;
        pointText.text = point.ToString();
    }

    public void GameOver()
    {
        if (!gameOver)
            return;

        print("game over");
        gameOverPanel.SetActive(true);
        defaultCashText.text = cash.ToString();
        int c = (int)(point * 0.25f);
        cash += c;
        cashText.text = c.ToString();
        PlayerPrefs.SetInt("PlayerCash" , cash);
        scoreText.text = point.ToString();
        Time.timeScale = 0;
    }
}
