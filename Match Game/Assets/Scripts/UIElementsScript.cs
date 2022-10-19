using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsScript : MonoBehaviour
{
    public static UIElementsScript Instance;

    public bool specialItems;
    [SerializeField] Toggle specialItemsToggle;

    [SerializeField] TextMeshProUGUI columnCount;
    [SerializeField] TextMeshProUGUI rowCount;
    [SerializeField] TextMeshProUGUI colorCount;
    [SerializeField] TextMeshProUGUI groupA;
    [SerializeField] TextMeshProUGUI groupB;
    [SerializeField] TextMeshProUGUI groupC;    
    
    [SerializeField] Slider columnCountSlider;
    [SerializeField] Slider rowCountSlider;
    [SerializeField] Slider colorCountSlider;
    [SerializeField] Slider groupASlider;
    [SerializeField] Slider groupBSlider;
    [SerializeField] Slider groupCSlider;
    private void Start()
    {
        columnCountSlider.value = LevelManager.Instance.columnCount;
        rowCountSlider.value = LevelManager.Instance.rowCount;
        colorCountSlider.value = LevelManager.Instance.colorsCount;
        groupCSlider.value = LevelManager.Instance.c;
        groupBSlider.value = LevelManager.Instance.b;
        groupASlider.value = LevelManager.Instance.a;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void CreateSpecialItems()
    {
        specialItems = specialItemsToggle.isOn;
    }

    public void ColumnCountText()
    {
        columnCount.text = columnCountSlider.value.ToString();
        LevelManager.Instance.columnCount = (int)columnCountSlider.value;
    }
    public void RowCountText()
    {
        rowCount.text = rowCountSlider.value.ToString();
        LevelManager.Instance.rowCount = (int)rowCountSlider.value;

    }
    public void ColorCountText()
    {
        colorCount.text = colorCountSlider.value.ToString();
        LevelManager.Instance.colorsCount = (int)colorCountSlider.value;

    }
    public void GroupAText()
    {
        groupA.text = groupASlider.value.ToString();
        ValueControl();
        LevelManager.Instance.a = (int)groupASlider.value;

    }
    public void GroupBText()
    {
        groupB.text = groupBSlider.value.ToString();
        ValueControl();
        LevelManager.Instance.b = (int)groupBSlider.value;

    }
    public void GroupCText()
    {
        groupC.text = groupCSlider.value.ToString();
        ValueControl();
        LevelManager.Instance.c = (int)groupCSlider.value;

    }

    void ValueControl()
    {
        groupASlider.minValue = 0;
        groupASlider.maxValue = groupBSlider.value - 1;
        groupBSlider.minValue = groupASlider.value + 1;
        groupBSlider.maxValue = groupCSlider.value - 1;
        groupCSlider.minValue = groupBSlider.value + 1;
        groupCSlider.maxValue = 20;
    }

    public void PlayButton()
    {
        GameObject.Find("MainMenu").SetActive(false);
        GameManager.Instance.SpawnBlocks();
    }

    public void MixerButton()
    {
        GameManager.Instance.MixBlocks();
    }

}
