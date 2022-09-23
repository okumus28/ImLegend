using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] float fuel;
    [SerializeField] float armor;

    CarController currentCar;

    private void Update()
    {
        transform.Rotate(0,2,0 , Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            currentCar = other.GetComponent<CarController>();

            currentCar.currentFuel += fuel;
            if(currentCar.currentFuel > currentCar.maxFuel)
                currentCar.currentFuel = currentCar.maxFuel;

            currentCar.currentArmor += armor;
            if (currentCar.currentArmor > currentCar.maxArmor)
                currentCar.currentArmor = currentCar.maxArmor;

            GetComponent<AudioSource>().Play();

            Destroy(gameObject , 1f);
        }
    }

}
