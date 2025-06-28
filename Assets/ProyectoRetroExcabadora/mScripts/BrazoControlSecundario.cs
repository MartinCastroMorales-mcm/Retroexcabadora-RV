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
        TurnProvider.enabled = false; // Desactiva el volteo
    }

    public void Soltar()
    {
        brazoControlSecundario = false;
        joystickIzquierdoLecturaZ.action.Disable();
        TurnProvider.enabled = true; // Activa el volteo
    }

    void Update()
    {
        if (!brazoControlSecundario)
            return;

        Vector2 input = joystickIzquierdoLecturaZ.action.ReadValue<Vector2>();
        Vector2 inputY = joystickIzquierdoLecturaY.action.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) > 0.01f) // Tolerancia para evitar ruido mínimo
        {
            // Rotación sobre eje Y (Vector3.up) usando la entrada X del joystick
            pala.Rotate(0f, input.x * velocidadRotacion * Time.deltaTime, 0f, Space.Self);
        }

        if (Mathf.Abs(inputY.y) > 0.01f) // Tolerancia para evitar ruido mínimo
        {
            // Rotación sobre eje Y (Vector3.up) usando la entrada X del joystick
            brazoSegundo.Rotate(0f, inputY.y * velocidadRotacion * Time.deltaTime, 0f, Space.Self);
        }
    }
}