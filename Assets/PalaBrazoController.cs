using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaBrazoController : MonoBehaviour
{
    public Transform puntoAtraccion; // El punto donde debería llegar el Debris (dentro de la pala)
    private GameObject debrisActual;
    private bool atrayendoDebris = false;
    private bool debrisAdherido = false; // Nueva variable para controlar cuando está adherido
    private Rigidbody rb;

    void Update()
    {
        // Si el Debris ha sido atraído y sigue moviéndose, actualizamos su posición
        if (atrayendoDebris && debrisActual != null)
        {
            // Mover el Debris hacia el punto de atracción
            Vector3 direccion = (puntoAtraccion.position - debrisActual.transform.position).normalized;
            float velocidad = 5f; // Ajusta la velocidad

            // Movimiento suave
            debrisActual.transform.position += direccion * velocidad * Time.deltaTime;

            // Comprobar si ha llegado a la posición
            if (Vector3.Distance(debrisActual.transform.position, puntoAtraccion.position) < 0.1f)
            {
                // Obtener el Rigidbody y configurarlo para que siga a la pala
                Rigidbody debrisRb = debrisActual.GetComponent<Rigidbody>();
                if (debrisRb != null)
                {
                    debrisRb.isKinematic = true; // Hacer que el Rigidbody sea kinematic
                    debrisRb.velocity = Vector3.zero; // Detener cualquier movimiento
                    debrisRb.angularVelocity = Vector3.zero; // Detener cualquier rotación
                }
                
                // Cambiar estado: ya no atraer, ahora mantener adherido
                atrayendoDebris = false;
                debrisAdherido = true;
            }
        }
        
        // Si el debris está adherido, mantenerlo en la posición del punto de atracción
        if (debrisAdherido && debrisActual != null && puntoAtraccion != null)
        {
            debrisActual.transform.position = puntoAtraccion.position;
            debrisActual.transform.rotation = puntoAtraccion.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Debris") && debrisActual == null)
        {
            // Obtener el Rigidbody del Debris
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false; // Desactivamos la gravedad del Debris
                rb.velocity = Vector3.zero; // Detener movimiento actual
                rb.angularVelocity = Vector3.zero; // Detener rotación actual
                debrisActual = other.gameObject; // Guardamos la referencia al Debris
                atrayendoDebris = true; // Activamos la atracción
            }
        }
    }

    // Método para liberar el debris (puedes llamarlo desde otro script si es necesario)
    public void LiberarDebris()
    {
        if (debrisActual != null)
        {
            // Restaurar el Rigidbody a su estado normal
            Rigidbody debrisRb = debrisActual.GetComponent<Rigidbody>();
            if (debrisRb != null)
            {
                debrisRb.isKinematic = false;
                debrisRb.useGravity = true;
            }
            
            debrisActual.transform.SetParent(null); // Deshacer el parentesco
            debrisActual = null; // Limpiar la referencia
            atrayendoDebris = false;
            debrisAdherido = false; // Resetear el estado de adherido
        }
    }
}
