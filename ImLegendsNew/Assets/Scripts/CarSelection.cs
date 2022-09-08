using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    int currentCarIndex;

    public static Car car;

    private void Awake()
    {
        currentCarIndex = PlayerPrefs.GetInt("CurrentCarIndex");
        SelectedCar(currentCarIndex);
    }

    void SelectedCar(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        car = transform.GetChild(_index).GetComponent<Car>();

        if (previousButton == null)
            return;

        previousButton.interactable = _index != 0;
        nextButton.interactable = _index != transform.childCount - 1;

    }

    public void ChangeCar(int _change)
    {
        currentCarIndex += _change;
        SelectedCar(currentCarIndex);
        PlayerPrefs.SetInt("CurrentCarIndex", currentCarIndex);
    }
}
