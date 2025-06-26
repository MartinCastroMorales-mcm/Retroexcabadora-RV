using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Joystick : MonoBehaviour
{
    //this two are X rotation
    public float maxAngleForFork = 30;
    public float minAngleForFork = 70;
    private float maxAngleForPala = 30f;
    private float minAngleForPala = 70f;
    public GameObject Pala;
    public GameObject ForkForPala;
    public GameObject joystickGameObject;
    private Vector3 startPos;
    private XRGrabInteractable _grabInteractable;
    private Rigidbody rigidbody;
    private float turnSpeed = 50f;

    private bool isGrabbing = false;
    public GameObject Retroexcabadora;

    public void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Grabbed!");
        isGrabbing = true;

        //if my left hand is ok then do x
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            //grabInteractable.attachTransform = AttachPointLeft;
        }
        else
        {
            //grabInteractable.attachTransform = AttachPointRight;
        }
    }  
    public void OnDrop(SelectExitEventArgs args)  
    {  
        Debug.Log("Dropped!");
        isGrabbing = false;
        this.transform.position = startPos;
        this.rigidbody.velocity = Vector3.zero;
        this.rigidbody.angularVelocity = Vector3.zero;
        this.rigidbody.isKinematic = true;
        //reset the rotation, except the z one.
        //float preservedZ = this.transform.rotation.eulerAngles.z;
        this.transform.rotation = Quaternion.Euler(-0.008f, 0.169f, -0.051f);
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
        if (true)
        {
            //TODO: REMPLAZAR VECTOR3 POR LA RETROEXCABADORA
            Transform joystickTransform = joystickGameObject.transform;

            //dotproduct            
            float tiltedX = Vector3.Dot(joystickTransform.up, Vector3.right);
            //vector3.right es el vector unitario de x
            if (tiltedX > 0.7f)
            { //cos(45) = 0.70711
                Debug.Log("Tilted to the right");
                moverPalaArriba();
            }
            else if (tiltedX < -0.7f)
            { //cos(45) = 0.70711
                Debug.Log("Tilted to the left");
                moverPalaAbajo();
            }
            else
            {
            }
            float tiltedZ = Vector3.Dot(joystickTransform.up, Vector3.forward);
            if (tiltedZ > 0.7f)
            { //cos(45) = 0.70711
                moverForkHaciaArriba();
                Debug.Log("Tilted to the forwards");
            }
            else if (tiltedZ < -0.7f)
            { //cos(45) = 0.70711
                Debug.Log("Tilted to the backwards");
                moverForkHaciaAbajo();
            }
        }
        //moverForkHaciaArriba();
        //moverForkHaciaAbajo();
        //moverPalaAbajo();
        //moverPalaArriba();
    }

    void moverPalaAbajo()
    {
        Transform retroexcabadoraTransform = Retroexcabadora.transform;
        Transform palaTransform = Pala.transform;

        float angle = Vector3.Angle(palaTransform.right, retroexcabadoraTransform.up);
        if (angle < minAngleForPala)
        {
            //Debug.Log("Angulo de pala1: " + angle);
            palaTransform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        //palaTransform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
    }
    void moverPalaArriba()
    {
        Transform retroexcabadoraTransform = Retroexcabadora.transform;
        Transform palaTransform = Pala.transform;


        float angle = Vector3.Angle(palaTransform.right, retroexcabadoraTransform.up);
        if (angle > maxAngleForPala)
        {
            palaTransform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }
        else
        {
            Debug.Log("Angulo de pala2: " + angle);

        }
    }

    void moverForkHaciaArriba()
    {
        Transform retroexcabadoraTransform = Retroexcabadora.transform;
        Transform forkTransform = ForkForPala.transform;
        float forkTiltedZ = Vector3.Dot(forkTransform.right,  retroexcabadoraTransform.right);
        if (forkTiltedZ > 0.64)
        { //cos(50) ~ 0.64
            Debug.Log("fork can move now: " + forkTiltedZ);
            forkTransform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }
        else
        {
            Debug.Log("fork should not move now: " + forkTiltedZ);
        }
    }
    void moverForkHaciaAbajo()
    {
        Transform retroexcabadoraTransform = Retroexcabadora.transform;
        Transform forkTransform = ForkForPala.transform;


        float angle = Vector3.Angle(forkTransform.right, retroexcabadoraTransform.up);
        if (angle < 90)
        {
            forkTransform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
    }

    void moverPala()
    {
        Transform joystickTransform = joystickGameObject.transform;
        Transform forkTransform = ForkForPala.transform;
        Transform palaTransform = Pala.transform;
        //Debug.Log("Fork rotation: " + forkTransform.localEulerAngles);
        //Debug.Log("Pala rotation: " + palaTransform.localEulerAngles);
        //Debug.Log("joystick rotation: " + palaTransform.eulerAngles);
         // Create a file in persistentDataPath (works on all platforms)
        //filePath = Path.Combine(Application.persistentDataPath, "rotation_log.txt");

        // Clear existing file or create new
        //File.WriteAllText(filePath, "Fork rotation: " + forkTransform.localEulerAngles + "\n");
        //File.WriteAllText(filePath, "Pala rotation: " + palaTransform.localEulerAngles + "\n");
        //File.WriteAllText(filePath, "joystick rotation: " + joystickTransform.eulerAngles + "\n");
        //joystick is forward
        bool isJoystickForward = false;
        if (joystickTransform.localEulerAngles.z <= 320 && joystickTransform.localEulerAngles.z >= 200)
        {
            isJoystickForward = true;
            Debug.Log("is forward: " + joystickTransform.localEulerAngles.z);
        } else
        {
            //Debug.Log("not is forward");
        }
        bool isJoystickBackwards = false;
        if (joystickTransform.localEulerAngles.z >= 40 && joystickTransform.localEulerAngles.z <= 150)
        {
            isJoystickBackwards = true;
            Debug.Log("is backwards: " + joystickTransform.localEulerAngles.z);
        }
        else
        {
            //Debug.Log("not is backwards");

        }
        
        if (true && NormalizeAngle(forkTransform.localEulerAngles.x) <= maxAngleForFork) 
        {
            forkTransform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        //if (isJoystickForward && NormalizeAngle(forkTransform.localEulerAngles.x) <= maxAngleForFork)
        //{
            //Debug.Log("fork angle: " + forkTransform.localEulerAngles.y);
            //forkTransform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        //}
        if (false && palaTransform.localEulerAngles.x >= minAngleForPala)
        {
            palaTransform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
        if (false && palaTransform.localEulerAngles.y <= maxAngleForPala)
        {
            palaTransform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }
    }
    float NormalizeAngle(float angle)
    {
        Debug.Log(angle);
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }
}
