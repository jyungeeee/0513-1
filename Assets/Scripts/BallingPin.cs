using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallingPin : MonoBehaviour
{
    private bool isFallDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isFallDown)
        {
            //Debug.Log(this.transform.up);
            //Debug.Log(Vector3.up);
            float innerProduct = Vector3.Dot(this.transform.up, Vector3.up);
            //Debug.Log($"innderProduct: {innerProduct}");

            if (innerProduct != 1)
            {
                isFallDown = true;
                Debug.Log($"{this.gameObject.name} is FallDown");
            }
        }
    }
}
