using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandState { NONE = 0, RIGHT, LEFT };

public class CustomController : MonoBehaviour
{
    public InputDeviceCharacteristics characteristics;
    
    [SerializeField]
    private List<GameObject> controllerModels;
    private GameObject controllerInstance;
    private InputDevice availableDevice;

    public bool renderController; // variable that Changes between Hand and Controller
    public GameObject handModel;  // prefab of Hand
    private GameObject handInstance; // Instance of Hand
    private Animator handModelAnimator; // Animator of Hand

    public GameObject handGun; // handGun

    private bool triggerButton;

    public HandState currentHand;
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        // used for get right controller
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        foreach (var device in devices)
        {
            Debug.Log($"Available Device Name: {device.name}, Characteristics: {device.characteristics}");
            //Debug.Log(devices.Count);

        }
        if (devices.Count > 0)
        {
            availableDevice = devices[0];

            GameObject currentControllerModel;

            if (availableDevice.name.Contains("Left"))
            {
                currentControllerModel = controllerModels[1];
                currentHand = HandState.LEFT;
            }
            else if (availableDevice.name.Contains("Right"))
            {
                currentControllerModel = controllerModels[2];
                currentHand = HandState.RIGHT;
            }
            else
            {
                currentControllerModel = null;
                currentHand = HandState.NONE;
            }

            //string name = "";
            //if ("Oculus Touch Controller - Left" == availableDevice.name)
            //{   //Oculus Touch Controller - Left
            //    name = "Oculus Quest Controller - Left";
            //}
            //else if ("Oculus Touch Controller - Right" == availableDevice.name)
            //{   //Oculus Touch Controller - Right
            //    name = "Oculus Quest Controller - Right";
            //}
            //GameObject currentControllerModel = controllerModels.Find(controller => controller.name == name);
            if (currentControllerModel)
            {
                controllerInstance = Instantiate(currentControllerModel, transform);
            }
            else
            {
                Debug.LogError("Didn't get suitable controller model");
                controllerInstance = Instantiate(controllerModels[0], transform);
            }

            //Debug.Log($"HandModel: {handModel}");
            //Debug.Log($"HandInstance: {handInstance}");
            handInstance = Instantiate(handModel, transform); // Add handInstance
            //Debug.Log($"After HandInstantiate: {handInstance}");

            handModelAnimator = handInstance.GetComponent<Animator>(); // Set handModelAnimator

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!availableDevice.isValid)
        {
            TryInitialize();
            return;
        }

        if (renderController)
        {
            handInstance.SetActive(false);
            controllerInstance.SetActive(true);
        }
        else
        {
            handInstance.SetActive(true);
            controllerInstance.SetActive(false);
            UpdateHandAnimationethod();
        }

        if (handGun != null)
        {
            bool menuButtonValue;
            if (availableDevice.TryGetFeatureValue(CommonUsages.triggerButton, out menuButtonValue) && menuButtonValue)
            {
                if (triggerButton == false && currentHand == handGun.GetComponent<SimpleShootCustom>().currentGrab)
                {
                    handGun.GetComponent<SimpleShootCustom>().Shoot();
                    triggerButton = true;
                }

            }
            else
            {
                triggerButton = false;
            }
        }

        if (FindObjectOfType<GameManager>().isGameOver)
        {
            bool menuButtonValue;
            if (availableDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonValue) && menuButtonValue)
            {
                FindObjectOfType<GameManager>().RestartGame();
            }
        }

    }

    private void UpdateHandAnimationethod()
    {
        if (availableDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handModelAnimator.SetFloat("Trigger", triggerValue);

        }
        else
        {
            handModelAnimator.SetFloat("Trigger", 0);

        }
        if (availableDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handModelAnimator.SetFloat("Grip", gripValue);

        }
        else
        {
            handModelAnimator.SetFloat("Grip", 0);

        }
    }
}
