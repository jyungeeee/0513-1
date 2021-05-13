using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : MonoBehaviour
{
    [SerializeField]
    public float speed = 3.0f; //movement speed
    public float gravityMultiplier = 1.0f; // used when affected by gravity
    [SerializeField]
    public List<XRController> controllers = null; // list of controllers
    private CharacterController characterController = null; // characterController of VR Rig
    private GameObject head = null; // head of camera

    bool isStarted = false; // whether started or not
    private void Awake()
    {
        // assign CharacterController and set Camera position
        characterController = GetComponent<CharacterController>();
        //characterController.isTrigger = true;
        head = GetComponent<XRRig>().cameraGameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // manage initial settings
        PositionController();

    }


    // Update is called once per frame
    void Update()
    {
        PositionController(); //set postion
        Move();
        CheckForInput();
        ApplyGravity();

    }

    private void PositionController()
    {
        // forsmooth moving y-pos of head decided 1 ~ 2
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        // get new center position
        Vector3 newCenter = Vector3.zero;
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        characterController.center = newCenter;
    }

    private void CheckForInput()
    {
        // if there is input from set controller, do moving
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
            {
                CheckForMovement(controller.inputDevice);
            }
        }
    }

    private void CheckForMovement(InputDevice device)
    {
        // lever can be readed by primary2DAxis
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            StartMove(position);

    }

    private void StartMove(Vector2 position)
    {
        // set direction for moving
        Vector3 direction = new Vector3(position.x, 0, position.y); // y-axis doesn't affect
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0); // apply head rotation

        direction = Quaternion.Euler(headRotation) * direction;

        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime); // must use deltaTime
    }

    private void ApplyGravity()
    {
        // apply gravity to downward
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
       // print(gravity);
        gravity.y = Time.deltaTime;

        characterController.Move(gravity * Time.deltaTime);

        //print("d");
    }

    public void StartMove()
    {
        isStarted = true;
    }
    private void Move()
    {
        if (!isStarted)
            return;

        //setting direction
        Vector3 direction = new Vector3(0, 0, 1);
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);

    }
}
