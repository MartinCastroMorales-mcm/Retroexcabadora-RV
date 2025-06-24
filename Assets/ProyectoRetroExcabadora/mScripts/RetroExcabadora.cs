using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RetroExcabadora : MonoBehaviour
{
    public InputActionProperty rightAction;
    public InputActionProperty leftAction;
    public GameObject player;
    public GameObject targetInVehicle;
    public GameObject playerCamera;
    public GameObject DoorCube;

    public GameObject LeftHand;
    public GameObject RightHand;
    //public CharacterController characterController;
    private bool isInVehicle = false;
    private bool isDoorCubeVisible = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputHandlingForDoor();
    }

    void InputHandlingForDoor()
    {
        //if either hand is in, the block turns yellow and the hand thats inside can "pinch it" to enter
        Bounds cubeBounds = new Bounds(DoorCube.transform.position, DoorCube.transform.localScale);
        Vector3 rightHandPosition = RightHand.transform.position;
        Vector3 leftHandPosition = LeftHand.transform.position;
        if (cubeBounds.Contains(rightHandPosition)) {
            //turn block yellow
            setDoorCubeVisible(true);
            //check if button is pressed
            if (rightAction.action.ReadValue<float>() == 1)
            {
                this.EnterExitVehicle();
            }
            return;
        }
        if (cubeBounds.Contains(leftHandPosition)) {
            //turn block yellow
            setDoorCubeVisible(true);
            //check if button is pressed
            if (leftAction.action.ReadValue<float>() == 1)
            {
                this.EnterExitVehicle();
            }
            return;
        }
        setDoorCubeVisible(false);
    }

    void setDoorCubeVisible(bool willDoorVisible)
    {
        DoorCube.GetComponent<MeshRenderer>().enabled = willDoorVisible;
    }

    void EnterExitVehicle() {
        if (isInVehicle)
        {
            Debug.Log("Exit the vehicle");
        }
        else
        {
            Debug.Log("Enter the vehicle");
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}

//add to press the button of the hand in tthe blob
//-3.847
//374.079
//7.8
//-142.6
