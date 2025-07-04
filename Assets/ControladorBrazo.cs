using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorBrazo : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private InputActionReference moverBrazoIzquierda; // usar control izquierdo
    [SerializeField] private InputActionReference moverBrazoDerecha;   // usar control izquierdo

    [SerializeField] private float minY = 130f;  // Limite minimo en grados
    [SerializeField] private float maxY = 170f;  // Limite maximo en grados

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

        // Ajustar si pasa de 360�
        nuevaRotacionY = (nuevaRotacionY + 360f) % 360f;

        // Verificar que est� dentro de los l�mites
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