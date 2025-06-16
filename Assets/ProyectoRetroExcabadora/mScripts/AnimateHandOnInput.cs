using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public Animator handAnimator;
    public InputActionProperty pellizcoAnimationAction;
    public InputActionProperty agarreAnimationAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pellizcoAnimationAction.action.ReadValue<float>();
        //Aqui se le asigna que tecla se usa para la animacion
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = agarreAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
