using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CabinaMovimientoBloqueo : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider moveProvider;

    private bool bloqueado = false;

    void Update()
    {
        if (RetroExcabadora.EnCabina && !bloqueado)
        {
            moveProvider.enabled = false; // 🔒 Desactiva el movimiento continuo
            bloqueado = true;
        }
        else if (!RetroExcabadora.EnCabina && bloqueado)
        {
            moveProvider.enabled = true;  // 🔓 Activa nuevamente al salir
            bloqueado = false;
        }
    }
}
