using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoyIzqDetect : MonoBehaviour
{
    public InputActionReference joystickIzquierdoLectura;

    void OnEnable()
    {
        // Activamos el input igual que lo hace Unity internamente
        joystickIzquierdoLectura?.action?.Enable(); // (alternativamente puedes usar EnableDirectAction() si accedes como InputActionProperty)

        Debug.Log("Joystick izquierdo ACTIVADO manualmente");
    }

    void OnDisable()
    {
        joystickIzquierdoLectura?.action?.Disable();
    }

    void Update()
    {
        if (!RetroExcabadora.EnCabina) return;

        Vector2 input = joystickIzquierdoLectura.action.ReadValue<Vector2>();
        Debug.Log($"[JOY TEST] Valor leído: {input}");

        if (input != Vector2.zero)
        {
            Debug.Log($"[JOY DETECTADO] Movimiento en cabina: {input}");
        }
    }
}
