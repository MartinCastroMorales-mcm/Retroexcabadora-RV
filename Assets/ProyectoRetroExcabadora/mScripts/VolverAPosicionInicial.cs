using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VolverAPosicionInicial : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private XRGrabInteractable grabInteractable;
    private Collider objetoCollider;

    void Start()
    {
        // Guarda la posición y rotación inicial
        posicionInicial = transform.localPosition;
        rotacionInicial = transform.localRotation;

        // Obtiene el componente XRGrabInteractable
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Obtiene el Collider para desactivar las colisiones temporalmente
        objetoCollider = GetComponent<Collider>();

        // Escucha el evento de soltar
        grabInteractable.selectExited.AddListener(OnSoltar);
    }

    private void OnSoltar(SelectExitEventArgs args)
    {
        // Inicia la corrutina que devuelve el objeto con retardo
        StartCoroutine(VolverConRetraso());
    }

    private IEnumerator VolverConRetraso()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Desactiva temporalmente las colisiones
            if (objetoCollider != null)
                objetoCollider.enabled = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Espera un breve tiempo para evitar colisiones
        yield return new WaitForSeconds(0.1f);

        // Mueve el objeto a su posición y rotación inicial
        transform.localPosition = posicionInicial;
        transform.localRotation = rotacionInicial;

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Vuelve a habilitar las colisiones después de la corrección
        if (objetoCollider != null)
            objetoCollider.enabled = true;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectExited.RemoveListener(OnSoltar);
    }
}
