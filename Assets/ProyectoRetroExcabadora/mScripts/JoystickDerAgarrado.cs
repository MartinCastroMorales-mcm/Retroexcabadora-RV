using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickDerAgarrado : MonoBehaviour
{
    public BrazoControlSecundario brazoControlSecundario;

    void Awake()
    {
        var interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener(_ => brazoControlSecundario.EmpezarAgarrar());
        interactable.selectExited.AddListener(_ => brazoControlSecundario.Soltar());
    }
}
