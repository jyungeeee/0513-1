using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]    
    private AudioClip swordClip;


    public float attackAmount = 20f;
    AudioSource swordAudio;

    // Start is called before the first frame update
    void Start()
    {
        swordAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            BulletSpawner bulletMonster = other.GetComponent<BulletSpawner>();

            if (bulletMonster != null)
            {
                bulletMonster.Hit(attackAmount);

            }
            swordAudio.PlayOneShot(swordClip);
        }
        else if (other.tag == "Enemy2")
        {
            MonsterCtrl alien = other.GetComponent<MonsterCtrl>();

            if (alien != null)
            {
                alien.getDamage(attackAmount);

            }
            swordAudio.PlayOneShot(swordClip);

        }

    }
}
