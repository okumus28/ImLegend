using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] float blood;
    [SerializeField] float damage;
    Vector3 target;
    public BoxCollider boundsCollider;

    [SerializeField] private float offsetY;
    [SerializeField] private float speed;

    [SerializeField] AudioClip zombiExplode;

    // Start is called before the first frame update
    void Start()
    {
        target = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position , target) <= 2f)
        {
            target = RandomPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            AudioController.instance.PlayAudio(zombiExplode);
            GameManager.instance.KilledZombi(1);
            GameManager.instance.MonsterModeMeter(Random.Range(blood - 2, blood + 5));
            damage -= damage*other.GetComponent<Car>().carData.properties.zombieResist / 100;
            other.GetComponent<CarController>().currentArmor -= Random.Range(damage - 3, damage + 3);
            if (other.GetComponent<CarController>().currentSpeed >= 50)
            {
                GameManager.instance.comboT = 0;
                ComboStart();
            }
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Monster"))
        {
            AudioController.instance.PlayAudio(zombiExplode);
            GameManager.instance.KilledZombi(1);
            Destroy(gameObject);
        }
    }

    Vector3 RandomPosition()
    {
        Bounds bounds = boundsCollider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        
        return new Vector3(x , offsetY , z);
    }

    void ComboStart()
    {
        GameManager.instance.comboValue++;
        GameManager.instance.Combo();
    }
}
