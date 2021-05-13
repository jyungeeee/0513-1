using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6f;
    private Rigidbody bulletRigidbody;

    [SerializeField]
    private float damage = 50;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playercontroller = other.GetComponent<PlayerController>();

            if (playercontroller != null)
            {
                playercontroller.GetDamage(damage);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
