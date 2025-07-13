using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PalaController : MonoBehaviour
{
    bool hasDebris = false;
    public GameObject debrisPosInPala;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision pala");
        if (other.gameObject.CompareTag("Debris"))
        {
            Debug.Log("Debris collission");
            Rigidbody debrisRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (debrisRigidbody != null)
            {
                // Ejecutar funcion de guardar debris sobre la pala
                debrisRigidbody.isKinematic = true;
                debrisRigidbody.useGravity = false;
                Debug.Log("has touched Debris with pala");
                this.hasDebris = true;
                other.transform.position = debrisPosInPala.transform.position;
                other.transform.SetParent(this.transform);
            }
        }
    }
}
