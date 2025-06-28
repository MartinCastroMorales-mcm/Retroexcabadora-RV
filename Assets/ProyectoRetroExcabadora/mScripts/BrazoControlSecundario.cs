using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BrazoControlSecundario : MonoBehaviour
{
    public InputActionReference joystickIzquierdoLecturaZ; // Asignar desde el Inspector
    public InputActionReference joystickIzquierdoLecturaY; // Asignar desde el Inspector
    public Transform brazoSegundo;
    public Transform pala;
    public ActionBasedContinuousTurnProvider TurnProvider;


    public float velocidadRotacion = 50f;

    private bool brazoControlSecundario = false;

    public void EmpezarAgarrar()
    {
        brazoControlSecundario = true;
        joystickIzquierdoLecturaZ.action.Enable();
        joystickIzquierdoLecturaY.action.Enable();
        TurnProvider.enabled = false; // Desactiva el volteo
    }

    public void Soltar()
    {
        brazoControlSecundario = false;
        joystickIzquierdoLecturaZ.action.Disable();
        joystickIzquierdoLecturaY.action.Disable();
        TurnProvider.enabled = true; // Activa el volteo
    }

    void Update()
    {
        if (!brazoControlSecundario)
            return;

        Vector2 input = joystickIzquierdoLecturaZ.action.ReadValue<Vector2>();
        Vector2 inputY = joystickIzquierdoLecturaY.action.ReadValue<Vector2>();

        // --- Control para la pala (rotación en eje Y usando input.x) ---
        if (Mathf.Abs(input.x) > 0.01f)
        {
            Vector3 rotacionActual = pala.localEulerAngles;
            float rotY = rotacionActual.y;
            if (rotY > 180f) rotY -= 360f;

            rotY += input.x * velocidadRotacion * Time.deltaTime;
            rotY = Mathf.Clamp(rotY, -40f, 60f); // Ajusta los límites según tu diseño

            pala.localRotation = Quaternion.Euler(0f, rotY, 0f);
        }

        // --- Control para el brazo secundario (rotación en eje Z usando inputY.y) ---
        if (Mathf.Abs(inputY.y) > 0.01f)
        {
            Vector3 rotacionActual = brazoSegundo.localEulerAngles;
            float rotZ = rotacionActual.z;
            if (rotZ > 180f) rotZ -= 360f;

            rotZ += inputY.y * velocidadRotacion * Time.deltaTime;
            rotZ = Mathf.Clamp(rotZ, -10f, 40f); // Ajusta los límites si es necesario

            brazoSegundo.localRotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
}