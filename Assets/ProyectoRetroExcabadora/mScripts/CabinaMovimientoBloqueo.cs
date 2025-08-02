using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CabinaMovimientoBloqueo : MonoBehaviour
{
    RetroExcabadora retroExcabadoraScript;

    public ActionBasedContinuousMoveProvider moveProvider;
    public XRGrabInteractable joystickIzquierdoInteractable; // Referencia al XRGrabInteractable del joystick izquierdo

    private bool bloqueado = false;
    private bool joystickAgarrado = false;

    void Start()
    {
        retroExcabadoraScript = GameObject.
            FindGameObjectWithTag("RetroExcabadoraTag").GetComponent<RetroExcabadora>();

        // Configurar listeners para detectar cuando el joystick izquierdo es agarrado/soltado
        if (joystickIzquierdoInteractable != null)
        {
            joystickIzquierdoInteractable.selectEntered.AddListener(_ => OnJoystickAgarrado());
            joystickIzquierdoInteractable.selectExited.AddListener(_ => OnJoystickSoltado());
        }
    }
    void Update()
    {
        if (RetroExcabadora.EnCabina) //&& !bloqueado)
        {
            moveProvider.enabled = false; // 🔒 Desactiva el movimiento continuo
            //Es imposible que bloqueado sea true ya que tiene que ser true para que esta linea se ejecute
            bloqueado = true;
            {
                // Solo leer el input si el joystick NO está agarrado
                if (!joystickAgarrado)
                {
                    Vector2 originalInputAxis = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>();
                    UsarInput(originalInputAxis);
                }
                else
                {
                    // Si el joystick está agarrado, enviar input cero
                    UsarInput(Vector2.zero);
                    Debug.Log("Joystick agarrado - Input bloqueado");
                }
            }

        }
        else if (!RetroExcabadora.EnCabina) //&& bloqueado)
        {
            moveProvider.enabled = true;  // 🔓 Activa nuevamente al salir
            bloqueado = false;
        }
    }
    void UsarInput(Vector2 input)
    {
        //Entrega el estado
        retroExcabadoraScript.updatePedalInput(input);
        //Debug.Log(input);
    }

    // Métodos para manejar cuando el joystick es agarrado/soltado
    void OnJoystickAgarrado()
    {
        joystickAgarrado = true;
        Debug.Log("Joystick izquierdo agarrado - Movimiento del vehículo bloqueado");
    }

    void OnJoystickSoltado()
    {
        joystickAgarrado = false;
        Debug.Log("Joystick izquierdo soltado - Movimiento del vehículo habilitado");
    }

    void OnDestroy()
    {
        // Limpiar listeners al destruir el objeto
        if (joystickIzquierdoInteractable != null)
        {
            joystickIzquierdoInteractable.selectEntered.RemoveListener(_ => OnJoystickAgarrado());
            joystickIzquierdoInteractable.selectExited.RemoveListener(_ => OnJoystickSoltado());
        }
    }
}
