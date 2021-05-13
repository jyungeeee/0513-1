using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Renderer circleColor;
    // Start is called before the first frame update
    void Start()
    {
        circleColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider collider)
    {


        if (collider.gameObject.tag == "Player")

        {
            circleColor.material.SetColor("_Color", Color.red);
            Debug.Log("Check");
        }
    }
    
}
