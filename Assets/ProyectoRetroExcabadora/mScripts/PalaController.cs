using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PalaController : MonoBehaviour
{
    public static PalaController Instance { get; private set; }
    private bool hasDebris;
    public GameObject debrisPosInPala;
    public GameObject Debris;
    public float waitTime = 2f;
    private bool isHigh;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Start PalaController");
        this.Debris = null;
        this.hasDebris = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Has Debris: " + this.hasDebris);
        if(waitTime > 0) {
            waitTime -= Time.deltaTime;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[PALA CONTROLLER] OnTriggerEnter con: {other.name}");
        
        if(waitTime > 0) {
            Debug.Log("[PALA CONTROLLER] wait for the next collision");
            return;
        }
        Debug.Log("[PALA CONTROLLER] collision pala");
        if (other.gameObject.CompareTag("Debris"))
        {
            Debug.Log("[PALA CONTROLLER] Debris collission - TOMANDO CONTROL");
            Rigidbody debrisRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (debrisRigidbody != null)
            {
                // DEBUG: Logs para diagnosticar posiciones
                Debug.Log($"[PALA CONTROLLER DEBUG] Debris original pos: {other.transform.position}");
                Debug.Log($"[PALA CONTROLLER DEBUG] debrisPosInPala pos: {debrisPosInPala.transform.position}");
                Debug.Log($"[PALA CONTROLLER DEBUG] Nueva posición calculada: {debrisPosInPala.transform.position + new Vector3(0, 1, 0)}");
                
                // Ejecutar funcion de guardar debris sobre la pala
                debrisRigidbody.isKinematic = true;
                debrisRigidbody.useGravity = false;
                this.hasDebris = true;
                Debug.Log("[PALA CONTROLLER] has touched Debris with pala: " + this.hasDebris);
                this.Debris = other.gameObject;
                other.transform.position = debrisPosInPala.transform.position + new Vector3(0, 1, 0);
                other.transform.SetParent(this.transform);
            }
        }
    }

    public void ReleaseDebris()
    {
        Debug.Log("ReleaseDebris hasDebris: " + this.hasDebris + " Debris is null: " + (this.Debris != null));
        if(this.hasDebris) {
            if(this.Debris != null) {
                Debug.Log("Excecute the body of the function Release");
                Rigidbody debrisRigidbody = this.Debris.GetComponent<Rigidbody>();
                if(debrisRigidbody != null) {
                    Debris.transform.SetParent(null);
                    debrisRigidbody.isKinematic = false;
                    debrisRigidbody.useGravity = true;
                    //debrisRigidbody.AddForce(transform.forward * 2f, ForceMode.Impulse);
                }
                this.hasDebris = false;
                this.Debris = null;
                this.waitTime = 2f;
            }
        }
    }
}
