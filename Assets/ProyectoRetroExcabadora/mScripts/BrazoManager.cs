using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoControl : MonoBehaviour
{
    public Transform rotadorY; // Rotación en Y
    public Transform brazo;    // Rotación en Z
    public Transform palancaVR;

    public float sensibilidadY = 1f;
    public float sensibilidadZ = 1f;
    public float anguloMaxY = 12f, anguloMinY = -12f;
    public float anguloMaxZ = 30f, anguloMinZ = -10f;

    private bool estaAgarrado = false;
    private float tiempoInicioAgarrado = 0f;
    private float tiempoIgnorar = 1f;

    private Quaternion rotacionInicialPalanca;
    private Quaternion rotacionBaseY;
    private Quaternion rotacionBaseZ;
    private bool referenciaAjustada = false;

    public void EmpezarAgarrar()
    {
        estaAgarrado = true;
        tiempoInicioAgarrado = Time.time;
        referenciaAjustada = false;
        Debug.Log("Palanca agarrada");
    }

    public void Soltar()
    {
        estaAgarrado = false;
        Debug.Log("Palanca soltada");
    }

    void Update()
    {
        if (!estaAgarrado) return;
        if (Time.time - tiempoInicioAgarrado < tiempoIgnorar) return;

        if (!referenciaAjustada)
        {
            rotacionInicialPalanca = palancaVR.localRotation;
            rotacionBaseY = rotadorY.localRotation;
            rotacionBaseZ = brazo.localRotation;
            referenciaAjustada = true;
            return;
        }

        float deltaY = palancaVR.localEulerAngles.y - rotacionInicialPalanca.eulerAngles.y;
        float deltaZ = palancaVR.localEulerAngles.z - rotacionInicialPalanca.eulerAngles.z;

        if (deltaY > 180f) deltaY -= 360f;
        if (deltaZ > 180f) deltaZ -= 360f;

        // Directamente clamped sin acumulación
        float anguloY = Mathf.Clamp(deltaY * sensibilidadY, anguloMinY, anguloMaxY);
        float anguloZ = Mathf.Clamp(deltaZ * sensibilidadZ, anguloMinZ, anguloMaxZ);

        rotadorY.localRotation = rotacionBaseY * Quaternion.Euler(0, anguloY, 0);
        brazo.localRotation = rotacionBaseZ * Quaternion.Euler(0, 0, anguloZ);

        Debug.Log($"ΔY: {deltaY:F2}, ΔZ: {deltaZ:F2} | rotY: {anguloY:F2}, rotZ: {anguloZ:F2}");
    }
}
