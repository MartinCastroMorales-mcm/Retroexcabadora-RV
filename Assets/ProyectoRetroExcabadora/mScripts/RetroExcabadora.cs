using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RetroExcabadora : MonoBehaviour
{
    public InputActionProperty rightAction;
    public GameObject player;
    public GameObject targetInVehicle;
    bool isInVehicle = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rightAction.action.ReadValue<float>());
        if (rightAction.action.ReadValue<float>() == 1)
        {
            this.EnterExitVehicle();
            Debug.Log("Hello World");
        }

    }
    void EnterExitVehicle() {
        if (isInVehicle)
        {
            Debug.Log("Exit the vehicle");
        }
        else
        {
            Debug.Log("Enter the vehicle");
            player.transform.position = targetInVehicle.transform.position;

        }
    }
}
