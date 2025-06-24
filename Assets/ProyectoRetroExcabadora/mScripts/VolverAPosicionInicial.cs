using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VolverAPosicionInicial : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Guarda la posición y rotación inicial
        posicionInicial = transform.localPosition;
        rotacionInicial = transform.localRotation;

        // Obtiene el componente XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Escucha el evento de soltar
        grabInteractable.selectExited.AddListener(OnSoltar);
    }

    private void OnSoltar(SelectExitEventArgs args)
    {
        // Vuelve a la posición y rotación inicial
        transform.localPosition = posicionInicial;
        transform.localRotation = rotacionInicial;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.RemoveListener(OnSoltar);
    }
}