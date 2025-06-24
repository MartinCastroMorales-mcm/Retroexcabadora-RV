using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CabinaMovimientoBloqueo : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider moveProvider;

    private Vector2 originalInputAxis;

    void Update()
    {
        if (RetroExcabadora.EnCabina)
        {
            // Guardamos el input original (opcional)
            originalInputAxis = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>();

            // Creamos un override del input, anulándolo
            moveProvider.leftHandMoveAction.action.Disable(); // desactiva el input
        }
        else
        {
            if (!moveProvider.leftHandMoveAction.action.enabled)
                moveProvider.leftHandMoveAction.action.Enable(); // vuelve a activarlo
        }
    }
}
