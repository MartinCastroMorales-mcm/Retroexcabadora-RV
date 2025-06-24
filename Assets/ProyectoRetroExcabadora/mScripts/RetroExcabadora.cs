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
    public Transform puntoMira;
    public Transform puntoSalida;
    public float cooldownTime = 1.0f;

    private bool isInVehicle = false;
    private float lastToggleTime = -999f;

    public static bool EnCabina { get; private set; } = false;

    void Update()
    {
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
