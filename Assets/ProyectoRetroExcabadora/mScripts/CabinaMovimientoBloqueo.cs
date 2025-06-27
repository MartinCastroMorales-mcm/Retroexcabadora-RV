using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CabinaMovimientoBloqueo : MonoBehaviour
{
    RetroExcabadora retroExcabadoraScript;

    public ActionBasedContinuousMoveProvider moveProvider;

    void Start()
    {
        retroExcabadoraScript = GameObject.
            FindGameObjectWithTag("RetroExcabadoraTag").GetComponent<RetroExcabadora>();
    }
    void Update()
    {
        if (RetroExcabadora.EnCabina)
        {
            {
                Vector2 originalInputAxis = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>();
                UsarInput(originalInputAxis);
            }
            // Guardamos el input original (opcional)

            // Creamos un override del input, anulándolo
            //moveProvider.leftHandMoveAction.action.Disable(); // desactiva el input
            //moveProvider.inputOverride = Vector2.zero;

            /*
            Martín: Por alguna razon esta linea funciona mientras que con Disable()
            el original input era siempre (0,0)
            */
            moveProvider.enabled = false;
        }
        else
        {
            moveProvider.enabled = true;
        }
    }
    void UsarInput(Vector2 input)
    {
        //Entrega el estado
        retroExcabadoraScript.updatePedalInput(input);
        Debug.Log(input);
    }
}
