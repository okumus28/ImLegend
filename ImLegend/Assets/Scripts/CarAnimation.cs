using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    [SerializeField] Vector3 finalPosition;
    Vector3 initialPosition;
    Vector3 initialRotation;

    [SerializeField] float animationSpeed;
    [SerializeField] float turnSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPosition, animationSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position , finalPosition) <= 1f)
        {
            transform.localEulerAngles += new Vector3(0, turnSpeed * Time.deltaTime, 0);
        }
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
        transform.localEulerAngles = initialRotation;
    }
}
