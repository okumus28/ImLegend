using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Property : MonoBehaviour
{
    [Header("Value Texts")]
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI armorText;
    [SerializeField] TextMeshProUGUI fuelTankText;
    [SerializeField] TextMeshProUGUI zombieResistText;
    [SerializeField] TextMeshProUGUI monsterDurationText;
    [SerializeField] TextMeshProUGUI comboDurationText;

    [Header("Fill Bars")]
    [SerializeField] Image speedFillBar;
    [SerializeField] Image armorFillBar;
    [SerializeField] Image fuelTankFillBar;
    [SerializeField] Image zombieResistFillBar;
    [SerializeField] Image monsterDurationFillBar;
    [SerializeField] Image comboDurationFillBar;

    public static UI_Property Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UI_PropertiesUpdate(Properties _prop)
    {
        speedText.text = _prop.speed.ToString();
        armorText.text = _prop.armor.ToString();
        fuelTankText.text = _prop.fuelTank.ToString();
        zombieResistText.text = _prop.zombieResist.ToString();
        monsterDurationText.text = _prop.monsterDuration.ToString();
        comboDurationText.text = _prop.comboDuration.ToString();

        speedFillBar.fillAmount = _prop.speed / 300;
        armorFillBar.fillAmount = _prop.armor / 300;
        fuelTankFillBar.fillAmount = _prop.fuelTank / 200;
        zombieResistFillBar.fillAmount = _prop.zombieResist / 100;
        monsterDurationFillBar.fillAmount = _prop.monsterDuration / 10;
        comboDurationFillBar.fillAmount = _prop.comboDuration / 10;
    }

}
