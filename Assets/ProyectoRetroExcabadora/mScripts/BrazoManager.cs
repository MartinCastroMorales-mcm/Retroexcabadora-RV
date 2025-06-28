using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrazoControl : MonoBehaviour
{
    public InputActionReference joystickIzquierdoLecturaZ; // Asignar desde el Inspector
    public InputActionReference joystickIzquierdoLecturaY; // Asignar desde el Inspector
    public Transform brazoBase;
    public Transform brazoSegundo;
    public float velocidadRotacion = 50f;

    private bool agarrandoPalanca = false;

    public void EmpezarAgarrar()
    {
        agarrandoPalanca = true;
        joystickIzquierdoLecturaZ.action.Enable();
        Debug.Log("Palanca agarrada");
    }

    public void Soltar()
    {
        agarrandoPalanca = false;
        joystickIzquierdoLecturaZ.action.Disable();
        Debug.Log("Palanca soltada");
    }

    void Update()
    {
        if (!agarrandoPalanca)
            return;

        Vector2 input = joystickIzquierdoLecturaZ.action.ReadValue<Vector2>();
        Vector2 inputY = joystickIzquierdoLecturaY.action.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) > 0.01f) // Tolerancia para evitar ruido mínimo
        {
            // Rotación sobre eje Y (Vector3.up) usando la entrada X del joystick
            brazoBase.Rotate( 0f, input.x * velocidadRotacion * Time.deltaTime, 0f, Space.Self);
        }

        if (Mathf.Abs(inputY.y) > 0.01f) // Tolerancia para evitar ruido mínimo
        {
            // Rotación sobre eje Y (Vector3.up) usando la entrada X del joystick
            brazoSegundo.Rotate(0f, inputY.y * velocidadRotacion * Time.deltaTime, 0f, Space.Self);
        }
    }
}