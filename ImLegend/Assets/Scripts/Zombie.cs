using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] int healt;
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, 0, target.z), speed);
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
                ComboStart();
            }
            Destroy(gameObject);
        }
        
        if (other.CompareTag("Monster"))
        {
            AudioController.instance.PlayAudio(zombiExplode);
            GameManager.instance.KilledZombi(1);
            if (other.GetComponent<CarController>().currentSpeed >= 50)
            {
                ComboStart();
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("GunFireArea"))
        {
            GunController.Instance.zombies.Add(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GunFireArea"))
        {
            GunController.Instance.zombies.Remove(this);
        }
    }

    private void OnDestroy()
    {
        if (GunController.Instance.zombies.Contains(this))
        {
            GunController.Instance.zombies.Remove(this);
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
        GameManager.instance.comboT = 0;
        GameManager.instance.comboValue++;
        GameManager.instance.Combo();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(healt);
        healt -= damage;
        if (healt <= 0)
        {
            AudioController.instance.PlayAudio(zombiExplode);
            GameManager.instance.KilledZombi(1);
            ComboStart();
            Destroy(this.gameObject);
        }
    }
}
