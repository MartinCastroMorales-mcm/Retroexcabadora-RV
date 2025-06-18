using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorBrazo : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private InputActionReference moverBrazoIzquierda; // tecla A o botón izquierdo
    [SerializeField] private InputActionReference moverBrazoDerecha;   // tecla D o botón derecho

    [SerializeField] private float minY = 130f;  // Límite mínimo en grados
    [SerializeField] private float maxY = 170f;  // Límite máximo en grados

    private void OnEnable()
    {
        moverBrazoIzquierda.action.Enable();
        moverBrazoDerecha.action.Enable();
    }

    private void OnDisable()
    {
        moverBrazoIzquierda.action.Disable();
        moverBrazoDerecha.action.Disable();
    }

    void Update()
    {
        float direccion = 0f;

        if (moverBrazoIzquierda.action.IsPressed())
            direccion = 1f;
        else if (moverBrazoDerecha.action.IsPressed())
            direccion = -1f;

        float nuevaRotacionY = transform.eulerAngles.y + direccion * speed * Time.deltaTime;

        // Ajustar si pasa de 360°
        nuevaRotacionY = (nuevaRotacionY + 360f) % 360f;

        // Verificar que esté dentro de los límites
        if (nuevaRotacionY >= minY && nuevaRotacionY <= maxY)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                nuevaRotacionY,
                transform.eulerAngles.z
            );
        }
    }
}