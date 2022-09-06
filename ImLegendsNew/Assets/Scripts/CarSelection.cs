using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;

    int currentCarIndex;

    private void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("CurrentCarIndex");
        SelectedCar(currentCarIndex);
    }

    void SelectedCar(int _index)
    {
        previousButton.interactable = _index != 0;
        nextButton.interactable = _index != transform.childCount - 1;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void ChangeCar(int _change)
    {
        currentCarIndex += _change;
        SelectedCar(currentCarIndex);
        PlayerPrefs.SetInt("CurrentCarIndex", currentCarIndex);
    }
}
