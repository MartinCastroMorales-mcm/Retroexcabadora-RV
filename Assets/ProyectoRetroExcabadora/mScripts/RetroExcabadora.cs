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
    public Transform puntoMira;
    public Transform puntoSalida;
    public float cooldownTime = 1.0f;

    private bool isInVehicle = false;
    private float lastToggleTime = -999f;

    public static bool EnCabina { get; private set; } = false;

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
        if (rightAction.action.ReadValue<float>() == 1 && Time.time - lastToggleTime > cooldownTime)
        {
            lastToggleTime = Time.time;
            EnterExitVehicle();
        }
    }

    void EnterExitVehicle()
    {
        if (isInVehicle)
        {
            // Salir de la cabina
            player.transform.position = puntoSalida.transform.position;
            EnCabina = false;
            isInVehicle = false;
        }
        else
        {
            Debug.Log("Enter the vehicle");
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);
            // Entrar a la cabina
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);

            Vector3 direccionObjetivo = puntoMira.forward;
            direccionObjetivo.y = 0;

            Vector3 direccionCamara = playerCamera.transform.forward;
            direccionCamara.y = 0;

            Quaternion rotacionActual = Quaternion.LookRotation(direccionCamara);
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccionObjetivo);
            Quaternion ajusteRotacion = rotacionDeseada * Quaternion.Inverse(rotacionActual);

            player.transform.rotation = ajusteRotacion * player.transform.rotation;

            EnCabina = true;
            isInVehicle = true;
        }
    }
}

//add to press the button of the hand in tthe blob
//-3.847
//374.079
//7.8
//-142.6
