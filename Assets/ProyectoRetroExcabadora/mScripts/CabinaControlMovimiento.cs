using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CabinaControlMovimiento : MonoBehaviour
{
    public Transform jugadorParaRotar; // normalmente el XR Origin
    public InputActionReference joystickIzquierdo; // Vector2
    public float velocidadRotacion = 45f;

    void Update()
    {
        if (RetroExcabadora.EnCabina)
        {
            Vector2 entrada = joystickIzquierdo.action.ReadValue<Vector2>();

            // Solo tomamos el eje X para rotar horizontalmente
            float rotacion = entrada.x * velocidadRotacion * Time.deltaTime;

            jugadorParaRotar.Rotate(0, rotacion, 0);
        }
    }
}

