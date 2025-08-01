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
        joystickIzquierdoLecturaY.action.Enable();
        Debug.Log("Palanca agarrada");
    }

    public void Soltar()
    {
        agarrandoPalanca = false;
        joystickIzquierdoLecturaZ.action.Disable();
        joystickIzquierdoLecturaY.action.Disable();
        Debug.Log("Palanca soltada");
    }

void Update()
{
    if (!agarrandoPalanca)
        return;

    Vector2 input = joystickIzquierdoLecturaZ.action.ReadValue<Vector2>();
    Vector2 inputY = joystickIzquierdoLecturaY.action.ReadValue<Vector2>();

    if (Mathf.Abs(input.x) > 0.01f)
    {
        // Obtener la rotación actual en el espacio local
        Vector3 rotacionActualX = brazoBase.localEulerAngles;

        // Convertimos a un rango [-180, 180] para evitar errores de overflow
        float rotY = rotacionActualX.y;
        if (rotY > 180f) rotY -= 360f;

        // Aplicar la rotación basada en el input
        rotY += input.x * velocidadRotacion * Time.deltaTime;

        // Limitar la rotación entre -45 y 45 grados (ajústalo a lo que necesites)
        rotY = Mathf.Clamp(rotY, -11f, 11f);

        // Aplicamos la rotación de vuelta
        brazoBase.localRotation = Quaternion.Euler(0f, rotY, 0f);
    }

    if (Mathf.Abs(inputY.y) > 0.01f) // Tolerancia para evitar ruido mínimo
     {
            // Obtener la rotación actual en el espacio local
            Vector3 rotacionActualY = brazoSegundo.localEulerAngles;

            // Convertimos a un rango [-180, 180] para evitar errores de overflow
            float rotZ = rotacionActualY.y;
            if (rotZ > 180f) rotZ -= 360f;

            // Aplicar la rotación basada en el input
            rotZ += input.x * velocidadRotacion * Time.deltaTime;

            // Limitar la rotación entre -60 y 45 grados
            rotZ = Mathf.Clamp(rotZ, -60f, 45f);

            // Aplicamos la rotación de vuelta
            brazoSegundo.localRotation = Quaternion.Euler(0f, rotZ, 0f);
            // Rotación sobre eje Y (Vector3.up) usando la entrada X del joystick
            brazoSegundo.Rotate(0f, inputY.y * velocidadRotacion * Time.deltaTime, 0f, Space.Self);
     }
    }
}