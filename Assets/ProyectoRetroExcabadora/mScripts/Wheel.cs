using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Wheel : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform AttachPointLeft;
    public Transform AttachPointRight;
    private Vector3 startPos;
    private XRGrabInteractable _grabInteractable;
    private Rigidbody rigidbody;

    private bool isYelloBoxVisible = false;

    public void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Grabbed!");

        //if my left hand is ok then do x
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            grabInteractable.attachTransform = AttachPointLeft;
        }
        else
        {
            grabInteractable.attachTransform = AttachPointRight;
        }
    }  
    public void OnDrop(SelectExitEventArgs args)  
    {  
        Debug.Log("Dropped!");
        this.transform.position = startPos;
        this.rigidbody.velocity = Vector3.zero;
        this.rigidbody.angularVelocity = Vector3.zero;
        this.rigidbody.isKinematic = true;
        //reset the rotation, except the z one.
        float preservedZ = this.transform.rotation.eulerAngles.z;
        this.transform.rotation = Quaternion.Euler(52.492f, 2.007f, preservedZ);
    }

    //Esta funcion cambia a color amarillo cuando se puede interactuar con el objeto
    void InputHandlingForWheel()
    {
        Bounds wheelBounds = new Bounds();
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        this.rigidbody = rb;
        startPos = this.transform.position;
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnDrop);
    }
    // Update is called once per frame
    void Update()
    {

    }


}
