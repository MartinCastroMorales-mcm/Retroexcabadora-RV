using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoControlSecundario : MonoBehaviour
{
    public Transform brazo;         // El objeto que rota en Y (segundo brazo)
    public Transform palancaVR;     // Joystick derecho

    public float sensibilidadY = 1f;
    public float anguloMaxY = 30f, anguloMinY = -75f;

    private bool estaAgarrado = false;
    private float tiempoInicioAgarrado = 0f;
    private float tiempoIgnorar = 0.5f;

    private Quaternion rotacionInicialPalanca;
    private Quaternion rotacionBase;
    private bool referenciaAjustada = false;

    public void EmpezarAgarrar()
    {
        estaAgarrado = true;
        tiempoInicioAgarrado = Time.time;
        referenciaAjustada = false;
        Debug.Log("Joystick derecho agarrado");
    }

    public void Soltar()
    {
        estaAgarrado = false;
        Debug.Log("Joystick derecho soltado");
    }

    void Update()
    {
        if (!estaAgarrado) return;
        if (Time.time - tiempoInicioAgarrado < tiempoIgnorar) return;

        if (!referenciaAjustada)
        {
            rotacionInicialPalanca = palancaVR.localRotation;
            rotacionBase = brazo.localRotation;
            referenciaAjustada = true;
            return;
        }

        float deltaY = palancaVR.localEulerAngles.y - rotacionInicialPalanca.eulerAngles.y;
        if (deltaY > 180f) deltaY -= 360f;

        float anguloY = Mathf.Clamp(deltaY * sensibilidadY, anguloMinY, anguloMaxY);

        brazo.localRotation = rotacionBase * Quaternion.Euler(0, anguloY, 0);

        Debug.Log($"ΔY: {deltaY:F2} | rotY: {anguloY:F2}");
    }
}
