using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RetroExcabadora : MonoBehaviour
{
    public InputActionProperty rightAction;
    public InputActionProperty leftAction;
    public GameObject player;
    public GameObject targetInVehicle;
    public GameObject playerCamera;
    public GameObject DoorCube;

    public GameObject LeftHand;
    public GameObject RightHand;
    //public CharacterController characterController;
    private bool isInVehicle = false;
    private bool isDoorCubeVisible = false;
    public Transform puntoMira;
    public Transform puntoSalida;
    public float cooldownTime = 1.0f;

    private float lastToggleTime = -999f;

    public static bool EnCabina { get; private set; } = false;

    public class stateContextContainer
    {
        private float speed = 0f;
        private float acceleration = 1f;
        public float turnSpeed = 5f;
        public int wheelAngle = 0;
        private const float maxTurnSpeed = 30f;
        private const float tiron = 0f;
        private const float maxAcceleration = 0f;
        private const float maxSpeed = 100f;
        private const float StaticFriction = 1f;
        private const float kineticFriction = 0f;
        

        public float getSpeed()
        {
            return this.speed;
        }
        public float getAcceleration()
        {
            return this.acceleration;
        }
        public void setSpeed(float speed)
        {
            if (speed <= maxSpeed)
            {
                this.speed = speed; 
            }
        }
        void increaseAcceleration(float dt)
        {
            if (this.acceleration >= maxAcceleration)
            {
                //return this.acceleration * dt;
            }

        }
        public void setTurnSpeed(float anguloNormalizado)
        {
            this.turnSpeed = anguloNormalizado * maxTurnSpeed;
        }
    }
    public stateContextContainer myStateContext;
    void Start()
    {
        myStateContext = new stateContextContainer();

    }

    void Update()
    {
        InputHandlingForDoor();
        moveForward(this.myStateContext.getSpeed());
        rotateVehicle(1f);
    }

    void InputHandlingForDoor()
    {
        //if either hand is in, the block turns yellow and the hand thats inside can "pinch it" to enter
        //Esta linea da error debido a que entrega las coordenadas locales
        //Bounds cubeBounds = new Bounds(DoorCube.transform.position, DoorCube.transform.localScale);
        Bounds cubeBounds = DoorCube.GetComponent<Renderer>().bounds;
        Vector3 rightHandPosition = RightHand.transform.position;
        Vector3 leftHandPosition = LeftHand.transform.position;
        if (cubeBounds.Contains(rightHandPosition))
        {
            Debug.Log("true look at yellow");
            //turn block yellow
            setDoorCubeVisible(true);
            //check if button is pressed
            if (rightAction.action.ReadValue<float>() == 1 && Time.time - lastToggleTime > cooldownTime)
            {
                lastToggleTime = Time.time;
                this.EnterExitVehicle();
            }
            return;
        }
        if (cubeBounds.Contains(leftHandPosition))
        {
            //turn block yellow
            setDoorCubeVisible(true);
            //check if button is pressed
            if (leftAction.action.ReadValue<float>() == 1 && Time.time - lastToggleTime > cooldownTime)
            {
                lastToggleTime = Time.time;
                this.EnterExitVehicle();
            }
            return;
        }
        setDoorCubeVisible(false);
    }

    void setDoorCubeVisible(bool willDoorVisible)
    {
        DoorCube.GetComponent<MeshRenderer>().enabled = willDoorVisible;
    }

    void EnterExitVehicle()
    {
        if (isInVehicle)
        {
            // Salir de la cabina
            player.transform.position = puntoSalida.transform.position;
            EnCabina = false;
            isInVehicle = false;
            player.transform.SetParent(null);
        }
        else
        {
            Debug.Log("Enter the vehicle");
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);
            // Entrar a la cabina
            player.transform.position = targetInVehicle.transform.position + new Vector3(0f, -0.5f, 0f);

            Vector3 direccionObjetivo = puntoMira.forward;
            direccionObjetivo.y = 0;

            Vector3 direccionCamara = playerCamera.transform.forward;
            direccionCamara.y = 0;

            Quaternion rotacionActual = Quaternion.LookRotation(direccionCamara);
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccionObjetivo);
            Quaternion ajusteRotacion = rotacionDeseada * Quaternion.Inverse(rotacionActual);

            player.transform.rotation = ajusteRotacion * player.transform.rotation;

            player.transform.SetParent(this.transform);

            EnCabina = true;
            isInVehicle = true;
        }
    }

    public void updatePedalInput(Vector2 input)
    {
        Debug.Log("this is public updateInput" + input);
        if (input.y == 1)
        {
            //moveForward(this.myStateContext.getSpeed());
            this.myStateContext.setSpeed(this.myStateContext.getSpeed() + this.myStateContext.getAcceleration());
        }
        if (input.y == -1)
        {
            this.myStateContext.setSpeed(this.myStateContext.getSpeed() - this.myStateContext.getAcceleration());
            //moveBackwards(this.myStateContext.getSpeed());
        }
        if (input.y == 0)
        {
            this.myStateContext.setSpeed(0f);
        }
    }
    public void updateWheelInput(float angle)
    {
        //set Turning Speed
        Debug.Log("Angulo de manurio" + angle);
        //maximo a la derecha -90
        if (angle < -90f)
        {
            angle = -90f;
        }
        //maximo a la izquierda 90
        if (angle > 90f)
        {
            angle = 90f;
        }
        float anguloNormalizado = angle / 90f;
        this.myStateContext.setTurnSpeed(anguloNormalizado);

    }

    void moveForward(float Speed)
    {
        this.transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    void rotateVehicle(float rotation)
    {
        if (this.myStateContext.getSpeed() > 0)
        {
            this.transform.Rotate(0, this.myStateContext.turnSpeed * Time.deltaTime, 0);
        }
    }
}

//add to press the button of the hand in tthe blob
//-3.847
//374.079
//7.8
//-142.6
