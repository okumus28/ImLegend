using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Property : MonoBehaviour
{
    [Header("Value Texts")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI fuelTankText;
    public TextMeshProUGUI zombieResistText;
    public TextMeshProUGUI monsterDurationText;
    public TextMeshProUGUI comboDurationText;

    [Header("Fill Bars")]
    public Image speedFillBar;
    public Image armorFillBar;
    public Image fuelTankFillBar;
    public Image zombieResistFillBar;
    public Image monsterDurationFillBar;
    public Image comboDurationFillBar;

    public static UI_Property instance { get; private set; }

    private void Awake()
    {
        instance = this;
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
