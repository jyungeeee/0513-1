using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage = 35;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit != null)
        {
            Debug.Log($"Controller HIT {hit.gameObject.name}");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            BulletSpawner bulletSpawner = other.GetComponent<BulletSpawner>();
            if (bulletSpawner != null)
            {
                bulletSpawner.Hit(bulletDamage);
            }
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy2")
        {
            MonsterCtrl alien = other.GetComponent<MonsterCtrl>();
            if (alien != null)
            {
                alien.getDamage(bulletDamage);
            }
            Destroy(gameObject);
        }
        else if (other.tag == "START")
        {
            //Start Game
            FindObjectOfType<GameManager>().StartGame();
        }
    }
}
