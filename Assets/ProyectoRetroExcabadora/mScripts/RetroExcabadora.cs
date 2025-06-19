using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RetroExcabadora : MonoBehaviour
{
    public InputActionProperty rightAction;
    public GameObject player;
    public GameObject targetInVehicle;
    public GameObject playerCamera;
    //public CharacterController characterController;
    bool isInVehicle = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Solo activar cuando se toque la puerta
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
            //characterController.height = 5;
        }
        else
        {
            Debug.Log("Enter the vehicle");
            //characterController.height = 2;
            //playerCamera.transform.Rotate(7.9f, -142.6f, 0f);
            //playerCamera.transform.rotation = Quaternion.Euler(7.9f, -142.6f, 0f);
            //player.transform.Rotate(-3.847f, 374.079f, 0f, Space.World);
            //playerCamera.transform.Rotate(0f, 0f, 0f, Space.World);
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);

        }
    }
}
//-3.847
//374.079



//7.8
//-142.6
