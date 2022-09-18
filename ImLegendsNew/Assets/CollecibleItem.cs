using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecibleItem : MonoBehaviour
{
    [SerializeField] float fuel;
    [SerializeField] float armor;

    private void Update()
    {
        transform.Rotate(0,2,0 , Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            other.GetComponent<RearWheelDrive>().currentFuel += fuel;
            other.GetComponent<RearWheelDrive>().currentArmor += armor;

            Destroy(gameObject);
        }
    }

}
