using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickAgarrado : MonoBehaviour
{
    public BrazoControl brazoControl;

    void Awake()
    {
        var interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener(_ => brazoControl.EmpezarAgarrar());
        interactable.selectExited.AddListener(_ => brazoControl.Soltar());
    }
}