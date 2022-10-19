using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireSpeed;
    [SerializeField] int capacity;
    [SerializeField] bool canShoot = true;

    [SerializeField]Transform target;
    private void Update()
    {
        Gettarget();
    }

    void Gettarget()
    {
        if (target == null && GunController.Instance.zombies.Count > 0)
        {
            int rnd = Random.Range(0, GunController.Instance.zombies.Count);
            target = GunController.Instance.zombies[rnd].transform;
        }

        if ( target != null && !GunController.Instance.zombies.Contains(target.GetComponent<Zombie>()))
        {
            target = null;
        }

        if (target != null)
        {
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }
    }

    public void StartFire()
    {
        if (target !=null)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        if (canShoot && capacity > 0)
        {
            canShoot = false;
            capacity--;
            Bullet bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.target = target;
            yield return new WaitForSeconds(1 / fireSpeed);
            canShoot = true;
            //Debug.Log("shoot");

            StopCoroutine(Fire());
        }
    }
}
