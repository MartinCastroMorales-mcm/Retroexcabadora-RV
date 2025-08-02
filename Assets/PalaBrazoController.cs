using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaBrazoController : MonoBehaviour
{
    public Vector3 offsetAtraccion = new Vector3(0, 0.5f, 0); // Offset relativo a la pala
    private Vector3 posicionAtraccionGlobal; // Posición mundial FIJA durante la atracción
    private GameObject debrisActual;
    private bool atrayendoDebris = false;
    private bool debrisAdherido = false; // Nueva variable para controlar cuando está adherido
    private Rigidbody rb;

    void Update()
    {
        // Si el Debris ha sido atraído y sigue moviéndose, actualizamos su posición
        if (atrayendoDebris && debrisActual != null)
        {
            // Usar la posición mundial FIJA capturada al iniciar la atracción
            Vector3 direccion = (posicionAtraccionGlobal - debrisActual.transform.position).normalized;
            float velocidad = 5f; // Ajusta la velocidad

            // Movimiento suave
            debrisActual.transform.position += direccion * velocidad * Time.deltaTime;

            // Comprobar si ha llegado a la posición
            if (Vector3.Distance(debrisActual.transform.position, posicionAtraccionGlobal) < 0.1f)
            {
                // Obtener el Rigidbody y configurarlo para que siga a la pala
                Rigidbody debrisRb = debrisActual.GetComponent<Rigidbody>();
                if (debrisRb != null)
                {
                    debrisRb.isKinematic = true; // Hacer que el Rigidbody sea kinematic
                    debrisRb.velocity = Vector3.zero; // Detener cualquier movimiento
                    debrisRb.angularVelocity = Vector3.zero; // Detener cualquier rotación
                }
                
                // Adherir el debris a la pala como hijo
                debrisActual.transform.SetParent(transform);
                
                // Cambiar estado: ya no atraer, ahora mantener adherido
                atrayendoDebris = false;
                debrisAdherido = true;
            }
        }
        
        // Si el debris está adherido, se mueve automáticamente con la pala (es hijo)
        if (debrisAdherido && debrisActual != null)
        {
            // Ya es hijo de la pala, se mueve automáticamente
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[TRIGGER] OnTriggerEnter detectado con: {other.name}");
        
        if (other.CompareTag("Debris") && debrisActual == null)
        {
            Debug.Log($"[TRIGGER] Es Debris y debrisActual es null - INICIANDO ATRACCIÓN");
            
            // CAPTURAR la posición mundial ACTUAL donde queremos atraer el debris
            posicionAtraccionGlobal = transform.TransformPoint(offsetAtraccion);
            Debug.Log($"[TRIGGER] Posición global FIJA capturada: {posicionAtraccionGlobal}");
            
            // Obtener el Rigidbody del Debris
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log($"[TRIGGER] Rigidbody encontrado - Mass: {rb.mass}, isKinematic: {rb.isKinematic}, useGravity: {rb.useGravity}");
                
                rb.useGravity = false; // Desactivamos la gravedad del Debris
                rb.velocity = Vector3.zero; // Detener movimiento actual
                rb.angularVelocity = Vector3.zero; // Detener rotación actual
                
                
                debrisActual = other.gameObject; // Guardamos la referencia al Debris
                atrayendoDebris = true; // Activamos la atracción
               }
            else
            {
                Debug.Log($"[TRIGGER] ERROR: {other.name} no tiene Rigidbody");
            }
        }
        else
        {
            Debug.Log($"[TRIGGER] NO procesa: Tag={other.tag}, debrisActual null={debrisActual == null}");
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
