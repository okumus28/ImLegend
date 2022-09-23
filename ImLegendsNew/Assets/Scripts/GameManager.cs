using System.Collections;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] Image monsterMeter3;
    [SerializeField] Button monsterModeStartButton;

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
    public bool monsterMode;

    private float timeElapsed;
    private float lerpDuration;

    public int horizontalInput;
    public bool isBreaking;

    public GameObject comboPanel;
    public TextMeshProUGUI comboValueText;
    public Image comboTimeFillBar;

    public TextMeshProUGUI comboEndText;
    public float comboT;
    public int comboValue;
    bool comboControl;

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
        MonsterModeMeter(0);        
    }

    private void Update()
    {
        if (monsterMode)
        {
            timeElapsed += Time.deltaTime / lerpDuration;
            blood = Mathf.Lerp(99, 0, timeElapsed);

            //if (timeElapsed < lerpDuration)
            //{
            //    Debug.Log(timeElapsed / lerpDuration);
            //    blood = Mathf.Lerp(blood, 0, timeElapsed / lerpDuration);

            monsterMeter.fillAmount = (float)blood / 100;
            monsterMeter2.fillAmount = (float)blood / 100;
            monsterMeter3.fillAmount = (float)blood / 100;

            //    timeElapsed += Time.deltaTime;
            //    //timeElapsed /= 1.3f;
            //}
        }

        if (comboPanel.activeSelf)
        {
            if (comboT <= properties.comboDuration * 2)
            {
                comboValueText.text = (comboValue).ToString();
                comboTimeFillBar.fillAmount = (properties.comboDuration * 2 - comboT) / properties.comboDuration * 2;
                comboT += Time.deltaTime / 2;
                
            }
            else
            {
                Point(comboValue * 100);
                comboEndText.text = "+" + (comboValue * comboValue * 100).ToString();
                comboEndText.gameObject.SetActive(true);
                comboPanel.SetActive(false);
                comboValue = 0;
                comboT = 0;
                Invoke(nameof(InvokeSetActiveEndText), 2);
            }
        }
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

        fuelCursor.localEulerAngles = new Vector3(0, 0, 110 - ((200 - currentFuel) * 1.275f));
        fuelImage.color = currentFuel < 200 / 5 ? currentFuel < 200 / 10 ? Color.red : Color.yellow : Color.black;

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

    public void MonsterModeMeter(float b)
    {
        this.blood += b;

        monsterModeStartButton.GetComponent<Button>().enabled = blood >= 100;

        monsterMeter.fillAmount = (float)blood / 100;
        monsterMeter2.fillAmount = (float)blood / 100;
        monsterMeter3.fillAmount = (float)blood / 100;

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

        int c = (int)(point * 0.25f) + (killedZombi * 15) + (int)distance / 10;

        cash += c;
        cashText.text = c.ToString();
        PlayerPrefs.SetInt("PlayerCash" , cash);
        scoreText.text = point.ToString();

        Time.timeScale = 0;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    public void SettingsButton()
    {
        Debug.Log("SETTÝNGS ON");
    }

    public void MonsterModeSTartButton()
    {
        if (blood >= 100)
        {
            timeElapsed = 0;
            lerpDuration = properties.monsterDuration;
            monsterMode = true;
            blood = 99;
            currentCar.GetComponent<CarController>().StartCoroutine("MMM");
            monsterModeStartButton.GetComponent<Button>().enabled = false;
        }
    }

    public void SetHorizontalInput(int axis)
    {
        horizontalInput = axis;
    }

    public void SetBreakingInput(bool isBreaking)
    {
        this.isBreaking = isBreaking;
    }

    public void Combo()
    {
        //comboControl = true;
        if (comboT <= properties.comboDuration)
        {
            comboPanel.SetActive(true);
            comboEndText.gameObject.SetActive(false);
            comboValueText.text = comboValue.ToString();
            //comboTimeFillBar.fillAmount = comboT / properties.comboDuration;
            //comboT += Time.deltaTime;
        }
    }

    void InvokeSetActiveEndText()
    {
        comboEndText.gameObject.SetActive(false);
    }
}
