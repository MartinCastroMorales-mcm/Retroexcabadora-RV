using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCabinaFixer : MonoBehaviour
{
    public Rigidbody[] joysticksRigidbodies; // Arrastra todos los joysticks de la cabina aquí
    
    private bool[] originalIsKinematic;
    
    void Start()
    {
        // Guardar el estado original de cada joystick
        if (joysticksRigidbodies != null)
        {
            originalIsKinematic = new bool[joysticksRigidbodies.Length];
            for (int i = 0; i < joysticksRigidbodies.Length; i++)
            {
                if (joysticksRigidbodies[i] != null)
                {
                    originalIsKinematic[i] = joysticksRigidbodies[i].isKinematic;
                }
            }
        }
    }
    
    void Update()
    {
        if (joysticksRigidbodies == null) return;
        
        if (RetroExcabadora.EnCabina)
        {
            // Cuando estés en la cabina, hacer todos los joysticks kinematic
            foreach (var rb in joysticksRigidbodies)
            {
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
        else
        {
            // Cuando salgas de la cabina, restaurar el estado original
            for (int i = 0; i < joysticksRigidbodies.Length; i++)
            {
                if (joysticksRigidbodies[i] != null)
                {
                    joysticksRigidbodies[i].isKinematic = originalIsKinematic[i];
                }
            }
        }
    }
}
