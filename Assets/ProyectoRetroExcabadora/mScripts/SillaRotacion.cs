using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillaRotacion : MonoBehaviour
{
    public Transform xrRig; // Referencia al XR Origin
    private Quaternion rotacionInicial; // Almacena rotación original

    void Start()
    {
        rotacionInicial = transform.rotation; // Guarda la rotación inicial al iniciar
    }

    void Update()
    {
        if (RetroExcabadora.EnCabina && xrRig != null)
        {
            float rotY = xrRig.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, rotY, 0);
        }
        else
        {
            // Si saliste de la cabina, vuelve la silla a su posición original
            transform.rotation = rotacionInicial;
        }
    }
}
