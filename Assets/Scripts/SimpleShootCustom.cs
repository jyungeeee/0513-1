using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleShootCustom : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;

    [SerializeField]
    public float shotPower = 500f;

    public bool isGrab = false;

    public AudioClip fireClip; // soundClip of gun shoot
    AudioSource fireAudio; // variable for AudioSource Component

    public HandState currentGrab; // shows which hand grabs the handgun
    // Start is called before the first frame update
    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        fireAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    GetComponent<Animator>().SetTrigger("Fire1");
        //}
    }
    public void Shoot()
    {
        if (isGrab == true)
        {
            GameObject tempFlash;
            Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            fireAudio.PlayOneShot(fireClip);


            //CasingRelease();

        }

    }

    void CasingRelease()
    {
        GameObject casing;
        casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
    }

    public void grabGun()
    {
        isGrab = true;
    }
    public void dropGun()
    {
        isGrab = false;
    }

    public void SetGraspState(HandState state)
    {
        currentGrab = state;
    }
    public void SetGraspNONE()
    {
        if (!GetComponent<XRGrabInteractable>().isSelected)
            currentGrab = HandState.NONE;
    }
    public void SetGraspLEFT()
    {
        currentGrab = HandState.LEFT;
    }
    public void SetGraspRIGHT()
    {
        currentGrab = HandState.RIGHT;
    }

}
