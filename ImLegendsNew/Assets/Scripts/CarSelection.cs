using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    public static int currentCarIndex;

    public static Car car;

    private void Awake()
    {
        currentCarIndex = PlayerPrefs.GetInt("CurrentCarIndex");
        car = transform.GetChild(currentCarIndex).GetComponent<Car>();
        SelectedCar(currentCarIndex);
        Debug.Log(car.carData.carName);
        car.CarSelectButtonText();
    }

    void SelectedCar(int _index)
    {
        car = transform.GetChild(_index).GetComponent<Car>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        Debug.Log(transform.GetChild(currentCarIndex).GetComponent<Car>().purchase);

        if (previousButton == null)
            return;

        if (transform.GetChild(currentCarIndex).GetComponent<Car>().purchase)
        {            
            PlayerPrefs.SetInt("CurrentCarIndex" , currentCarIndex);
        }

        previousButton.interactable = _index != 0;
        nextButton.interactable = _index != transform.childCount - 1;

    }

    public void ChangeCar(int _change)
    {
        transform.GetChild(currentCarIndex).gameObject.SetActive(false);
        currentCarIndex += _change;
        SelectedCar(currentCarIndex);

        //PlayerPrefs.SetInt("CurrentCarIndex", currentCarIndex);

        //car = transform.GetChild(currentCarIndex).GetComponent<Car>();

    }
}
