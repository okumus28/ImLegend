using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public int damage;

    private void Start()
    {
        Destroy(this.gameObject, 2f);
    }
    private void Update()
    {
        if (target == null) Destroy(this.gameObject);
        else
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x , target.position.y + 2.25f, target.position.z), 200 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("HitZombie");
            other.GetComponent<Zombie>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
