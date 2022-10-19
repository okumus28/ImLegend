using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    public static GunController Instance;
    public Gun[] guns;
    public List<Zombie> zombies;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            guns[0].StartFire();
        }
    }
}
